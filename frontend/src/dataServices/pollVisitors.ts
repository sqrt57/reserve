import http from '../services/http';
import type { ShortVisitorDto } from '../backendDto/visitor';
import { ref } from 'vue';

const queryInterval = 10000;

export const visitorsData = ref<ShortVisitorDto[]>();

let polling = false;
let intervalId: number | null = null;

export function startPoll() {
    polling = true;
    serialQueryData();
}

export function stopPoll() {
    polling = false;
    if (intervalId)
        clearInterval(intervalId);
}

export function queryNow() {
    if (intervalId)
        clearInterval(intervalId);
    serialQueryData();
}

async function queryData() {
    try {
        const visitors = await getVisitors();
        visitorsData.value = visitors;
    } catch (error) {
        console.log(error);
    }
}

async function serialQueryData() {
    intervalId = null;
    await queryData();
    if (!polling)
        return;
    if (!intervalId)
        intervalId = setTimeout(serialQueryData, queryInterval);
}
export async function getVisitors(): Promise<ShortVisitorDto[]> {
    const result = await http.get('visitors');
    return result.data;
}
