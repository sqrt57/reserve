<script setup lang="ts">
import { ref, onMounted, onUnmounted, computed } from 'vue';
import { DateTime, Duration } from 'luxon';
import { closeVisitor, createNewVisitor, paidVisitor } from '@/dataServices/visitors';
import { type ShortVisitorDto } from '../backendDto/visitor';
import NewVisitorDialog, { type INewVisitorForm } from '@/components/NewVisitorDialog.vue'
import PayDialog, { type IPayForm } from '@/components/PayDialog.vue'
import { visitorsData, startPoll, stopPoll, queryNow, tariffsData } from '@/dataServices/pollVisitors';

// Polling visitors

onMounted(() => {
    startPoll();
});

onUnmounted(() => {
    stopPoll();
});

// Visitors list

interface IVisitor {
    dto: ShortVisitorDto;
    badgeNumber: string;
    name: string;
    tariff: string;
    openDateTime: string;
    closeDateTime: string;
    duration: string;
    billed: string;
    paid: string;
}

const visitors = computed(() => visitorsData.value?.map(GetVisitorRow));

function GetVisitorRow(dto: ShortVisitorDto): IVisitor {
    const visitor: IVisitor = {
        dto: dto,
        badgeNumber: dto.badgeNumber ?? "",
        name: dto.name ?? "",
        tariff: formatTariff(dto.tariffId),
        openDateTime: formatDateTime(dto.openDateTime),
        closeDateTime: formatDateTime(dto.closeDateTime),
        duration: formatDurationSeconds(dto.durationSeconds),
        billed: formatMoney(dto.billed),
        paid: formatMoney(dto.paid),
    };
    return visitor;
}

function formatDateTime(source: string | null): string {
    if (!source) return "";
    const date = DateTime.fromISO(source);
    const now = DateTime.now();
    if (date.year === now.year && date.month === now.month && date.day === now.day) {
        return date.toFormat("H:mm");
    } else {
        return date.toFormat("y-MM-dd H:mm");
    }
}

function formatDurationSeconds(source: number | null): string {
    if (!source) return "";
    return Duration.fromMillis(source * 1000).toFormat("h'h' mm'm'");
}

function formatMoney(source: number | null): string {
    if (!source) return "";
    return Math.floor(source).toString();
}

function formatTariff(tariffId: number): string {
    return tariffsData.value?.find(t => t.id === tariffId)?.name ?? "";
}

// Create new visitor

const newVisitorDialogRef = ref<typeof NewVisitorDialog | null>(null);

function newVisitor() {
    newVisitorDialogRef.value?.showForm(tariffsData.value);
}

async function newVisitorConfirm(formData: INewVisitorForm) {
    const data = {
        badgeNumber: formData.badgeNumber,
        name: formData.name,
        tariffId: formData.tariffId ?? 0,
    };
    await createNewVisitor(data);
    queryNow();
}

// Close visitor

async function close(row: IVisitor) {
    await closeVisitor({ id: row.dto.id, });
    queryNow();
}

// Process payment

const payDialogRef = ref<typeof PayDialog | null>(null);

function pay(row: IVisitor) {
    payDialogRef.value?.showForm({ id: row.dto.id, paid: row.billed, });
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
    <el-table :data="visitors" style="width: 100%">
        <el-table-column prop="badgeNumber" label="Badge" />
        <el-table-column prop="name" label="Name" />
        <el-table-column prop="tariff" label="Tariff" />
        <el-table-column prop="openDateTime" label="Opened" />
        <el-table-column prop="closeDateTime" label="Closed" />
        <el-table-column prop="duration" label="Time" />
        <el-table-column prop="billed" label="Bill" />
        <el-table-column prop="paid" label="Paid" />
        <el-table-column label="Operations">
            <template #default="scope">
                <el-button size="small" type="primary" @click="close(scope.row)">Close</el-button>
                <el-button size="small" type="success" @click="pay(scope.row)">Paid</el-button>
            </template>
        </el-table-column>
    </el-table>

    <NewVisitorDialog ref="newVisitorDialogRef" @commited="newVisitorConfirm" tariffs="" />
    <PayDialog ref="payDialogRef" @commited="payConfirm" />
</template>

<style>
.toolbar {
    margin: 10px 0;
    display: flex;
    flex-direction: row;
}
</style>
