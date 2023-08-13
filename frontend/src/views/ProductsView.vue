<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import type { NewProductDto, ProductDto, UpdateProductDto } from '@/backendDto/product';
import { getAllProducts, newProduct } from '@/dataServices/products';
import EditProductDialog from '@/components/EditProductDialog.vue';

onMounted(() => {
    queryNow();
});

const productsData = ref<ProductDto[]>();
const editProductDialogRef = ref<typeof EditProductDialog | undefined>();

async function queryNow() {
    productsData.value = await getAllProducts();
}

function addProduct() {
    editProductDialogRef.value?.showNewForm();
}

async function newProductCommited(dto: NewProductDto) {
    await newProduct(dto);
    await queryNow();
}

async function updateProductCommited(dto: UpdateProductDto) {
}


</script>

<template>
    <div class="toolbar">
        <el-button type="primary" @click="queryNow()">Refresh</el-button>
        <el-button type="success" @click="addProduct()">New</el-button>
    </div>
    <el-table :data="productsData" style="width: 100%">
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="price" label="Price" />
        <el-table-column prop="inStock" label="In stock" />
    </el-table>
    <EditProductDialog ref="editProductDialogRef" @commitedNew="newProductCommited" @commitedUpdate="updateProductCommited" />
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
