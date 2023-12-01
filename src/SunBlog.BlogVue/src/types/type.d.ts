// api统一返回结果
declare interface ZResponseBase<T = any> {
  /**
   * 业务状态码
   */
  statusCode: number;
  /**
   * 是否成功
   */
  success: boolean;
  /**
   * 错误消息
   */
  errors?: string;
  /**
   * 结果
   */
  result?: T;

  /**
   * 扩展值
   */
  extras?: any;

  /**
   * 时间戳
   */
 unAuthorizedRequest: boolean;
}
