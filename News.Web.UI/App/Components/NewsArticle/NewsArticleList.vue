<template>
  <div id="app">
    <h1>News articles list</h1>
    <ul>
      <li v-for="newsArticle in newsArticles" v-bind:key="newsArticle.Id">
        {{ newsArticle.title }} <br/>
        {{ newsArticle.body }} <br/>
        {{ newsArticle.authorName }} <br/>
        {{ newsArticle.datePublished }} <br/>
        
      </li>
    </ul>
  </div>
</template>

<script>
  import NewsDataAPI from '../../Api/NewsAPI.js'
  import $ from 'jquery'

  export default {
  data () {
  return {
  newsArticles: [],
  error: null,
  }
  },
  methods: {
  getNewsArticleList() {
  NewsDataAPI
  .getData()
  .then((data) => {
  this.newsArticles = data;
  });
  }
  },
  created () {
  this.getNewsArticleList();
  }
  }
</script>

<style lang="scss">
</style>