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
 * @interface AddCommentInput
 */
export interface AddCommentInput {

    /**
     * 对应模块ID（null表留言，0代表友链的评论）
     *
     * @type {string}
     * @memberof AddCommentInput
     */
    moduleId?: string | null;

    /**
     * 顶级楼层评论ID
     *
     * @type {string}
     * @memberof AddCommentInput
     */
    rootId?: string | null;

    /**
     * 被回复的评论ID
     *
     * @type {string}
     * @memberof AddCommentInput
     */
    parentId?: string | null;

    /**
     * 回复人ID
     *
     * @type {string}
     * @memberof AddCommentInput
     */
    replyAccountId?: string | null;

    /**
     * 评论内容
     *
     * @type {string}
     * @memberof AddCommentInput
     */
    content: string;
}
