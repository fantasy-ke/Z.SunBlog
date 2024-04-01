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

import { Gender } from './gender';
 /**
 * 
 *
 * @export
 * @interface UserInfoOutput
 */
export interface UserInfoOutput {

    /**
     * Id
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    id?: string | null;

    /**
     * 姓名
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    name?: string | null;

    /**
     * 账户名
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    userName?: string | null;

    /**
     * 头像
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    avatar?: string | null;

    /**
     * 生日
     *
     * @type {Date}
     * @memberof UserInfoOutput
     */
    birthday?: Date | null;

    /**
     * 邮箱
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    email?: string | null;

    /**
     * @type {Gender}
     * @memberof UserInfoOutput
     */
    gender?: Gender;

    /**
     * 昵称
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    nickName?: string | null;

    /**
     * 备注
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    remark?: string | null;

    /**
     * 最后登录ip
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    lastLoginIp?: string | null;

    /**
     * 最后登录IP所属地址
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    lastLoginAddress?: string | null;

    /**
     * 手机号码
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    mobile?: string | null;

    /**
     * 机构id
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    orgId?: string | null;

    /**
     * 机构名称
     *
     * @type {string}
     * @memberof UserInfoOutput
     */
    orgName?: string | null;

    /**
     * 授权按钮
     *
     * @type {Array<string>}
     * @memberof UserInfoOutput
     */
    authBtnList?: Array<string> | null;
}
