<script setup lang="ts">
import { ref, onMounted, reactive } from 'vue';
import { getVisitors, createNewVisitor } from '../dataServices/visitors';
import { type ShortVisitorDto } from '../backendDto/visitor';
import type { FormInstance } from 'element-plus';

const queryInterval = 10000;

const visitorsData = ref<ShortVisitorDto>();
const newVisitorFormVisible = ref(false);

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

interface NewVistorForm {
    badgeNumber?: string;
    name?: string;
}

const newVistorForm = reactive<NewVistorForm>({
    badgeNumber: undefined,
    name: undefined,
});

const newVistorFormRef = ref<FormInstance>();

function newVisitor() {
    newVistorForm.badgeNumber = undefined;
    newVistorForm.name = undefined;
    newVisitorFormVisible.value = true;
}

function newVisitorCancel() {
    newVisitorFormVisible.value = false;
}

async function newVisitorConfirm() {
    newVisitorFormVisible.value = false;
    await createNewVisitor(newVistorForm);
}

</script>

<template>
    <div class="toolbar">
        <el-button type="primary" @click="newVisitor()">New visitor</el-button>
    </div>
    <el-table :data="visitorsData" style="width: 100%">
        <el-table-column prop="badgeNumber" label="Badge" />
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="openDateTime" label="Opened" />
        <el-table-column prop="closeDateTime" label="Closed" />
        <el-table-column prop="openBill" label="Open Bill" />
        <el-table-column prop="billed" label="Bill" />
        <el-table-column prop="payed" label="Payed" />
    </el-table>

    <el-dialog v-model="newVisitorFormVisible" title="New visitor">
        <el-form :model="newVistorForm" ref="newVistorFormRef">
            <el-form-item label="Badge number" prop="badgeNumber">
                <el-input v-model="newVistorForm.badgeNumber" />
            </el-form-item>
            <el-form-item label="Name" prop="name">
                <el-input v-model="newVistorForm.name" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="newVisitorCancel()">Cancel</el-button>
                <el-button type="primary" @click="newVisitorConfirm()">Confirm</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
