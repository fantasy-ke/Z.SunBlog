import { reactive } from "vue";
import http from "~/utils/http";
import type { Pagination } from "./models/pagination";
import type { TalksOutputPageResult, TalkDetailOutput } from "./models";

class TalksApi {
  /**
   * 说说列表
   * @param query 查询参数
   * @returns
   */
  list = (data: Ref<Pagination>) => {
    return http.post<TalksOutputPageResult>("/TalksCs/GetList",data);
  };

  /**
   * 说说详情信息
   * @param id 说说ID
   * @returns
   */
  talkDetail = (id: string) => {
    return http.post<TalkDetailOutput>("/TalksCs/TalkDetail", { id });
  };
}

export default new TalksApi();
