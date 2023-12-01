import { AuthsServiceProxy, OAuthsServiceProxy } from "@/shared/service-proxies";
import { defineStore } from "pinia";
import { computed, inject } from "vue";
import apiHttpClient from "../utils/api-http-client";
import { StoreKey, useUserStore } from "./user";
import { useToast } from "./toast";
const _oAuthCService = new OAuthsServiceProxy(inject("$baseurl"), apiHttpClient as any);
const _authService = new AuthsServiceProxy(inject("$baseurl"), apiHttpClient as any);

export const useAuth = defineStore(StoreKey.Auth, () => {
  const userStore = useUserStore();
  /**
   * 登录
   * @param code 登录码
   * @returns
   */
  const login = async (code: string) => {
    const { result, success } = await _oAuthCService.login(code);
    if (success) {
      userStore.setToken({ zToken: result });
      await getUserInfo();
    }
    return result;
  };

  /**
   * 退出登录
   */
  const logout = () => {
    _authService.zSignOut(userStore.zToken.accessToken).then((res) => {
      const toast = useToast();
      userStore.clearToken();
      toast.success("退出登录成功！");
    });
  };

  /**
   * 获取用户信息
   */
  const getUserInfo = async () => {
    await _oAuthCService.userInfo().then((res) => {
      if (res.success) {
        userStore.setUserInfo(res.result);
      }
    });
  };

  const info = computed(() => {
    return userStore.userInfo;
  });

  return { login, logout, getUserInfo, info };
});
