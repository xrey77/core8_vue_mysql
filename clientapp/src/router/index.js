import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Location from '@/views/Location.vue';
import About from '@/views/About.vue';
const routes = [
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
        // component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
    }
];
const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});
export default router;
//# sourceMappingURL=index.js.map