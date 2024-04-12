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

import type { RouterMetaOutput } from './router-meta-output';
 /**
 * 
 *
 * @export
 * @interface RouterOutput
 */
export interface RouterOutput {

    /**
     * 路由名称
     *
     * @type {string}
     * @memberof RouterOutput
     */
    name?: string | null;

    /**
     * 路由地址
     *
     * @type {string}
     * @memberof RouterOutput
     */
    path?: string | null;

    /**
     * 组件
     *
     * @type {string}
     * @memberof RouterOutput
     */
    component?: string | null;

    /**
     * @type {RouterMetaOutput}
     * @memberof RouterOutput
     */
    meta?: RouterMetaOutput;

    /**
     * 子菜单
     *
     * @type {Array<RouterOutput>}
     * @memberof RouterOutput
     */
    children?: Array<RouterOutput> | null;
}