export default defineNuxtPlugin((nuxtApp) => {
  nuxtApp.vueApp.config.errorHandler = (error, instance, info) => {
    // handle error, e.g. report to a service
    console.log("错误拦截器1");
    console.log(error, instance, info);
  };

  // Also possible
  nuxtApp.hook("vue:error", (error, instance, info) => {
    // handle error, e.g. report to a service
    console.log("错误拦截器2");
    console.log(error, instance, info);
  });
});
