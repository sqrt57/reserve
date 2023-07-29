<script setup lang="ts">
import { onMounted, ref } from 'vue';
import axios, { AxiosError } from 'axios';
import http from '../services/http';
import { useRouter } from 'vue-router';

const router = useRouter();
const weatherData = ref();

onMounted(async () => {
    try {
        const response = await http.get('WeatherForecast');
        weatherData.value = response.data;
    } catch (error) {
        console.log(error);
    }
});

const onLogout = async () => {
    const result = await http.post('account/logout');
    router.push({ name: 'login', });
};

</script>

<template>
    <main>
        <el-table :data="weatherData" style="width: 100%">
            <el-table-column prop="date" label="Date" width="180" />
            <el-table-column prop="temperatureC" label="C" width="100" />
            <el-table-column prop="summary" label="Summary" />
        </el-table>
        <a href="javascript:" @click="onLogout()">Logout</a>
    </main>
</template>
