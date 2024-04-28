<template>
  <div class="blog-wrapper">
    <v-card class="animated zoomIn blog-card">
      <div class="author-wrapper">
        <!-- 博主头像 -->
        <v-avatar size="110" class="author-avatar" :image="info.avatarUrl!" />
        <div style="font-size: 1.375rem; margin-top: 0.625rem">
          {{ info.nikeName }}
        </div>
        <div style="font-size: 0.875rem">{{ info.motto }}</div>
      </div>
      <!-- 博客信息 -->
      <div class="blog-info-wrapper">
        <div class="blog-info-data">
          <a href="/archives">
            <div style="font-size: 0.875rem">文章</div>
            <div style="font-size: 1.25rem">
              {{ report.articleCount }}
            </div>
          </a>
        </div>
        <div class="blog-info-data">
          <a href="/categories">
            <div style="font-size: 0.875rem">分类</div>
            <div style="font-size: 1.25rem">
              {{ report.categoryCount }}
            </div>
          </a>
        </div>
        <div class="blog-info-data">
          <a href="/tags">
            <div style="font-size: 0.875rem">标签</div>
            <div style="font-size: 1.25rem">
              {{ report.tagCount }}
            </div>
          </a>
        </div>
      </div>
      <!-- 收藏按钮 -->
      <a
        class="collection-btn"
        @click="useToast().info('按CTRL+D 键将本页加入书签')"
      >
        <v-icon color="#fff" size="18" class="mr-1">mdi-bookmark</v-icon>
        加入书签
      </a>
      <!-- 社交信息 -->
      <div class="card-info-social">
        <a
          class="mr-5 iconfont iconqq"
          target="_blank"
          :href="`http://wpa.qq.com/msgrd?v=3&uin=111514&ste=${info.qq}&menu=yes`"
        />
        <a
          target="_blank"
          :href="info.github ?? ''"
          class="mr-5 iconfont icongithub"
        />
        <a
          target="_blank"
          :href="info.gitee ?? ''"
          class="iconfont icongitee-fill-round"
        />
      </div>
    </v-card>
    <!-- 网站信息 -->
    <v-card
      class="blog-card animated zoomIn mt-5 big"
      v-if="blogSetting.announcement"
    >
      <div class="web-info-title">
        <v-icon size="18">mdi-bell</v-icon>
        公告
      </div>
      <div style="font-size: 0.875rem">
        {{ blogSetting.announcement }}
      </div>
    </v-card>
    <!-- 网站信息 -->
    <v-card class="blog-card animated zoomIn mt-5">
      <div class="web-info-title">
        <v-icon size="18">mdi-chart-line</v-icon>
        网站资讯
      </div>
      <div class="web-info">
        <div style="padding: 4px 0 0">
          运行时间:<client-only
            ><span class="float-right">{{ runTime() }}</span></client-only
          >
        </div>
        <div style="padding: 4px 0 0">
          用户数量:<span class="float-right">
            {{ report.userCount }}
          </span>
        </div>
        <div style="padding: 4px 0 0">
          友链数量:<span class="float-right">
            {{ report.linkCount }}
          </span>
        </div>
      </div>
    </v-card>
  </div>
</template>
<script async setup lang="ts">
import dayjs from "dayjs";

const appStore = useApp();
const { info, report, blogSetting } = storeToRefs(appStore);

/**
 * 博客允许时间
 */
const runTime = (): string => {
  const timespan: number =
    new Date().getTime() -
    dayjs(blogSetting.value.runTime ?? "2023/06/01")
      .toDate()
      .getTime();
  const msPerDay: number = 24 * 60 * 60 * 1000;
  const daysold: number = Math.floor(timespan / msPerDay);
  let str: string = "";
  const day: Date = new Date();
  str += daysold + "天";
  str += day.getHours() + "时";
  str += day.getMinutes() + "分";
  str += day.getSeconds() + "秒";
  return str;
};
</script>
<style scoped lang="scss">
.blog-wrapper {
  position: sticky;
  top: 10px;
}
.blog-card {
  padding: 1.25rem 1.5rem;
  line-height: 2;
}
.author-wrapper {
  text-align: center;
}
.author-avatar {
  transition: all 0.5s;
}
.author-avatar:hover {
  transform: rotate(360deg);
}
.blog-info-wrapper {
  display: flex;
  justify-self: center;
  padding: 0.875rem 0;
}
.blog-info-data {
  flex: 1;
  text-align: center;
}
.blog-info-data a {
  text-decoration: none;
}
.collection-btn {
  position: relative;
  z-index: 1;
  display: block;
  height: 32px;
  color: #fff !important;
  font-size: 14px;
  line-height: 32px;
  text-align: center;
  background-color: #0099cc;
  border-radius: 5px;
  transition-duration: 1s;
  transition-property: color;
}
.collection-btn:before {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: -1;
  background: #ff7242;
  transform: scaleX(0);
  transform-origin: 0 50%;
  transition-timing-function: ease-out;
  transition-duration: 0.5s;
  transition-property: transform;
  content: "";
}
.collection-btn:hover:before {
  transform: scaleX(1);
  transition-timing-function: cubic-bezier(0.45, 1.64, 0.47, 0.66);
}
.card-info-social {
  margin: 6px 0 -6px;
  line-height: 40px;
  text-align: center;
}
.card-info-social a {
  font-size: 1.5rem;
}
.web-info {
  padding: 0.25rem;
  font-size: 0.875rem;
}
.big i {
  color: #f00;
  animation: big 0.8s linear infinite;
}
@keyframes big {
  0%,
  100% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.2);
  }
}
</style>
