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

import { AvailabilityStatus } from './availability-status';
 /**
 * 
 *
 * @export
 * @interface RolePageOutput
 */
export interface RolePageOutput {

    /**
     * 主键
     *
     * @type {string}
     * @memberof RolePageOutput
     */
    id?: string | null;

    /**
     * 角色名称
     *
     * @type {string}
     * @memberof RolePageOutput
     */
    name?: string | null;

    /**
     * 创建时间
     *
     * @type {Date}
     * @memberof RolePageOutput
     */
    createdTime?: Date | null;

    /**
     * @type {AvailabilityStatus}
     * @memberof RolePageOutput
     */
    status?: AvailabilityStatus;

    /**
     * 角色编码
     *
     * @type {string}
     * @memberof RolePageOutput
     */
    code?: string | null;

    /**
     * 排序值
     *
     * @type {number}
     * @memberof RolePageOutput
     */
    sort?: number;

    /**
     * 备注
     *
     * @type {string}
     * @memberof RolePageOutput
     */
    remark?: string | null;
}
