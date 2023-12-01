
/**
 * 扩展全局属性
 */
// declare module 'vue' {
//     interface ComponentCustomProperties {
//         //格式化日期
//         $formatDate: (value: string, format: string = "YYYY-MM-DD HH:mm:ss") => string
//     }
// }

declare interface Window {
  nextLoading: boolean;
  configs: {
    title: string;
    remoteServiceBaseUrl: string;
    baseWebSocketUrl: string;
    uploadUrl: string;
    port: number;
  };
}

