<script setup lang="ts">
import { onMounted, ref } from 'vue';
import axios from 'axios';
import { getApiUrl } from '@/services/path';
import router from '@/router';

const weatherData = ref();

onMounted(() => {
    const request = axios.get(getApiUrl() + 'WeatherForecast')
        .then(response => {
            weatherData.value = response.data;
        });
});

const onLogout = async () => {
    const result = await axios.post(getApiUrl() + 'account/logout');
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
        <a href="javascript:void;" @click="onLogout()">Logout</a>
    </main>
</template>
