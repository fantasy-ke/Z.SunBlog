import type { OAuthAccountDetailOutput, ZFantasyToken } from "~/api/models";

/* UserState */
export interface UserInfoState {
  zToken: ZFantasyToken | null;
  userInfo: OAuthAccountDetailOutput | null;
}
