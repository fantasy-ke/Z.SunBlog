<template>
  <!-- banner -->
  <div class="message-banner" :style="cover">
    <!-- 弹幕输入框 -->
    <div class="message-container" v-if="site?.result?.site?.isAllowMessage">
      <h1 class="message-title">留言板</h1>
      <div class="animated fadeInUp message-input-wrapper">
        <input
          v-model="state.content"
          @click="state.show = true"
          @keyup.enter="addToList"
          placeholder="说点什么吧"
        />
        <button
          class="ml-3 animated bounceInLeft"
          @click="addToList"
          v-show="state.show"
        >
          发送
        </button>
      </div>
    </div>
    <!-- 弹幕列表 -->
    <div class="barrage-container">
      <client-only>
        <vue-danmaku
          ref="danmaku"
          v-model:danmus="state.items"
          useSlot
          :loop="loop"
          randomChannel
          :speeds="150"
        >
          <template v-slot:dm="{ index, danmu }">
            <span class="barrage-items" :key="index">
              <img
                :src="danmu.avatar"
                width="30"
                height="30"
                style="border-radius: 50%"
              />
              <span class="ml-2">{{ danmu.nickName }} :</span>
              <span class="ml-2">{{ danmu.content }}</span>
            </span>
          </template>
        </vue-danmaku>
      </client-only>
    </div>
  </div>
</template>
<script setup lang="ts">
//弹幕开源地址：https://github.com/hellodigua/vue-danmaku/tree/vue3
// import vueDanmaku from "vue3-danmaku";
import { useAuth } from "~/stores/auth";
import CommentApi from "~/api/CommentApi";
import { useToast } from "~/stores/toast";
// import vueDanmaku from "vue3-danmaku";
const vueDanmaku = defineAsyncComponent(() => import("vue3-danmaku"));
import AppApi from "~/api/AppApi";
import type { CommentPageQueryInput } from "~/api/models/comment-page-query-input";
import type { CommentOutput } from "~/api/models";
const pager = ref<CommentPageQueryInput>({ pageNo: 1, pageSize: 1000 });
const [{ data: list }, { data: site }] = await Promise.all([
  CommentApi.list(pager),
  AppApi.info(),
]);

const toast = useToast();
const authStore = useAuth();
const state = reactive({
  content: "",
  items: [] as CommentOutput[],
  show: false,
  visible: false,
  message: "",
});

// 发送弹幕
const addToList = async () => {
  if (!state.content) {
    toast.error("请输入内容");
    return;
  }
  const isAuth = useCookie(accessTokenKey);
  if (!isAuth.value) {
    useToast().error("请登录后重试！");
    return false;
  }
  const { data } = await CommentApi.add({
    content: state.content,
  });
  if (data.value?.success) {
    state.items.push({
      content: state.content,
      avatar: authStore.info?.avatar,
    });
    state.content = "";
  }
};
// 弹幕实例
const danmaku = ref<any>(null);

// 封面图
const cover = computed(() => {
  const arr = site.value?.result?.covers?.Message ?? ["/cover/message.png"];
  const url = arr[randomNumber(0, arr.length - 1)];
  return "background: url(" + url + ") center center / cover no-repeat";
});

// 循环播放
const loop = computed(() => {
  return state.items.length > 50;
});

onMounted(() => {
  setTimeout(() => {
    state.items = list.value?.result?.rows ?? ([] as CommentOutput[]);
  }, 500);
});

useSeoMeta({
  title: "留言-" + site.value?.result?.site?.siteName,
  description: site.value?.result?.site?.description,
  keywords: site.value?.result?.site?.keyword,
});
useHead({
  link: [{ rel: "icon", href: site.value?.result?.site?.logoUrl ?? "" }],
});
</script>

<style scoped>
.message-banner {
  position: absolute;
  top: -60px;
  left: 0;
  right: 0;
  height: 100vh;
  /* background: url(https://www.static.talkxj.com/d5ojdj.jpg) center center /
        cover no-repeat; */
  background-color: #49b1f5;
  animation: header-effect 1s;
  margin-top: 60px;
}
.message-title {
  color: #eee;
  animation: title-scale 1s;
}
.message-container {
  position: absolute;
  width: 360px;
  top: 35%;
  left: 0;
  right: 0;
  text-align: center;
  z-index: 5;
  margin: 0 auto;
  color: #fff;
}
.message-input-wrapper {
  display: flex;
  justify-content: center;
  height: 2.5rem;
  margin-top: 2rem;
}
.message-input-wrapper input {
  outline: none;
  width: 70%;
  border-radius: 20px;
  height: 100%;
  padding: 0 1.25rem;
  color: #eee;
  border: #fff 1px solid;
}
.message-input-wrapper input::-webkit-input-placeholder {
  color: #eeee;
}
.message-input-wrapper button {
  outline: none;
  border-radius: 20px;
  height: 100%;
  padding: 0 1.25rem;
  border: #fff 1px solid;
}
.barrage-container {
  position: absolute;
  top: 50px;
  left: 0;
  right: 0;
  bottom: 0;
  height: calc(100% -50px);
  width: 100%;
}
.barrage-items {
  background: rgb(0, 0, 0, 0.7);
  border-radius: 100px;
  color: #fff;
  padding: 5px 10px 5px 5px;
  align-items: center;
  display: flex;
}
.barrage-container .vue-danmaku {
  height: 100%;
}
</style>
