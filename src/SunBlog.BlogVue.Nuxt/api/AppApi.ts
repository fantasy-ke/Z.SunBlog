import http from "~/utils/http";
import type { BlogOutput, FriendLinkOutput } from "./models";
/**
 * 博客基本信息
 */
class AppApi {
  /**
   * 博客基本信息
   * @returns
   */
  info = () => {
    return http.get<BlogOutput>("/OAuths/Info");
  };
  /**
   * 友情链接
   * @returns
   */
  links = () => {
    return http.get<Array<FriendLinkOutput>>("/OAuths/Links");
  };
}

export default new AppApi();
