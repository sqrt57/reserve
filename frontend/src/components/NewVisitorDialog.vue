<script setup lang="ts">
import { ref, reactive } from 'vue';
import type { FormInstance } from 'element-plus';
import type { TariffDto } from '@/backendDto/visitor';

const emit = defineEmits<{
    (e: 'commited', data: INewVisitorForm): void
}>();

const formVisible = ref(false);
const formTariffs = ref<TariffDto[]>();

export interface INewVisitorForm {
    badgeNumber: string | null;
    name: string | null;
    tariffId: number | null;
}

const form = reactive<INewVisitorForm>({
    badgeNumber: null,
    name: null,
    tariffId: null,
});

const formRef = ref<FormInstance>();

function showForm(tariffs: TariffDto[]) {
    tariffs.sort((a, b) => a.order - b.order);
    formTariffs.value = tariffs;
    form.badgeNumber = null;
    form.name = null;
    form.tariffId = tariffs[0].id;
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
            <el-form-item label="Tariff" prop="tariffId">
                <el-select v-model="form.tariffId" class="m-2" placeholder="Select" size="large">
                    <el-option v-for="item in formTariffs" :key="item.id" :label="item.name" :value="item.id" />
                </el-select>
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
