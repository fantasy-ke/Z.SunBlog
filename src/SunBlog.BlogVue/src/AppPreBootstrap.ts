//import axios from "axios";
import { AppConsts } from "./assets/appConst/AppConsts";
import httpClient from "./utils/http-client";

export class AppPreBootstrap {
  /**
   * 启动
   * @param callback 回调函数
   */
  public static run(callback: () => void) {
    // 获取客户端基础配置
    AppPreBootstrap.getApplicationConfig(callback);
  }

  /**
   * 初始化前端基本配置
   * @param callback
   */
  public static getApplicationConfig(callback: () => void) {
    let envName = '';
    if (import.meta.env.MODE !== "development") {
      envName = 'prod';
    } else {
      envName = 'dev';
    }
    callback();
    // const url = 'assets/appconfig.' + envName + '.json';
    // httpClient.get(url,{
    //   headers: {
    //       'Cache-Control': 'no-cache'
    //   }})
    //   .then((response: any) => {
    //     const result = response.data;
    //     AppConsts.appBaseUrl = window.location.protocol + '//' + window.location.host;
    //     AppConsts.remoteServiceBaseUrl = result.remoteServiceBaseUrl;
        
    //   })
    //   .catch((err) => {
    //     alert(`初始化配置信息出错,错误信息:\n\n${err.message}`);
    //   });
  }
}
