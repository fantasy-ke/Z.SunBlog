<template>
  <v-app :theme="theme">
    <!-- 导航栏 -->
    <TopNavBar />
    <!-- 侧边导航栏 -->
    <SideNavBar />
    <v-main>
      <slot />
    </v-main>
    <!-- 脚部 -->
    <Footer v-if="showFooter" />
    <!-- 返回顶部 -->
    <BackTop />
  </v-app>
</template>

<script async setup lang="ts">
import { storeToRefs } from "pinia";
import TopNavBar from "./TopNavBar.vue";
import SideNavBar from "./SideNavBar.vue";
import Footer from "./Footer.vue";
import BackTop from "./BackTop.vue";

const { theme } = storeToRefs(useThemeSettingStore());
const authStore = useAuth();
const route = useRoute();
const showFooter = computed(() => {
  return !route.path.startsWith("/message");
});
// onMounted(async () => {
// 第三方授权登录（QQ）
const code = (route.params.code || route.query.code) as string;
if (code) {
  onMounted(() => {
    nextTick(async () => {
      setTimeout(async () => {
        await authStore.login(code);
      }, 500);
    });
  });
}
// });
</script>
