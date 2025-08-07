import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '../views/Home.vue'
import Location from '@/views/Location.vue'
import About from '@/views/About.vue'
import Profile from '@/components/users/profile.vue'
import List from '@/components/Products/List.vue'
import Catalogs from '@/components/Products/Catalogs.vue'
import Search from '@/components/Products/Search.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/location',
    name: 'location',
    component: Location
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/profile',
    name: 'profile',
    component: Profile
  },
  {
    path: '/productlist',
    name: 'productlist',
    component: List
  },
  {
    path: '/productcatalogs',
    name: 'productcatalogs',
    component: Catalogs
  },
  {
    path: '/productsearch',
    name: 'productsearch',
    component: Search
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
