<script setup lang="ts">
import { ref, reactive } from 'vue';
import type { FormInstance } from 'element-plus';
import type { NewVisitorDto } from '@/backendDto/visitor';

const emit = defineEmits<{
    (e: 'commited', data: NewVisitorDto) : void
}>();

const formVisible = ref(false);

interface IForm {
    badgeNumber: string;
    name?: string;
}

const form = reactive<NewVisitorDto>({
    badgeNumber: null,
    name: null,
});

const formRef = ref<FormInstance>();

function showForm() {
    form.badgeNumber = null;
    form.name = null;
    formVisible.value = true;
}

function cancel() {
    formVisible.value = false;
}

function confirm() {
    formVisible.value = false;
    emit('commited', form);
}

defineExpose({ showForm, });

</script>

<template>
    <el-dialog v-model="formVisible" title="New visitor">
        <el-form :model="form" ref="formRef">
            <el-form-item label="Badge number" prop="badgeNumber">
                <el-input v-model="form.badgeNumber" />
            </el-form-item>
            <el-form-item label="Name" prop="name">
                <el-input v-model="form.name" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="cancel()">Cancel</el-button>
                <el-button type="primary" @click="confirm()">Confirm</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<style>
</style>
