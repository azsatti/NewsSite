import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import NewsArticleList from './components/NewsArticle/NewsArticleList.vue'
import AddNewsArticle from './components/NewsArticle/AddNewsArticle.vue'

Vue.config.productionTip = false
Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        component: App,
        children: [
            {
                path: 'newsArticle',
                component: NewsArticleList
            },
            {
            path: 'addNewsArticle',
            component: AddNewsArticle
            }
        ]
    }
]

const router = new VueRouter({
    routes,
    mode: 'history'
})

new Vue({
    el: '#app',
    template: "<div><router-view></router-view></div>",
    router
})