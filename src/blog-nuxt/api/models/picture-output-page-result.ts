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

import type { PictureOutput } from './picture-output';
 /**
 * 
 *
 * @export
 * @interface PictureOutputPageResult
 */
export interface PictureOutputPageResult {

    /**
     * @type {number}
     * @memberof PictureOutputPageResult
     */
    pageNo?: number;

    /**
     * @type {number}
     * @memberof PictureOutputPageResult
     */
    pageSize?: number;

    /**
     * @type {number}
     * @memberof PictureOutputPageResult
     */
    pages?: number;

    /**
     * @type {number}
     * @memberof PictureOutputPageResult
     */
    total?: number;

    /**
     * @type {Array<PictureOutput>}
     * @memberof PictureOutputPageResult
     */
    rows?: Array<PictureOutput> | null;
}
