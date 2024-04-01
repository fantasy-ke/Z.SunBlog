<template>
  <!-- banner -->
  <div
    class="banner"
    :style="{
      background:
        'url(' + data?.extras?.cover + ') center center / cover no-repeat',
    }"
  >
    <h1 class="banner-title">{{ data?.extras?.name }}</h1>
  </div>
  <!-- 相册列表 -->
  <v-card class="blog-container">
    <div class="photo-wrap" id="photos">
      <img
        v-for="(item, index) of data?.result?.rows"
        class="photo"
        :key="index"
        :src="item.url!"
      />
    </div>
  </v-card>
</template>

<script setup lang="ts">
import Viewer from "viewerjs";
import "viewerjs/dist/viewer.css";
import AlbumsApi from "~/api/AlbumsApi";

const route = useRoute();

definePageMeta({
  validate: async (route) => {
    // 验证id是否为数字
    return /.*/.test(route.params.id as string);
  },
});

const id = route.params.id as string;
const pager = ref({
  pageNo: 1,
  pageSize: 1000,
  albumId: id,
});
const { data } = await AlbumsApi.pictures(pager);

const viewer = ref<Viewer | null>(null);
onMounted(() => {
  nextTick(() => {
    viewer.value = new Viewer(document.getElementById("photos")!);
  });
});

onUnmounted(() => {
  viewer.value?.destroy();
});

useSeoMeta({
  title: "详情-" + data?.value?.extras?.name,
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
  .photo-wrap {
    padding: 5px;
  }
  .photo {
    width: 100%;
    border-radius: 15px;
    object-fit: cover;
    margin: auto;
    margin-bottom: 5px;
  }
}
</style>
