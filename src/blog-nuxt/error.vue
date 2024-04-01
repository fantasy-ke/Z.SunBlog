<template>
  <!-- <div>
    <h2>{{ props.error?.message }}</h2>
    <div>{{ props.error?.statusCode }}</div>
    <button @click="handleError">Clear errors 111</button>
  </div> -->
  <!-- <v-app>
    <v-main>
      <div
        class="banner"
        :style="{
          background: 'url(/cover/default.jpg) center center / cover no-repeat',
        }"
      >
        <h1 class="banner-title">
          {{
            props.error?.statusCode === 404
              ? "404 Page Not Found"
              : props.error?.message ?? "500 Internal Error"
          }}
          <p></p>
          <a href="/">返回首页</a>
        </h1>
      </div>
      <div class="blog-container">
        <img :src="props.error?.statusCode === 404 ? img404 : img50x" />
      </div>
    </v-main>
  </v-app> -->
  <section class="error-page">
    <div class="covers-main">
      <div class="wrapper">
        <div class="main-cover w3">
          <h1 style="color: white; text-align: center">
            {{ props.error?.message }}
          </h1>
          <h4 class="cover-para w3ls">{{ props.error?.statusCode }}</h4>
          <p class="form-text">
            Sorry, we're offline right now to make our site even better. Please,
            come back later and check what we've been up to.
          </p>
          <a href="/" class="back-button">Back to Home</a>
        </div>
        <div class="copyright text-center">
          <p>
            <a href="/">
              © 2020 Wed Error Page . All rights reserved | Design by
            </a>
          </p>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import "./assets/css/index.css";
const props = defineProps({
  error: Object,
});
const handleError = () => clearError({ redirect: "/" });
</script>

<style scoped>
.wrapper {
  width: 100%;
  padding-right: 15px;
  padding-left: 15px;
  margin-right: auto;
  margin-left: auto;
}
a {
  text-decoration: none;
}
@media (min-width: 576px) {
  .wrapper {
    max-width: 540px;
  }
}

@media (min-width: 768px) {
  .wrapper {
    max-width: 720px;
  }
}

@media (min-width: 992px) {
  .wrapper {
    max-width: 960px;
  }
}

@media (min-width: 1200px) {
  .wrapper {
    max-width: 1140px;
  }
}
.error-page .covers-main {
  background: url(/imgs/error.jpg) no-repeat center;
  background-size: cover;
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  -ms-background-size: cover;
  position: relative;
  z-index: 0;
  min-height: 100vh;
  align-items: center;
  display: grid;
  max-width: 100%;
}

.error-page .covers-main:before {
  content: "";
  background: rgba(16, 16, 16, 0.4);
  position: absolute;
  top: 0;
  min-height: 100%;
  left: 0;
  right: 0;
  z-index: -1;
}

.error-page .main-cover {
  max-width: 510px;
  margin: 0 auto;
}

.error-page a.logo {
  color: #fff;
  font-size: 32px;
  font-weight: 100;
  margin-bottom: 1rem;
  display: inline-block;
  line-height: 44px;
}

.error-page .cover-para {
  color: #fff;
  line-height: 120px;
  font-size: 180px;
  margin-bottom: 50px;
  font-weight: 600;
}

.error-page h4.cover-para span {
  font-size: 150px;
}

.error-page p.form-text {
  font-size: 17px;
  line-height: 25px;
  color: #eee;
}

.error-page a.back-button {
  border: none;
  font-weight: 600;
  display: inline-block;
  font-size: 16px;
  color: #fff;
  background: rgba(255, 255, 255, 0.16);
  text-align: center;
  padding: 12px 18px;
  margin-top: 4em;
  box-shadow: 0px 3px 4px 2px rgba(14, 14, 14, 0.04);
}

.copyright p {
  font-size: 15px;
  color: #ccc;
  line-height: 20px;
  margin-top: 4rem;
}

.copyright p a {
  color: #fff;
}

/* responsive */
@media screen and (max-width: 568px) {
  .error-page a.logo {
    font-size: 35px;
  }

  .error-page p.page-detail {
    font-size: 18px;
  }

  .error-page a.back-button {
    margin-top: 3em;
  }

  .error-page .cover-para {
    font-size: 130px;
    margin-bottom: 20px;
  }

  .error-page .cover-para {
    font-size: 160px;
  }

  .error-page h4.cover-para span {
    font-size: 130px;
  }
}

@media screen and (max-width: 384px) {
  .error-page p.form-text {
    font-size: 16px;
  }

  .error-page .cover-para {
    font-size: 130px;
  }

  .error-page h4.cover-para span {
    font-size: 110px;
  }
}
</style>
