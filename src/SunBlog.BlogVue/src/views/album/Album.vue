<template>
  <!-- banner -->
  <div class="banner" :style="cover">
    <h1 class="banner-title">相册</h1>
  </div>
  <!-- 相册内容 -->
  <v-card class="blog-container">
    <v-row>
      <v-col :md="6" v-for="item of state.albums" :key="item.id" style="flex-basis: auto">
        <div class="album-item">
          <v-img class="album-cover" :src="item.cover" cover />
          <router-link :to="'/albums/' + item.id" class="album-wrapper">
            <div class="album-name">{{ item.name }}</div>
            <div class="album-desc">{{ item.remark ?? item.name }}</div>
          </router-link>
        </div>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-pagination
          v-if="state.pages > 1"
          v-model="state.query.pageNo"
          size="x-small"
          :length="state.pages"
          active-color="#00C4B6"
          :total-visible="3"
          variant="elevated"
        ></v-pagination>
      </v-col>
    </v-row>
  </v-card>
</template>

<script setup lang="ts">
import { AlbumsOutput, Pagination, AlbumsCsServiceProxy } from "@/shared/service-proxies";
import { computed, inject, onMounted, reactive, watch } from "vue";
import { useApp } from "@/stores/app";

const _albumsCService = new AlbumsCsServiceProxy(inject("$baseurl"), inject("$api"));
const appStore = useApp();
const state = reactive({
  query: {
    pageNo: 1,
    pageSize: 6,
  } as Pagination,
  pages: 0,
  albums: [] as AlbumsOutput[],
});
const cover = computed(() => {
  return "background: url(" + appStore.albumCover() + ") center center / cover no-repeat";
});

const loadData = async () => {
  await _albumsCService.getList(state.query).then((res) => {
    console.log(res);
    if (res.result) {
      let data = res.result;
      state.albums = data?.rows ?? [];
      state.pages = data?.pages ?? 0;
    }
  });
};
watch(
  () => state.query.pageNo,
  async () => {
    window.scrollTo(0, 0);
    await loadData();
  }
);
onMounted(async () => {
  await loadData();
});
</script>

<style scoped>
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
</style>
