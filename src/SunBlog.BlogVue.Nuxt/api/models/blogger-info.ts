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

import type { ImgInfo } from './img-info';
 /**
 * 
 *
 * @export
 * @interface BloggerInfo
 */
export interface BloggerInfo {

    /**
     * @type {Array<ImgInfo>}
     * @memberof BloggerInfo
     */
    avatar?: Array<ImgInfo> | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    avatarUrl?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    nikeName?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    qq?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    github?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    gitee?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    motto?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    about?: string | null;

    /**
     * @type {string}
     * @memberof BloggerInfo
     */
    donation?: string | null;
}