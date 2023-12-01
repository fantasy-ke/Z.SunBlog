/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import { loadFonts } from "./webfontloader";
import vuetify from "./vuetify";
import pinia from "../stores";
import router from "../router";
import apiHttpClient from "../utils/api-http-client";

// Types
import type { App } from "vue";

export function registerPlugins(app: App) {
  loadFonts();
  app.use(vuetify).use(router).use(pinia);
}

export function registerglobal(app: App) {
  app.provide("$api", apiHttpClient);
}
