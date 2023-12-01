
/**
 * 应用使用的常量
 */
export class AppConsts {
    /**
     * 远程服务器地址
     */
    public static remoteServiceBaseUrl: string = import.meta.env.VITE_AXIOS_BASE_URL;
  
    /**
     * 门户地址
     */
    public static portalBaseUrl: string;
  
    /**
     * 当前应用地址
     */
    public static appBaseUrl: string;
}