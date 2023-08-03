import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import HallView from '../views/HallView.vue';
import VisitorsView from '../views/VisitorsView.vue';
import VisitorsHistoryView from '../views/VisitorsHistoryView.vue';
import AdminView from '../views/AdminView.vue';
import ProductsView from '../views/ProductsView.vue';

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
            redirect: { name: 'visitors' },
            component: HallView,
            children: [
                {
                    path: 'visitors',
                    name: 'visitors',
                    component: VisitorsView,
                },
                {
                    path: 'visitors-history',
                    name: 'visitors-history',
                    component: VisitorsHistoryView,
                },
                {
                    path: 'products',
                    name: 'products',
                    component: ProductsView,
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
