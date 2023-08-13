<script setup lang="ts">
import { ref, reactive } from 'vue';
import { ElNotification, type FormInstance, type FormRules } from 'element-plus';
import type { NewProductDto, UpdateProductDto } from '@/backendDto/product';

const emit = defineEmits<{
    (e: 'commitedNew', data: NewProductDto): void
    (e: 'commitedUpdate', data: UpdateProductDto): void
}>();

const formVisible = ref(false);

const title = ref("");

export interface IEditProductForm {
    id?: number;
    name?: string;
    price?: number;
}

const form = reactive<IEditProductForm>({
    id: undefined,
    name: undefined,
    price: undefined,
});

const formRef = ref<FormInstance>();

const rules = reactive<FormRules<IEditProductForm>>({
    name: [
        { required: true, message: 'Please enter product name', trigger: 'blur', },
    ],
    price: [
        { required: true, message: 'Please enter price', trigger: 'blur' },
    ],
});

function showNewForm() {
    title.value = "New product";
    form.id = undefined;
    form.name = undefined;
    form.price = undefined;
    formVisible.value = true;
}

function showUpdateForm(dto: UpdateProductDto) {
    title.value = "Edit product";
    form.id = dto.id;
    form.name = dto.name;
    form.price = dto.price;
    formVisible.value = true;
}

function cancel() {
    formVisible.value = false;
}

async function confirm(formElement: FormInstance | undefined) {
    if (!formElement) return;
    if (!await formElement.validate()) return;
    if (!form.name || !form.price) {
        ElNotification({
            title: 'Form error',
            message: 'Name or price is not available',
            type: 'error',
        })
        return;
    }
    formVisible.value = false;
    if (form.id) {
        emit('commitedUpdate', { id: form.id, name: form.name, price: form.price });
    } else {
        emit('commitedNew', { name: form.name, price: form.price });
    }
}

defineExpose({ showNewForm, showUpdateForm });

</script>

<template>
    <el-dialog v-model="formVisible" :rules="rules" :title="title">
        <el-form :model="form" ref="formRef">
            <el-form-item label="Name" prop="name">
                <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="Price" prop="price">
                <el-input v-model="form.price" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button type="primary" @click="confirm(formRef)">OK</el-button>
                <el-button @click="cancel()">Cancel</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<style>
</style>
