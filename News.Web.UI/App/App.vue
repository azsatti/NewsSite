<template>
  <div id="home">
    <router-link to="newsArticle">News Article List</router-link>
    <router-link to="addNewsArticle">Add News Article</router-link>
    <router-view></router-view>
  </div>
</template>

<script>
  export default { }
</script>

<style lang="scss">
  @import './variables.scss';

  #home {
  max-width: 400px;
  margin: 0 auto;
  line-height: 1.4;
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: $vue-blue;
  }

  h1 {
  text-align: center;
  }
</style>