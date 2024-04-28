<template>
  <div>
    <!-- banner -->
    <div class="banner" :style="cover">
      <h1 class="banner-title">归档</h1>
    </div>
    <!-- 归档列表 -->
    <v-row class="home-container">
      <v-col md="9" cols="12">
        <v-card class="blog-container">
          <timeline>
            <timeline-title bg-color="#49b1f5">
              还行！目前共计{{ sumCount ?? 0 }}篇文章，继续努力🪔
            </timeline-title>
            <timeline-item
              v-for="(key, index) of articleMap.keys()"
              :key="index"
              bg-color="#a9be7b"
            >
              <div style="font-size: 25px; color: #779649">{{ key }}</div>
              <timeline-item
                v-for="(daItem, indexDa) of articleMap.get(key)"
                :key="indexDa"
                :hollow="true"
                line-color="#779649"
              >
                <v-card style="padding: 10px 20px">
                  <!-- 日期 -->
                  <v-row>
                    <v-col md="2" cols="12">
                      <a
                        :href="'/articles/' + daItem.id"
                        style="width: 80px;height: 80px;overflow: hidden;display: inline-block;"
                        :title="daItem.title!"
                      >
                        <v-img
                          class="article-sort-item-img"
                          :src="daItem.cover!"
                          :cover="true"
                        />
                      </a>
                    </v-col>
                    <v-col md="10" cols="12" style="line-height: 35px;
"
                      ><div class="date">
                        <v-icon size="small">mdi mdi-calendar-range</v-icon
                        ><span class="time">{{
                          formatDate(daItem.publishTime)
                        }}</span>
                      </div>
                      <span v-if="daItem.isTop" style="font-size: 12px">
                        <span style="color: #ff7242">
                          <i class="iconfont iconzhiding" /> 置顶
                        </span>
                        <span class="separator">|</span>
                      </span>
                      <!-- 文章标题 -->
                      <a
                        :href="'/articles/' + daItem.id"
                        class="article-sort-item-title"
                        :title="daItem.title!"
                      >
                        {{ daItem.title }}
                      </a></v-col
                    >
                  </v-row>
                </v-card>
              </timeline-item>
            </timeline-item>
          </timeline>
          <!-- 分页按钮 -->
          <!-- <v-pagination
          v-if="(articles?.result?.pages ?? 0) > 1"
          size="x-small"
          :length="articles?.result?.pages"
          active-color="#1565C0"
          v-model="pager.pageNo"
          :total-visible="3"
          variant="elevated"
        ></v-pagination> -->
        </v-card>
      </v-col>
      <!-- 博主信息 -->
      <v-col md="3" cols="12" class="d-md-block d-none">
        <client-only><BlogInfo></BlogInfo></client-only>
      </v-col>
    </v-row>
  </div>
</template>

<script setup lang="ts">
// 文档：https://github.com/xiaojieajie/vue3-cute-timeline
// import "vue3-cute-component/dist/style.css";
// import { Timeline, TimelineTitle, TimelineItem } from "vue3-cute-component";
import type { Pagination } from "~/api/models/pagination";
import ArticleApi from "~/api/ArticleApi";
import AppApi from "~/api/AppApi";
import type { ArticleOutput } from "~/api/models";
const pager = ref<Pagination>({
  pageNo: 1,
  pageSize: 10,
});

const [{ data: articles }, { data: site }] = await Promise.all([
  ArticleApi.archiveList(),
  AppApi.info(),
]);

const sumCount = ref(0);
const articleMap = ref<Map<number, Array<ArticleOutput>>>(new Map());

watch(
  () => pager.value.pageNo,
  () => {
    setTimeout(() => {
      window.scrollTo({
        behavior: "smooth",
        top: 0,
      });
    }, 200);
  }
);

onMounted(async () => {
  let result = articles.value?.result as any;
  Object.keys(result ?? {}).forEach((key: string) => {
    const year = parseInt(key);
    const data = result[key] ?? [];
    articleMap.value.set(year, data);
  });
  articleMap.value = new Map(
    [...articleMap.value.entries()].sort((a, b) => b[0] - a[0])
  );
  sumCount.value = Array.from(articleMap.value.values()).reduce(
    (acc, cur) => acc + cur.length,
    0
  );
});

// 封面图
const cover = computed(() => {
  const arr = site.value?.result?.covers?.Archives ?? ["archives.jpg"];
  const url = arr[randomNumber(0, arr.length - 1)];
  return "background: url(" + url + ") center center / cover no-repeat";
});

useSeoMeta({
  title: "归档-" + site.value?.result?.site?.siteName,
  description: site.value?.result?.site?.description,
  keywords: site.value?.result?.site?.keyword,
});
useHead({
  link: [{ rel: "icon", href: site.value?.result?.site?.logoUrl ?? "" }],
});
</script>

<style scoped>
.date {
  color: #858585;
  font-size: 0.75rem;
}
.time {
  margin-left: 5px;
}
.timeline-item {
  padding-bottom: 0px;
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
.v-card--variant-elevated {
  border-top: 1px solid var(--7697135b-theme);
  box-shadow: none;
}
.article-sort-item-title{
  display: inline-block;
  color: #4c4948; 
  text-decoration: none;
  font-size: 1em;
  -webkit-transition: all 0.3s;
  -moz-transition: all 0.3s;
  -o-transition: all 0.3s;
  -ms-transition: all 0.3s;
  transition: all 0.3s;
  -webkit-line-clamp: 2;
}
.article-sort-item-title:hover {
    color: #49b1f5;
    -webkit-transform: translateX(10px);
    -moz-transform: translateX(10px);
    -o-transform: translateX(10px);
    -ms-transform: translateX(10px);
    transform: translateX(10px);
}
.article-sort-item-img{
  width: 100%;
    height: 100%;
  transition: filter 375ms ease-in 0.2s, transform 0.6s;
  object-fit: cover;
}
.article-sort-item-img:hover {
    -webkit-transform: scale(1.1);
    -moz-transform: scale(1.1);
    -o-transform: scale(1.1);
    -ms-transform: scale(1.1);
    transform: scale(1.1);
}
</style>
