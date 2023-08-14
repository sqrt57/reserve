<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import { Check, Delete, Edit, Message, Search, Star, ArrowUp, ArrowDown, } from '@element-plus/icons-vue'
import type { NewProductDto, ProductDto, UpdateProductDto } from '@/backendDto/product';
import { deleteProduct, getAllProducts, newProduct, updateProduct, updateProductInStock, updateProductsOrder } from '@/dataServices/products';
import EditProductDialog from '@/components/EditProductDialog.vue';
import { ElMessageBox } from 'element-plus';

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

async function deleteClick(row: ProductDto) {
    try {
        await ElMessageBox.confirm(
            'Do you really want to delete "' + row.name + '"?',
            'Warning',
            {
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancel',
                type: 'warning',
            }
        );
    } catch (error) { return; }
    await deleteProduct(row.id);
    await queryNow();
}

async function inStockChange(row: ProductDto) {
    await updateProductInStock(row);
    await queryNow();
}

function isFirst(index: number): boolean {
    return index === 0;
}

function isLast(index: number): boolean {
    return index + 1 === productsData.value?.length;
}

async function upClick(index: number, row: ProductDto) {
    if (!productsData.value) return;
    if (index === 0) return;
    const prevRow = productsData.value[index - 1];
    if (!prevRow) return;
    const dto = [
        { id: row.id, order: prevRow.order, },
        { id: prevRow.id, order: row.order, },
    ];
    await updateProductsOrder(dto);
    await queryNow();
}

async function downClick(index: number, row: ProductDto) {
    if (!productsData.value) return;
    if (index + 1 >= productsData.value.length) return;
    const nextRow = productsData.value?.[index + 1];
    if (!nextRow) return;
    const dto = [
        { id: row.id, order: nextRow.order, },
        { id: nextRow.id, order: row.order, },
    ];
    await updateProductsOrder(dto);
    await queryNow();
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
        <el-table-column prop="inStock" label="In stock">
            <template #default="scope">
                <el-checkbox v-model="scope.row.inStock" size="large" @change="inStockChange(scope.row)" />
            </template>
        </el-table-column>
        <el-table-column label="Operations">
            <template #default="scope">
                <el-button type="primary" :icon="Edit" circle @click="editClick(scope.row)" />
                <el-button type="info" :icon="ArrowUp" circle @click="upClick(scope.$index, scope.row)"
                    :disabled="isFirst(scope.$index)" />
                <el-button type="info" :icon="ArrowDown" circle @click="downClick(scope.$index, scope.row)"
                    :disabled="isLast(scope.$index)" />
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
