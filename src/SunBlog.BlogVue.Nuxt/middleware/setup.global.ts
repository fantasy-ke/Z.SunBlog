export default defineNuxtRouteMiddleware((to, from) => {
  if (to.path.toLowerCase() === "/user") {
    const { token } = useAuth()
    if (!token) {
      if (import.meta.client) {
        useToast().error("请先登录");
      }
      return "/";
    }
  }
});
