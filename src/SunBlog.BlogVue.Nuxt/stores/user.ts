import { defineStore } from "pinia";
import piniaPersistConfig from "@/stores/helper/persist";
import type { UserInfoState } from "./interfaces";
import type { OAuthAccountDetailOutput } from "~/api/models";

export enum StoreKey {
  User = "zblog_User",
  Auth = "zblog_Auth",
}
export const useUserStore = defineStore({
  id: StoreKey.User,
  state: (): UserInfoState => ({
    zToken: null,
    userInfo: null,
  }),
  getters: {},
  actions: {
    // Set Token
    setToken({ zToken, userInfo }: any) {
      this.zToken = zToken;
      this.userInfo = userInfo;
    },
    clearToken() {
      this.zToken = null;
      this.userInfo = null;
    },
    // Set setUserInfo
    setUserInfo(userInfo: OAuthAccountDetailOutput | undefined) {
      this.userInfo = userInfo!;
    },
  },
  persist:  process.client && piniaPersistConfig(StoreKey.User),
});
