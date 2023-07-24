<script setup lang="ts">
import { onMounted, ref } from 'vue';
import axios from 'axios';
import { getApiUrl } from '@/services/path';
import router from '@/router';

const weatherData = ref();

function bytesToBase64(bytes: any) {
    const binString = Array.from(bytes, (x) => String.fromCodePoint(x)).join("");
    return btoa(binString);
}

function getHeaders() {
    const sessionId = window.localStorage["sessionId"];
    const tokenUtf8 = JSON.stringify({ sessionId: sessionId });
    const token = bytesToBase64(new TextEncoder().encode(tokenUtf8));
    return { headers: { Authorization: "Bearer " + token, } };
}

onMounted(() => {
    const request = axios.get(getApiUrl() + 'WeatherForecast', getHeaders())
        .then(response => {
            weatherData.value = response.data;
        });
});

const onLogout = async () => {
    const result = await axios.post(getApiUrl() + 'account/logout', null, getHeaders());
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
