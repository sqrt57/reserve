import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import TestView from '../views/TestView.vue';
import HallView from '../views/HallView.vue';
import VisitorsView from '../views/VisitorsView.vue';
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
            path: '/test',
            name: 'test',
            component: TestView,
        },
        {
            path: '/hall',
            name: 'hall',
            redirect: { name: 'visitors' },
            component: HallView,
            children: [
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
