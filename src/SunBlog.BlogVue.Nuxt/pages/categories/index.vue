<template>
  <!-- banner -->
  <div class="category-banner banner" :style="cover">
    <h1 class="banner-title">分类</h1>
  </div>
  <v-row class="home-container">
    <v-col md="9" cols="12">
      <!-- 分类列表 -->
      <v-card class="blog-container">
        <div class="category-title">
          目前共计 {{ categories?.result?.length }} 个分类
        </div>
        <ul class="category-list">
          <li
            class="category-list-item"
            v-for="item of categories?.result"
            :key="item.id"
          >
            <a :href="'/categories/' + item.id" :title="item.name!">
              {{ item.name }}
              <span class="category-count">({{ item.total }})</span>
            </a>
          </li>
        </ul>
      </v-card>
    </v-col>
    <!-- 博主信息 -->
    <v-col md="3" cols="12" class="d-md-block d-none">
      <client-only><BlogInfo></BlogInfo></client-only>
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import AppApi from "~/api/AppApi";
import ArticleApi from "~/api/ArticleApi";
const [{ data: categories }, { data: site }] = await Promise.all([
  ArticleApi.categories(),
  AppApi.info(),
]);

// 封面图
const cover = computed(() => {
  const arr = site.value?.result?.covers?.Category ?? ["/cover/category.jpg"];
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

<style scoped>
.category-banner {
  /* background: url(https://static.talkxj.com/config/83be0017d7f1a29441e33083e7706936.jpg)
        center center / cover no-repeat; */
  background-color: #0099cc;
}
.category-title {
  color: #555;
  font-size: 25px;
  line-height: 2;
  text-align: center;
}
@media (max-width: 759px) {
  .category-title {
    font-size: 28px;
  }
}
.category-list {
  margin: 0 1.8rem;
  list-style: none;
}
.category-list-item {
  padding: 8px 1.8rem 8px 0;
  font-size: 18px;
}
.category-list-item:before {
  position: relative;
  left: -0.75rem;
  display: inline-block;
  width: 12px;
  height: 12px;
  background: #fff;
  border: 0.2rem solid #0099cc;
  border-radius: 50%;
  transition-duration: 0.3s;
  content: "";
}
.category-list-item:hover:before {
  border: 0.2rem solid #ff7242;
}
.category-list-item a:hover {
  color: #8e8cd8;
  transition: all 0.3s;
}
.category-list-item a:not(:hover) {
  transition: all 0.3s;
}
.category-count {
  color: #858585;
  font-size: 1rem;
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
</style>
