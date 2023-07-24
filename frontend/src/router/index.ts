import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import HallView from '../views/HallView.vue';
import VisitorsView from '../views/VisitorsView.vue';
import WeatherView from '../views/WeatherView.vue';
import GoodsAndServicesView from '../views/GoodsAndServicesView.vue';
import AdminView from '../views/AdminView.vue';

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: { name: 'hall' },
        },
        {
            path: '/login',
            name: 'login',
            component: LoginView,
        },
        {
            path: '/hall',
            name: 'hall',
            redirect: { name: 'weather' },
            component: HallView,
            children: [
                {
                    path: 'weather',
                    name: 'weather',
                    component: WeatherView,
                },
                {
                    path: 'visitors',
                    name: 'visitors',
                    component: VisitorsView,
                },
                {
                    path: 'goods-and-services',
                    name: 'goods-and-services',
                    component: GoodsAndServicesView,
                },
            ],
        },
        {
            path: '/admin',
            name: 'admin',
            component: AdminView,
            children: [],
        },
    ]
});

export default router;
