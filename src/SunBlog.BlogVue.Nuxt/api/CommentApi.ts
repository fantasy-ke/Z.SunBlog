import http from "~/utils/http";
import type { CommentPageQueryInput } from "./models/comment-page-query-input";
import type { AddCommentInput, ReplyOutputPageResult,CommentOutputPageResult, KeyDto } from "./models";

class CommentApi {
  /**
   * 评论或者留言列表
   * @param params
   * @returns
   */
  list = (data: Ref<CommentPageQueryInput>) => {
    return http.post<CommentOutputPageResult>("/CommentsCs/GetList", data);
  };

  /**
   *
   * @param params 回复分页查询
   * @returns
   */
  replyList = (data: CommentPageQueryInput) => {
    return http.post<ReplyOutputPageResult>("/CommentsCs/ReplyList", data);
  };

  /**
   *
   * @param data 评论、回复
   * @returns
   */
  add = (data: AddCommentInput) => {
    return http.post("CommentsCs/Add", data);
  };

  /**
   * 点赞
   * @param id 对象ID
   * @returns
   */
  praise = (data: KeyDto) => {
    return http.post<boolean>("/CommentsCs/Praise", data);
  };
}

export default new CommentApi();
