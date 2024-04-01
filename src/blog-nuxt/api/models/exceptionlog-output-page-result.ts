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

import type { ExceptionlogOutput } from './exceptionlog-output';
 /**
 * 
 *
 * @export
 * @interface ExceptionlogOutputPageResult
 */
export interface ExceptionlogOutputPageResult {

    /**
     * @type {number}
     * @memberof ExceptionlogOutputPageResult
     */
    pageNo?: number;

    /**
     * @type {number}
     * @memberof ExceptionlogOutputPageResult
     */
    pageSize?: number;

    /**
     * @type {number}
     * @memberof ExceptionlogOutputPageResult
     */
    pages?: number;

    /**
     * @type {number}
     * @memberof ExceptionlogOutputPageResult
     */
    total?: number;

    /**
     * @type {Array<ExceptionlogOutput>}
     * @memberof ExceptionlogOutputPageResult
     */
    rows?: Array<ExceptionlogOutput> | null;
}
