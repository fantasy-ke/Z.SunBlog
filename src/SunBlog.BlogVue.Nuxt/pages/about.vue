<template>
  <div class="about-banner banner" :style="cover">
    <h1 class="banner-title">关于我</h1>
  </div>
  <!-- 关于我内容 -->
  <v-card class="blog-container">
    <div class="my-wrapper">
      <v-avatar size="110" class="author-avatar" :image="info.avatarUrl ?? img">
      </v-avatar>
    </div>
    <div class="about-content markdown-body" v-html="info.about" />
  </v-card>
</template>

<script setup lang="ts">
import img from "~/assets/images/1.jpg";
import { computed } from "vue";
import AppApi from "~/api/AppApi";
const { data, error } = await AppApi.info();
if (error.value) {
  throw createError(error.value);
}
const info = computed(() => {
  return data.value!.result!.info!;
});

// 封面图
const cover = computed(() => {
  const arr = data.value?.result?.covers?.about ?? ["/cover/about.jpg"];
  const url = arr[randomNumber(0, arr.length - 1)];
  return `background: url(${url}) center center / cover no-repeat`;
});

useSeoMeta({
  title: "关于我" + " - " + data.value?.result?.site?.siteName,
  description: data.value?.result?.site?.description,
  keywords: data.value?.result?.site?.keyword,
});
useHead({
  link: [{ rel: "icon", href: data.value?.result?.site?.logoUrl ?? "" }],
});
</script>

<style scoped>
.about-banner {
  /* background: url(https://static.talkxj.com/config/2a56d15dd742ff8ac238a512d9a472a1.jpg) center center / cover
      no-repeat; */
  background-color: #49b1f5;
}
.about-content {
  word-break: break-word;
  line-height: 1.8;
  font-size: 14px;
}
.my-wrapper {
  text-align: center;
  margin-top: 20px;
}
.author-avatar {
  transition: all 0.5s;
}
.author-avatar:hover {
  transform: rotate(360deg);
}
.about-content {
  margin-top: 20px;
}
</style>
