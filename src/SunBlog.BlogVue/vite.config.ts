// Plugins
import vue from "@vitejs/plugin-vue";
import vuetify, { transformAssetUrls } from "vite-plugin-vuetify";
// Utilities
import { ConfigEnv, ProxyOptions, UserConfig } from "vite";
// 让项目支持require导入模块 import vitePluginRequire from 'vite-plugin-require'
import requireTransform from "vite-plugin-require-transform";
import { isProd, loadEnv } from "./src/utils/vite";
import { resolve } from "path";

const pathResolve = (dir: string): any => {
  return resolve(__dirname, ".", dir);
};

const viteConfig = ({ mode }: ConfigEnv): UserConfig => {
  const { VITE_PORT, VITE_OPEN, VITE_BASE_PATH, VITE_OUT_DIR, VITE_PROXY_URL, VITE_AXIOS_BASE_URL } = loadEnv(mode);

  const alias: Record<string, string> = {
    "@": pathResolve("./src"),
    assets: pathResolve("./src/assets"),
    // 'vue-i18n': isProd(mode) ? 'vue-i18n/dist/vue-i18n.cjs.prod.js' : 'vue-i18n/dist/vue-i18n.cjs.js',
  };

  let proxy: Record<string, string | ProxyOptions> = {
    "/api": {
      target: VITE_AXIOS_BASE_URL,
      ws: true,
      changeOrigin: true,
      secure: false, //解决target使用https出错问题
    },
  };

  return {
    plugins: [
      vue({
        template: { transformAssetUrls },
      }),
      vuetify({
        autoImport: true,
      }),
      requireTransform({
        fileRegex: /.ts$|.tsx$|.vue$/,
      }),
    ],
    define: { "process.env": {} },
    root: process.cwd(),
    resolve: {
      alias,
      extensions: [".js", ".json", ".jsx", ".mjs", ".ts", ".tsx", ".vue"],
    },
    css: {
      // css预处理器
      preprocessorOptions: {
        scss: {
          charset: false,
        },
      },
    },
    base: VITE_BASE_PATH,
    server: {
      host: true,
      port: VITE_PORT ?? window.configs.port,
      open: VITE_OPEN,
      proxy: proxy,
    },
    build: {
      cssCodeSplit: false,
      sourcemap: false,
      outDir: VITE_OUT_DIR,
      assetsDir: "assets",
      emptyOutDir: true,
      chunkSizeWarningLimit: 1500,
      rollupOptions: {
        // external: ['axios'],
        output: {
          manualChunks: {
            // 分包配置，配置完成自动按需加载
            vue: ["vue", "vue-router", "element-plus"],
          },
        },
      },
    },
  };
};

export default viteConfig;
