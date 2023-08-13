<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import { Check, Delete, Edit, Message, Search, Star, ArrowUp, ArrowDown, } from '@element-plus/icons-vue'
import type { NewProductDto, ProductDto, UpdateProductDto } from '@/backendDto/product';
import { getAllProducts, newProduct, updateProduct } from '@/dataServices/products';
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
    await updateProduct(dto);
    await queryNow();
}

function editClick(row: ProductDto) {
    editProductDialogRef.value?.showUpdateForm(row);
}

function deleteClick(row: ProductDto) {
}

function upClick(row: ProductDto) {
}

function downClick(row: ProductDto) {
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
        <el-table-column label="Operations">
            <template #default="scope">
                <el-button type="primary" :icon="Edit" circle @click="editClick(scope.row)" />
                <el-button type="info" :icon="ArrowUp" circle @click="upClick(scope.row)" />
                <el-button type="info" :icon="ArrowDown" circle @click="downClick(scope.row)" />
                <el-button type="danger" :icon="Delete" circle @click="deleteClick(scope.row)" />
            </template>
        </el-table-column>
    </el-table>
    <EditProductDialog ref="editProductDialogRef" @commitedNew="newProductCommited"
        @commitedUpdate="updateProductCommited" />
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
