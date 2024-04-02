// https://nuxt.com/docs/api/configuration/nuxt-config
import vuetify, { transformAssetUrls } from "vite-plugin-vuetify";
export default defineNuxtConfig({
  devtools: { enabled: true },
  build: {
    transpile: ["vuetify"],
  },
  css: [
    "~/assets/css/index.css",
    "~/assets/css/iconfont.css",
    "~/assets/css/markdown.css",
  ],
  //配置打包的目录
  // nitro: {
  //   output: {
  //     publicDir: "./dist/public",
  //     dir: "./dist",
  //     serverDir: "./dist/server",
  //   },
  // },
  modules: [
    "@pinia/nuxt",
    (_options, nuxt) => {
      nuxt.hooks.hook("vite:extendConfig", (config) => {
        // @ts-expect-error
        config.plugins.push(vuetify({ autoImport: true }));
      });
    },
    '@pinia-plugin-persistedstate/nuxt',
    //...
  ],
  imports: {
    dirs: ["./stores"],
  },
  vite: {
    vue: {
      template: {
        transformAssetUrls,
      },
    },
  },
  runtimeConfig: {
    public: {
      apiBaseUrl: process.env.NUXT_API_BASE_URL,
    },
  },
  // modules: ["vuetify-nuxt-module"],yarn add -D vuetify vite-plugin-vuetify
  // vuetify: {
  //   moduleOptions: {
  //     /* module specific options */
  //   },
  //   vuetifyOptions: {
  //     /* vuetify options */
  //   },
  // },
});
