<template>
  <div id="app">
    <h1>Add News Article</h1>
    <ul>
      <li v-for="newsArticle in newsArticles" v-bind:key="newsArticle.Id">
        {{ newsArticle.title }} <br/>
        {{ newsArticle.body }} <br/>
        {{ newsArticle.authorName }} <br/>
        {{ newsArticle.datePublished }} <br/>
        
      </li>
    </ul>
    <button v-on:click="fetchNewsArticle">Add</button>
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
  info: "Not run yet"
  }
  },
  methods: {
  fetchNewsArticle() {
  this.info = "Fetching"
  NewsDataAPI
  .getData()
  .then((data) => {
  this.newsArticles = data;
  this.info = "Done"
  });
  }
  },
  }
</script>

<style lang="scss">
</style>