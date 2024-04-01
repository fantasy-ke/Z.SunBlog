import http from "~/utils/http";
import type { AlbumsOutputPageResult, PictureOutputPageResult } from "./models";
import type { Pagination } from "./models/pagination";

class AlbumsApi {
  /**
   * 相册分页查询
   * @returns
   */
  list = (data: Ref<Pagination>) => {
    return http.request<AlbumsOutputPageResult>("/AlbumsCs/GetList", {
      body: data,
      method: "POST",
    });
  };

  /**
   * 相册下的图片
   * @returns
   */
  pictures = (
    data: Ref<{
      pageNo: number;
      pageSize: number;
      albumId?: string;
    }>
  ) => {
    return http.request<PictureOutputPageResult>("/AlbumsCs/Pictures", {
      body:data,
      method: "POST"
    });
  };
}

export default new AlbumsApi();
