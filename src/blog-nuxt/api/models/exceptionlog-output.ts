/* tslint:disable */
/* eslint-disable */
/**
 * SunBlog API
 * Web API for managing By Z.SunBlog
 *
 * OpenAPI spec version: SunBlog API v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

 /**
 * 
 *
 * @export
 * @interface ExceptionlogOutput
 */
export interface ExceptionlogOutput {

    /**
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    id?: string;

    /**
     * 请求URI
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    requestUri?: string | null;

    /**
     * 客户端IP
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    clientIP?: string | null;

    /**
     * 异常信息
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    message?: string | null;

    /**
     * 异常来源
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    source?: string | null;

    /**
     * 异常堆栈信息
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    stackTrace?: string | null;

    /**
     * 异常类型
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    type?: string | null;

    /**
     * 操作人id
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    operatorId?: string | null;

    /**
     * 操作人
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    operatorName?: string | null;

    /**
     * 用户代理（主要指浏览器）
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    userAgent?: string | null;

    /**
     * 操作系统
     *
     * @type {string}
     * @memberof ExceptionlogOutput
     */
    userOS?: string | null;

    /**
     * 创建时间
     *
     * @type {Date}
     * @memberof ExceptionlogOutput
     */
    creationTime?: Date | null;
}
