<template>
  <div id="app">
    <h1>Add news article</h1>
    <form>
    <div>
      <label for="title">Title</label>
      <input type="text" class="input" id ="title" name="title" />
    </div>
      <div>
        <label for="body">Body</label>
        <input type="text" class="input" id ="body" name="body"  />
      </div>
      <div>
        <label for="title">Author Name</label>
        <input type="text" class="input" id ="authorName" name="authorName" />
      </div>
      <div>
        <label for="title">Date Published</label>
        <input type="text" class="input" id ="datePublished" name="datePublished" />
      </div>
    <button v-on:click="addNewsArticle">Add</button>
    </form>
  </div>
</template>
