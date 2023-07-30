<script setup lang="ts">
import { ref, onMounted } from 'vue';
import http from '../services/http';
import { getVisitors } from '../dataServices/visitors';

const queryInterval = 10000;

const visitorsData = ref();

async function queryData() {
    try {
        const visitors = await getVisitors();
        visitorsData.value = visitors;
    } catch (error) {
        console.log(error);
    }
}

async function serialQueryData() {
    await queryData();
    setTimeout(serialQueryData, queryInterval);
}

onMounted(async () => {
    serialQueryData()
});

</script>

<template>
    <el-table :data="visitorsData" style="width: 100%">
        <el-table-column prop="badgeNumber" label="Badge" />
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="openDateTime" label="Opened" />
        <el-table-column prop="closeDateTime" label="Closed" />
        <el-table-column prop="openBill" label="Open Bill" />
        <el-table-column prop="billed" label="Bill" />
        <el-table-column prop="payed" label="Payed" />
    </el-table>
</template>

<style>
</style>
