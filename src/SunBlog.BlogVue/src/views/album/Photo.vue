<template>
  <div>
    <!-- banner -->
    <div class="banner" :style="state.cover">
      <h1 class="banner-title">{{ state.name }}</h1>
    </div>
    <!-- 相册列表 -->
    <v-card class="blog-container">
      <div class="photo-wrap" id="photos">
        <img v-for="(item, index) of state.photos" class="photo" :key="index" :src="item.url!" />
      </div>
      <!-- 无限加载 -->
      <!-- <infinite-loading @infinite="infiniteHandler">
      <div slot="no-more" />
      <div slot="no-results" />
    </infinite-loading> -->
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { inject, nextTick, onMounted, onUnmounted, reactive, ref } from "vue";
import Viewer from "viewerjs";
import "viewerjs/dist/viewer.css";
import { AlbumsCsServiceProxy, PictureOutput } from "@/shared/service-proxies";
import { useRoute } from "vue-router";
const _albumsCService = new AlbumsCsServiceProxy(inject("$baseurl"), inject("$api"));
const route = useRoute();
const state = reactive({
  photos: [] as PictureOutput[],
  cover: "",
  name: "",
  query: {
    pageNo: 1,
    pageSize: 1000,
    albumId: route.params.id as never,
  },
});
const viewer = ref<Viewer | null>(null);
onMounted(() => {
  viewer.value = new Viewer(document.getElementById("photos") as HTMLElement);
});
onMounted(async () => {
  await _albumsCService.pictures(state.query.albumId, state.query.pageNo, state.query.pageSize).then((res) => {
    if (res.result) {
      let data = res.result;
      state.photos = data?.rows ?? [];
      let extras = res.extras;
      state.cover = "background: url(" + extras?.cover + ") center center / cover no-repeat";
      state.name = extras?.name;
      nextTick(() => {
        viewer.value = new Viewer(document.getElementById("photos") as HTMLElement);
      });
    }
  });
});

onUnmounted(() => {
  viewer.value?.destroy();
});
</script>

<style scoped>
.photo-wrap {
  display: flex;
  flex-wrap: wrap;
}
.photo {
  margin: 3px;
  cursor: pointer;
  flex-grow: 1;
  object-fit: cover;
  height: 200px;
}
.photo-wrap::after {
  content: "";
  display: block;
  flex-grow: 9999;
}
@media (max-width: 759px) {
  .photo {
    width: 100%;
  }
}
</style>
