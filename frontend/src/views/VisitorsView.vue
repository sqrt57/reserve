<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import { closeVisitor, createNewVisitor, paidVisitor } from '@/dataServices/visitors';
import { type ShortVisitorDto } from '../backendDto/visitor';
import NewVisitorDialog, { type INewVisitorForm } from '@/components/NewVisitorDialog.vue'
import PayDialog, { type IPayForm } from '@/components/PayDialog.vue'
import { visitorsData, startPoll, stopPoll, queryNow } from '@/dataServices/pollVisitors';

// Polling visitors

onMounted(() => {
    startPoll();
});

onUnmounted(() => {
    stopPoll();
});

// Create new visitor

const newVisitorDialogRef = ref<typeof NewVisitorDialog | null>(null);

function newVisitor() {
    newVisitorDialogRef.value?.showForm();
}

async function newVisitorConfirm(data: INewVisitorForm) {
    await createNewVisitor(data);
    queryNow();
}

// Close visitor

async function close(row: ShortVisitorDto) {
    await closeVisitor({ id: row.id, });
    queryNow();
}

// Process payment

const payDialogRef = ref<typeof PayDialog | null>(null);

function pay(row: ShortVisitorDto) {
    payDialogRef.value?.showForm({ id: row.id, paid: row.billed, });
}

async function payConfirm(data: IPayForm) {
    await paidVisitor({ id: data.id, paid: data.paid, });
    queryNow();
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
        <el-table-column prop="paid" label="Paid" />
        <el-table-column label="Operations">
            <template #default="scope">
                <el-button size="small" type="primary" @click="close(scope.row)">Close</el-button>
                <el-button size="small" type="success" @click="pay(scope.row)">Paid</el-button>
            </template>
        </el-table-column>
    </el-table>

    <NewVisitorDialog ref="newVisitorDialogRef" @commited="newVisitorConfirm" />
    <PayDialog ref="payDialogRef" @commited="payConfirm" />
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
