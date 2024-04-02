import type { UseFetchOptions } from "nuxt/app";
import { defu } from "defu";
import { jwtDecode } from "jwt-decode";
import markdownToHtml from "./markdown";

export const accessTokenKey = "access-token";
export const refreshAccessTokenKey = `x-${accessTokenKey}`;

// 清除 token
export const clearAccessTokens = () => { };


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
    // Session.set(accessTokenKey, accessToken);
    // Session.set(refreshAccessTokenKey, refreshAccessToken);
  }
}

class http {
  /**
   * 发起网络请求
   *
   * @param url - 请求的URL
   * @param options - 请求的选项
   * @returns Promise响应数据
   */
  request = <T>(url: string, options: UseFetchOptions<ZResponseBase<T>> = {}) => {
    const apiUrl = useRuntimeConfig().public.apiBaseUrl as string;
    
    const defaults: UseFetchOptions<ZResponseBase<T>> = {
      // 此配置在nuxt.config.ts中
      baseURL:
        import.meta.env.MODE === "production" && import.meta.client
          ? "/api"
          : apiUrl,
      key: url,
      onRequest({ request, options }) {
        const userStore = useUserStore();
        if (userStore.zToken?.accessToken) {
          options.headers = {
            ...options.headers,
            Authorization: `Bearer ${userStore.zToken?.accessToken}`,
          };
          // 判断 accessToken 是否过期
          const jwt: any = decryptJWT(userStore.zToken?.accessToken);
          const exp = getJWTDate(jwt.exp as number);
          //token已过期
          if (new Date() >= exp) {
            // 获取刷新 token
            // const refreshAccessToken = useCookie(refreshAccessTokenKey);
            // // 携带刷新 token
            // if (refreshAccessToken) {
            //   options.headers = {
            //     ...options.headers,
            //     "X-Authorization": `Bearer ${refreshAccessToken}`,
            //   };
            // }
          }
        }
      },
      /**
       * 响应处理函数
       * 当响应状态为200时，获取accessToken和refreshAccessToken
       * 如果响应成功，将响应数据存储在response._data中
       */
      onResponse({ request, response, options }) {
        if (response.status === 200) {
          const accessToken = response.headers.get(accessTokenKey);
          const refreshAccessToken = response.headers.get(
            refreshAccessTokenKey
          );
          // 判断是否是无效 token
          if (accessToken === "invalid_token") {
            clearAccessTokens();
          } else if (accessToken && refreshAccessToken) {
            const token = useCookie(accessTokenKey);
            const refreshToken = useCookie(refreshAccessTokenKey);
            token.value = accessToken;
            refreshToken.value = refreshAccessToken;
          }
          if (import.meta.client && !response._data?.success) {
            let message = "";
            switch (response._data?.statusCode) {
              case 401:
                clearAccessTokens();
                message = "请登录后重试！";
                break;
              case 403:
                message = "您没有权限，无法执行此项操作！";
                break;
              default:
                message = JSON.stringify(response._data?.errors);
                break;
            }
            useToast().error(message);
          } 
          else if (
            response._data?.success &&
            response.url.includes("/ArticleCs/Info")
          ) {
            let data = response._data.result;
            if (data && data.content && !data.isHtml) {
              data.content = markdownToHtml(data.content);
              response._data.result = data;
            }
          }
        }
      },

      /**
       * 响应处理错误函数
       * 当响应出错时抛出业务错误
       */
      onResponseError({ request, response, options, error }) {},
    };
    const params = defu(options, defaults);
    return useFetch(url, params);
  };

  /**
   * 发起 GET 请求
   *
   * @param url 请求的 URL
   * @param options 请求选项
   * @returns Promise包含响应数据
   */
  get<T = any>(url: string, options?: UseFetchOptions<ZResponseBase<T>>) {
    return this.request<T>(url, {
      ...options,
      method: "GET",
    });
  }

  /**
   * 发送POST请求
   *
   * @param url - 请求的URL
   * @param data - 请求的数据
   * @param options - 请求的选项
   * @returns 返回请求的响应数据
   */
  post = <T = any>(
    url: string,
    data?: any,
    options?: UseFetchOptions<ZResponseBase<T>>
  ) => {
    return this.request<T>(url, {
      ...options,
      method: "POST",
      body: data,
    });
  };
}

/**
 * 解密 JWT token 的信息
 * @param token jwt token 字符串
 * @returns <any>object
 */
export function decryptJWT(token: string): any {
  return jwtDecode(token);
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

export default new http();
