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

import type { FriendLinkPageOutput } from './friend-link-page-output';
 /**
 * 
 *
 * @export
 * @interface FriendLinkPageOutputPageResult
 */
export interface FriendLinkPageOutputPageResult {

    /**
     * @type {number}
     * @memberof FriendLinkPageOutputPageResult
     */
    pageNo?: number;

    /**
     * @type {number}
     * @memberof FriendLinkPageOutputPageResult
     */
    pageSize?: number;

    /**
     * @type {number}
     * @memberof FriendLinkPageOutputPageResult
     */
    pages?: number;

    /**
     * @type {number}
     * @memberof FriendLinkPageOutputPageResult
     */
    total?: number;

    /**
     * @type {Array<FriendLinkPageOutput>}
     * @memberof FriendLinkPageOutputPageResult
     */
    rows?: Array<FriendLinkPageOutput> | null;
}