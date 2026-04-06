import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [  
{
    path: '/',
    component: () => import('@/views/Home.vue')
  },
  {
    path: '/location',
    component: () => import('@/views/Location.vue')
  },
  {
    path: '/about',
    component: () => import ('@/views/About.vue')
  },
  {
    path: '/profile',
    component: () => import('@/views/Profile.vue')
  },
  {
    path: '/productlist',
    component: () => import('@/views/ProductList.vue')
  },
  {
    path: '/productcatalogs',
    component: () => import('@/views/ProductCatalog.vue')
  },
  {
    path: '/productsearch',
    component: () => import('@/views/ProductSearch.vue')
  },

]
//webpack
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL), 
  routes
})

export default router
