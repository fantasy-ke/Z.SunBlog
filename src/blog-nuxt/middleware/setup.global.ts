export default defineNuxtRouteMiddleware((to, from) => {
  if (to.path.toLowerCase() === "/user") {
    const isAuth = useCookie(accessTokenKey);
    if (!isAuth.value) {
      if (import.meta.client) {
        useToast().error("请先登录");
      }
      return "/";
    }
  }
});
