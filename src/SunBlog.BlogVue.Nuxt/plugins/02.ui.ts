import { createVuetify } from "vuetify";
import "vuetify/styles";
import "@mdi/font/css/materialdesignicons.css";
import "vue3-cute-component/dist/style.css";
import * as pkg from "vue3-cute-component";
export default defineNuxtPlugin((app) => {
  // 使用UI框架
  const vuetify = createVuetify({
    theme: {
      themes: {
        light: {
          colors: {
            primary: "#1867C0",
            secondary: "#5CBBF6",
          },
        },
      },
    },
  });
  app.vueApp.use(vuetify);
  app.vueApp.use(pkg.plugin);
});
