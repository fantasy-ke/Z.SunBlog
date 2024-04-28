<template>
  <!-- banner -->
  <div class="tag-banner banner" :style="cover">
    <h1 class="banner-title">标签</h1>
  </div>
  <v-row class="home-container">
    <v-col md="9" cols="12">
      <!-- 标签列表 -->
      <v-card class="blog-container">
        <div class="tag-cloud-title">
          目前共计 {{ tags?.result?.length }} 个标签
        </div>
        <div class="tag-cloud">
          <a
            v-for="item of tags?.result"
            :style="{ 'font-size': item.color + '' }"
            :key="item.id"
            :href="'/tags/' + item.id"
          >
            {{ item.name }}
          </a>
        </div>
      </v-card>
    </v-col>
    <!-- 博主信息 -->
    <v-col md="3" cols="12" class="d-md-block d-none">
      <client-only><BlogInfo></BlogInfo></client-only>
    </v-col>
  </v-row>
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

onMounted(() => {
  tags?.value?.result?.forEach((item) => {
    item.color = Math.floor(Math.random() * 10) + 18 + "px";
  });
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
  background-color: #0099cc;
}
.tag-cloud-title {
  color: #555;
  font-size: 25px;
  line-height: 2;
  text-align: center;
}
@media (max-width: 759px) {
  .tag-cloud-title {
    font-size: 25px;
  }
}
.home-container {
  max-width: 1200px;
  margin: 320px auto 28px auto;
  padding: 0 5px;
}
@media (min-width: 760px) {
  :deep(.blog-container) {
    margin: 0;
  }
}
@media (max-width: 759px) {
  :deep(.blog-container) {
    margin: 0;
  }
}
.tag-cloud {
  margin-top: 20px;
  margin-bottom: 80px;
  text-align: center;
  a {
    display: inline-block;
    margin: 0px 16px;
    padding: 0 8px;
    color: #616161;
    line-height: 2;
    text-decoration: none;
    border-bottom: 1px solid #999;
    transition: all 0.3s;
  }
  a:hover {
    color: #03a9f4 !important;
    transform: scale(1.1);
  }
}
</style>
