import http from "~/utils/http";
import type { ArticleListQueryInput } from "./models/article-list-query-input";
import type {
  TagsOutput,
  CategoryOutput,
  ArticleReportOutput,
  ArticleOutputPageResult,
  ArticleInfoOutput,
  ArticleBasicsOutput,
} from "./models";

class ArticleApi {
  /**
   * 文章分页查询
   * @param params 查询参数
   * @returns
   */
  list = (data: Ref<ArticleListQueryInput>) => {
    return http.post<ArticleOutputPageResult>("/ArticleCs/GetList", data );
  };

  /**
   * 所有标签
   * @returns
   */
  tags = () => {
    return http.get<Array<TagsOutput>>("/ArticleCs/Tags");
  };

  /**
   * 所有栏目
   * @returns
   */
  categories = () => {
    return http.get<Array<CategoryOutput>>("/ArticleCs/Categories");
  };

  /**
   * 博客统计
   * @returns
   */
  report = () => {
    return http.get<ArticleReportOutput>("/ArticleCs/ReportStatistics");
  };
  /**
   * 文章详情
   * @param id 文章ID
   * @returns
   */
  info = (id: string) => {
    return http.get<ArticleInfoOutput>("/ArticleCs/Info", {
      query: { id },
    });
  };
  /**
   *  文章最新5条记录
   * @returns
   */
  latest = () => {
    return http.get<ArticleBasicsOutput[]>("/ArticleCs/Latest");
  };
}

export default new ArticleApi();
