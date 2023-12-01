import { OAuthAccountDetailOutput, ZFantasyToken } from "@/shared/service-proxies";

/* UserState */
export interface UserInfoState {
  zToken: ZFantasyToken;
  userInfo: OAuthAccountDetailOutput;
}
