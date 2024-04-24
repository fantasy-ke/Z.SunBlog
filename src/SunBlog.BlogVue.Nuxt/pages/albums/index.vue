<template>
  <!-- banner -->
  <div class="banner" :style="cover">
    <h1 class="banner-title">相册</h1>
  </div>
  <!-- 相册内容 -->
  <v-card class="blog-container">
    <v-row class="row">
      <v-col
        :md="6"
        v-for="item of list?.result?.rows"
        :key="item.id"
        style="flex-basis: auto"
      >
        <div class="album-item">
          <v-img class="album-cover" :src="item.cover!" cover />
          <a
            class="album-wrapper"
            :href="'/albums/' + item.id"
            :title="item.name!"
          >
            <div class="album-name">{{ item.name }}</div>
            <div class="album-desc">{{ item.remark ?? item.name }}</div>
          </a>
        </div>
      </v-col>
    </v-row>
    <v-row class="pager">
      <v-col>
        <v-pagination
          v-if="(list?.result?.pages ?? 0) > 1"
          v-model="pager.pageNo"
          size="x-small"
          :length="list?.result?.pages"
          active-color="#1565C0"
          :total-visible="3"
          variant="elevated"
        ></v-pagination>
      </v-col>
    </v-row>
  </v-card>
</template>

<script setup lang="ts">
import AppApi from "~/api/AppApi";
import type { Pagination } from "~/api/models/pagination";
import AlbumsApi from "~/api/AlbumsApi";
const pager = ref<Pagination>({
  pageNo: 1,
  pageSize: 6,
});
const [{ data: list }, { data: site }] = await Promise.all([
  AlbumsApi.list(pager),
  AppApi.info(),
]);

// 封面图
const cover = computed(() => {
  const arr = site.value?.result?.covers?.Album ?? ["/cover/album.jpg"];
  const url = arr[randomNumber(0, arr.length - 1)];
  return "background: url(" + url + ") center center / cover no-repeat";
});

watch(
  () => pager.value.pageNo,
  () => {
    if (import.meta.client) {
      setTimeout(() => {
        window.scrollTo({
          behavior: "smooth",
          top: 0,
        });
      }, 200);
    }
  }
);

useSeoMeta({
  title: "相册-" + site.value?.result?.site?.siteName,
  description: site.value?.result?.site?.description,
  keywords: site.value?.result?.site?.keyword,
});
</script>

<style lang="scss" scoped>
.album-item {
  overflow: hidden;
  position: relative;
  cursor: pointer;
  background: #000;
  border-radius: 0.5rem !important;
}
.album-cover {
  position: relative;
  max-width: none;
  width: calc(100% + 1.25rem);
  height: 250px;
  opacity: 0.8;
  transition: opacity 0.35s, transform 0.35s;
  transform: translate3d(-10px, 0, 0);
  object-fit: cover;
}
.album-wrapper {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  padding: 1.8rem 2rem;
  color: #fff !important;
}
.album-item:hover .album-cover {
  transform: translate3d(0, 0, 0);
  opacity: 0.4;
}
.album-item:hover .album-name:after {
  transform: translate3d(0, 0, 0);
}
.album-item:hover .album-desc {
  opacity: 1;
  filter: none;
  transform: translate3d(0, 0, 0);
}
.album-name {
  font-weight: bold;
  font-size: 1.25rem;
  overflow: hidden;
  padding: 0.7rem 0;
  position: relative;
}
.album-name:after {
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 2px;
  background: #fff;
  content: "";
  transition: transform 0.35s;
  transform: translate3d(-101%, 0, 0);
}
.album-desc {
  margin: 0;
  padding: 0.4rem 0 0;
  line-height: 1.5;
  opacity: 0;
  transition: opacity 0.35s, transform 0.35s;
  transform: translate3d(100%, 0, 0);
}

.load-wrapper {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
  button {
    background-color: #0099CC;
    color: #fff;
  }
}
@media (max-width: 759px) {
  .blog-container {
    .row {
      padding-top: 14px;
    }
  }
  .pager {
    margin-bottom: 14px;
  }
}
</style>
