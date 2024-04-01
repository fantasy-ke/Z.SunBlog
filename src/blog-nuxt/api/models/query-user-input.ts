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
 * @interface QueryUserInput
 */
export interface QueryUserInput {

    /**
     * @type {number}
     * @memberof QueryUserInput
     */
    pageNo?: number;

    /**
     * @type {number}
     * @memberof QueryUserInput
     */
    pageSize?: number;

    /**
     * 账号
     *
     * @type {string}
     * @memberof QueryUserInput
     */
    userName?: string | null;

    /**
     * 组织机构Id
     *
     * @type {string}
     * @memberof QueryUserInput
     */
    orgId?: string | null;

    /**
     * 手机号
     *
     * @type {string}
     * @memberof QueryUserInput
     */
    mobile?: string | null;

    /**
     * 姓名
     *
     * @type {string}
     * @memberof QueryUserInput
     */
    name?: string | null;
}
