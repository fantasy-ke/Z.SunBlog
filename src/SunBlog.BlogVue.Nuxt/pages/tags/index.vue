<template>
  <!-- banner -->
  <div class="tag-banner banner" :style="cover">
    <h1 class="banner-title">标签</h1>
  </div>
  <!-- 标签列表 -->
  <v-card class="blog-container">
    <div class="tag-cloud-title">目前共计 {{ tags?.result?.length }} 个标签</div>
    <div class="tag-cloud">
      <a
        :style="{ 'font-size': Math.floor(Math.random() * 10) + 18 + 'px;'  }"
        v-for="item of tags?.result"
        :key="item.id"
        :href="'/tags/' + item.id"
      >
        {{ item.name }}
      </a>
    </div>
  </v-card>
</template>

<script setup lang="ts">
import ArticleApi from "~/api/ArticleApi";
import AppApi from "~/api/AppApi";

const [{ data: tags }, { data: site }] = await Promise.all([
  ArticleApi.tags(),
  AppApi.info(),
]);

// 封面图
const cover = computed(() => {
  const arr = site.value?.result?.covers?.Tag ?? ["/cover/tag.png"];
  const url = arr[randomNumber(0, arr.length - 1)];
  return "background: url(" + url + ") center center / cover no-repeat";
});

useSeoMeta({
  title: "标签-" + site.value?.result?.site?.siteName,
  description: site.value?.result?.site?.description,
  keywords: site.value?.result?.site?.keyword,
});
useHead({
  link: [{ rel: "icon", href: site.value?.result?.site?.logoUrl ?? "" }],
});
</script>

<style scoped lang="scss">
.tag-banner {
  // background: url(https://www.static.talkxj.com/73lleo.png) center center /
  //   cover no-repeat;
  background-color: #0099CC;
}
.tag-cloud-title {
  line-height: 2;
  font-size: 25px;
  color: #555;
  text-align: center;
}
@media (max-width: 759px) {
  .tag-cloud-title {
    font-size: 25px;
  }
}
.tag-cloud {
  text-align: center;
  margin-top: 20px;
  margin-bottom: 80px;
  a {
    color: #616161;
    display: inline-block;
    text-decoration: none;
    padding: 0 8px;
    margin: 0px 16px;
    line-height: 2;
    transition: all 0.3s;
    border-bottom: 1px solid #999;
  }
  a:hover {
    color: #03a9f4 !important;
    transform: scale(1.1);
  }
}
</style>
