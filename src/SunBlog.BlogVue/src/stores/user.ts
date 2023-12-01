import { defineStore } from "pinia";
import piniaPersistConfig from "@/stores/helper/persist";
import { UserInfoState } from "./interfaces";
import { OAuthAccountDetailOutput } from "@/shared/service-proxies";

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
    setUserInfo(userInfo: OAuthAccountDetailOutput) {
      this.userInfo = userInfo;
    },
  },
  persist: piniaPersistConfig(StoreKey.User),
});
