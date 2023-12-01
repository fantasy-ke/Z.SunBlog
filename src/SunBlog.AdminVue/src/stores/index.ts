// https://pinia.vuejs.org/
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';

// 创建
const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);

// 导出
export default pinia;
