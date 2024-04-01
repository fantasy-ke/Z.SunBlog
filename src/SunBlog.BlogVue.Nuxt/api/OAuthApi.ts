import http from "~/utils/http";
import type { AddLinkOutput, OAuthAccountDetailOutput } from "./models";

class OAuthApi {
  /**
   *  获取第三方登录跳转链接
   * @returns
   */
  get = (type = "qq") => {
    return http.get<string>("/OAuths/GetIpAddress",{
      query: { type },
    });
  };
  /**
   * 登录
   * @param code 登录码
   * @returns
   */
  login = (code: string) => {
    return http.get<string>("/OAuths/Login",{
      query: { code },
    });
  };
  /**
   * 获取用户信息
   * @returns
   */
  info = () => {
    return http.post<OAuthAccountDetailOutput>("/OAuths/UserInfo");
  };

  /**
   * 申请友链
   * @param data
   * @returns
   */
  addLink = (data: AddLinkOutput) => {
    return http.post("/oauth/link", data);
  };
}

export default new OAuthApi();
