<script setup lang="ts">
import { ref, onMounted, reactive, onUnmounted } from 'vue';
import { createNewVisitor } from '@/dataServices/visitors';
import { type NewVisitorDto, type ShortVisitorDto } from '../backendDto/visitor';
import NewVisitorDialog from '@/components/NewVisitorDialog.vue'
import { visitorsData, startPoll, stopPoll, queryNow } from '@/dataServices/pollVisitors';

onMounted(() => {
    startPoll();
});

onUnmounted(() => {
    stopPoll();
});


const newVisitorDialogRef = ref<typeof NewVisitorDialog | null>(null);

function newVisitor() {
    newVisitorDialogRef?.value?.showForm();
}

async function newVisitorConfirm(data: NewVisitorDto) {
    await createNewVisitor(data);
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
        <el-table-column prop="payed" label="Payed" />
    </el-table>

    <NewVisitorDialog ref="newVisitorDialogRef" @commited="newVisitorConfirm" />
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
