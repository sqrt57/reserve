import router from '../router';
import http from './http';

export async function logout() {
    const result = await http.post('account/logout');
    router.push({ name: 'login' });
}
