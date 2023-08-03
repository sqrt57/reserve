<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import type { ProductDto } from '@/backendDto/product';
import { getAllProducts } from '@/dataServices/products';

onMounted(() => {
    queryNow();
});

const productsData = ref<ProductDto[]>();

async function queryNow() {
    productsData.value = await getAllProducts();
}

</script>

<template>
    <div class="toolbar">
        <el-button type="primary" @click="queryNow()">Refresh</el-button>
    </div>
    <el-table :data="productsData" style="width: 100%">
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="price" label="Price" />
        <el-table-column prop="inStock" label="In stock" />
    </el-table>
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
