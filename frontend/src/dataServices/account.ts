import router from '../router';
import http from '../services/http';

export async function logout() {
    const result = await http.post('account/logout');
    router.push({ name: 'login' });
}
