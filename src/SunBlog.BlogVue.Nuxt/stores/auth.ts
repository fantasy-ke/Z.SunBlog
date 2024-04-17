import OAuthApi from "~/api/OAuthApi";
import { defineStore } from "pinia";
import { reactive, computed } from "vue";
import signalR from "@/utils/signalRService";
export const useAuth = defineStore(StoreKey.Auth, () => {
  const userStore = useUserStore();

  /**
   * 登录
   * @param code 登录码
   * @returns
   */
  const login = async (code: string) => {
    const {data} = await OAuthApi.login(code);
    if (data.value?.success) {
      signalR.start();
      userStore.setToken({ zToken: data.value.result });
      await getUserInfo();
    } else {
      if (import.meta.server) {
        throw createError({
          message: data.value?.errors,
          statusCode: data.value?.statusCode,
        });
      }
    }
    return data.value?.result;
  };

  /**
   * 退出登录
   */

    /**
   * 退出登录
   */
    const logout = () => {
      OAuthApi.logout(userStore.zToken?.accessToken!).then(() => {
        const toast = useToast();
        userStore.clearToken();
        signalR.close();
        toast.success("退出登录成功！");
      });
    };

  /**
   * 退出登录
   */
  const clearToken = async () => {
    await OAuthApi.logout(userStore.zToken?.accessToken!);
    userStore.clearToken();
  };

  /**
   * 获取用户信息
   */
  const getUserInfo = async () => {
    const {
      data: { value },
    } = await OAuthApi.info();
    userStore.setUserInfo(value?.result);
  };

  const info = computed(() => {
    return userStore.userInfo;
  });

  const token = computed(() => {
    return userStore.zToken;
  });

  return { login, logout, getUserInfo, clearToken, info, token };
});
