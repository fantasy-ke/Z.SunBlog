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



declare  enum MethodType {
  GET = "GET",
  HEAD ="HEAD",
  PATCH ="PATCH",
  POST ="POST",
  PUT="PUT",
  DELETE="DELETE",
  CONNECT ="CONNECT",
  OPTIONS ="OPTIONS",
  TRACE ="TRACE"
}

// declare module "markdown-it-sub";
// declare module "markdown-it-sup";
// declare module "markdown-it-mark";
// declare module "markdown-it-abbr";
// declare module "markdown-it-container";
// declare module "markdown-it-deflist";
// declare module "markdown-it-emoji";
// declare module "markdown-it-footnote";
// declare module "markdown-it-ins";
// declare module "markdown-it-task-lists";
// declare module "markdown-it-katex-external";
