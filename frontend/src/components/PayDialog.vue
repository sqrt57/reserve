<script setup lang="ts">
import { ref, reactive } from 'vue';
import type { FormInstance } from 'element-plus';

const emit = defineEmits<{
    (e: 'commited', data: IPayForm) : void
}>();

const formVisible = ref(false);

export interface IPayForm {
    id: number,
    paid: number;
}

const form = reactive<IPayForm>({
    id: 0,
    paid: 0,
});

const formRef = ref<FormInstance>();

function showForm(initialValues: IPayForm) {
    form.id = initialValues.id;
    form.paid = initialValues.paid;
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
    <el-dialog v-model="formVisible" title="Visitor payment">
        <el-form :model="form" ref="formRef">
            <el-form-item label="Paid" prop="paid">
                <el-input v-model="form.paid" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button type="primary" @click="confirm()">OK</el-button>
                <el-button @click="cancel()">Cancel</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<style>
</style>
