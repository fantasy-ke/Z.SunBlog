import { AppConsts } from "@/assets/appConst/AppConsts";
import { useToast } from "@/stores/toast";
import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from "axios";
import { Session } from "./storage";
import { useUserStore } from "@/stores/user";

// token 键定义
export const accessTokenKey = "access-token";
export const refreshAccessTokenKey = `x-${accessTokenKey}`;

// 清除 token
export const clearAccessTokens = () => {};

/**
 * 检查并存储授权信息
 * @param res 响应对象
 */
export function checkAndStoreAuthentication(res: any): void {
  // 读取响应报文头 token 信息
  const accessToken = res.headers[accessTokenKey];
  const refreshAccessToken = res.headers[refreshAccessTokenKey];

  // 判断是否是无效 token
  if (accessToken === "invalid_token") {
    clearAccessTokens();
  }
  // 判断是否存在刷新 token，如果存在则存储在本地
  else if (refreshAccessToken && accessToken && accessToken !== "invalid_token") {
    Session.set(accessTokenKey, accessToken);
    Session.set(refreshAccessTokenKey, refreshAccessToken);
  }
}

/**
 * 解密 JWT token 的信息
 * @param token jwt token 字符串
 * @returns <any>object
 */
export function decryptJWT(token: string): any {
  token = token.replace(/_/g, "/").replace(/-/g, "+");
  const json = decodeURIComponent(escape(window.atob(token.split(".")[1])));
  return JSON.parse(json);
}

/**
 * 将 JWT 时间戳转换成 Date
 * @description 主要针对 `exp`，`iat`，`nbf`
 * @param timestamp 时间戳
 * @returns Date 对象
 */
export function getJWTDate(timestamp: number): Date {
  return new Date(timestamp * 1000);
}

class Axios {
  // 创建 axios 实例
  readonly apiHttpClient: AxiosInstance;
  // 基础配置，url和超时时间
  baseConfig: AxiosRequestConfig = {
    baseURL: window.configs.remoteServiceBaseUrl, //import.meta.env.VITE_API_URL,
    headers: { "Content-Type": "application/json" },
    timeout: 300000,
  };
  constructor(config?: AxiosRequestConfig) {
    this.apiHttpClient = axios.create(Object.assign(this.baseConfig, config));
    // request 拦截器 axios 的一些配置
    this.apiHttpClient.interceptors.request.use(
      (config: InternalAxiosRequestConfig) => {
        const userStore = useUserStore();
        if (config.headers && typeof config.headers.set === "function") {
          config.headers.set("Authorization", `Bearer ${userStore.zToken?.accessToken}`);
        }
        // if (accessToken) {
        //   // 将 token 添加到请求报文头中
        //   config.headers!["Authorization"] = `Bearer ${accessToken}`;
        //   // 判断 accessToken 是否过期
        //   // const jwt: any = decryptJWT(accessToken);
        //   // const exp = getJWTDate(jwt.exp as number);
        //   // //token已过期
        //   // if (new Date() >= exp) {
        //   //   // 获取刷新 token
        //   //   const refreshAccessToken = Session.getString(refreshAccessTokenKey);
        //   //   // 携带刷新 token
        //   //   if (refreshAccessToken) {
        //   //     config.headers!["X-Authorization"] = `Bearer ${refreshAccessToken}`;
        //   //   }
        //   // }
        // }

        return config;
      },
      (err: any) => {
        return Promise.reject(err);
      }
    );

    // respone 拦截器 axios 的一些配置
    this.apiHttpClient.interceptors.response.use(
      async (res: AxiosResponse) => {
        // 检查并存储授权信息
        // checkAndStoreAuthentication(res);
        const data = res.data;
        // code为200 或 返回的是文件流 直接返回
        if (data.statusCode === 200 || res.config.responseType === "blob") {
          return res;
        }
        let message = "";
        switch (data.statusCode) {
          case 401:
            clearAccessTokens();
            message = "请登录后重试...";
            break;
          case 403:
            message = "您没有权限，无法执行此项操作！";
            break;
          default:
            message = JSON.stringify(data.errors);
            break;
        }
        if (message) {
          const toast = useToast();
          toast.error(message);
        }
        if (data.statusCode === 401) {
          clearAccessTokens();
        }
        return res;
      },
      (err: any) => {
        // 这里用来处理http常见错误，进行全局提示
        let message = err.message;
        // 请求超时 && 网络错误单独判断，没有 response
        if (err.message.indexOf("timeout") !== -1) message = "请求超时！请您稍后重试";
        if (err.message.indexOf("Network Error") !== -1) message = "网络错误！请您稍后重试";
        if (err.response && err.response.status && !message) {
          switch (err.response.status) {
            case 400:
              message = "请求错误(400)";
              break;
            case 401:
              message = "未授权，请重新登录(401)";
              // 这里可以做清空storage并跳转到登录页的操作
              break;
            case 403:
              message = "拒绝访问(403)";
              break;
            case 404:
              message = "请求出错(404)";
              break;
            case 408:
              message = "请求超时(408)";
              break;
            case 500:
              message = "服务器错误(500)";
              break;
            case 501:
              message = "服务未实现(501)";
              break;
            case 502:
              message = "网络错误(502)";
              break;
            case 503:
              message = "服务不可用(503)";
              break;
            case 504:
              message = "网络超时(504)";
              break;
            case 505:
              message = "HTTP版本不受支持(505)";
              break;
            default:
              console.log(err, 15212);
              message = `连接出错(${err.response})!`;
          }
        }
        // 这里错误消息可以使用全局弹框展示出来
        if (message) {
          const toast = useToast();
          toast.error(message);
        }

        // 这里是AxiosError类型，所以一般我们只reject我们需要的响应即可
        return Promise.reject(err.response.data);
      }
    );
  }

  /**
   * request请求
   * @param config 配置参数
   * @returns
   */
  request = <T = any>(config: AxiosRequestConfig<T>): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.request(config);
  };

  /**
   * get请求
   * @param url 请求地址
   * @param config 配置参数
   * @returns
   */
  get = <T = any>(url: string, config?: AxiosRequestConfig): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.get(url, config);
  };

  /**
   * post请求
   * @param url 请求地址
   * @param data 请求数据
   * @param config 配置参数
   * @returns
   */
  post = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.post(url, data, config);
  };

  /**
   * put请求
   * @param url 请求地址
   * @param data 请求数据
   * @param config 配置参数
   * @returns
   */
  put = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.put(url, data, config);
  };

  /**
   * patch请求
   * @param url 请求地址
   * @param data 请求数据
   * @param config 配置参数
   * @returns
   */
  patch = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.patch(url, data, config);
  };

  /**
   * delete请求
   * @param url 请求地址
   * @param config 配置参数
   * @returns
   */
  delete = <T = any>(url: string, config?: AxiosRequestConfig): Promise<ZResponseBase<T>> => {
    return this.apiHttpClient.delete(url, config);
  };
  /**
   * 上传文件
   * @param url 上传地址
   * @param data 文件
   * @returns
   */
  upload = <T = any>(url: string, data?: any): Promise<T> => {
    return this.apiHttpClient.post(url, data, {
      headers: { "Content-Type": "multipart/form-data" },
    });
  };
}
export default new Axios();
