import "vue-toastification/dist/index.css";
import "nprogress/nprogress.css";
import NProgress from "nprogress";
import Toast from "vue-toastification";

export default defineNuxtPlugin((app) => {
  app.hook("app:created", async () => {
    // 初始化基本信息
    const appStore = useApp();
    await appStore.init();
    await appStore.getSiteReport();
  });
  app.hook("page:start", () => {
    NProgress.start();
  });
  app.hook("page:finish", () => {
    NProgress.done();
  });

  // 使用弹窗提示
  app.vueApp.use(Toast);
});
