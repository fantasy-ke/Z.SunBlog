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
 * @interface ArticleReportOutput
 */
export interface ArticleReportOutput {

    /**
     * 文章数量
     *
     * @type {number}
     * @memberof ArticleReportOutput
     */
    articleCount?: number;

    /**
     * 标签数量
     *
     * @type {number}
     * @memberof ArticleReportOutput
     */
    tagCount?: number;

    /**
     * 栏目数量
     *
     * @type {number}
     * @memberof ArticleReportOutput
     */
    categoryCount?: number;

    /**
     * 用户量
     *
     * @type {number}
     * @memberof ArticleReportOutput
     */
    userCount?: number;

    /**
     * 友链数量
     *
     * @type {number}
     * @memberof ArticleReportOutput
     */
    linkCount?: number;
}