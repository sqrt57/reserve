import axios from 'axios';
import router from '../router';

const apiUrl = "/api/";

export const rawHttp = axios.create({
    baseURL: apiUrl,
    headers: {
        Accept: "application/json",
    },
});

export const http = axios.create({
    baseURL: apiUrl,
    headers: {
        Accept: "application/json",
    },
});

http.interceptors.response.use(
    response => Promise.resolve(response),
    error => {
        if (error.response.status === 401) {
            router.push({ name: 'login', });
        }
        return Promise.reject(error);
    }
);

export default http;
