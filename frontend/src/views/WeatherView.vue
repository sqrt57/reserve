<template>
    <main>
        <el-table :data="weatherData" style="width: 100%">
            <el-table-column prop="date" label="Date" width="180" />
            <el-table-column prop="temperatureC" label="C" width="100" />
            <el-table-column prop="summary" label="Summary" />
        </el-table>
    </main>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import axios from 'axios';

const weatherData = ref();

onMounted(() => {
    const request = axios.get('api/WeatherForecast')
        .then(response => {
            weatherData.value = response.data;
        });
});

</script>
