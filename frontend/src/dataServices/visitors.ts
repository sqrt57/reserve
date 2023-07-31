import http from '../services/http';
import type { CloseVisitorDto, NewVisitorDto, PaidVisitorDto } from '../backendDto/visitor';

export async function createNewVisitor(data: NewVisitorDto) {
    await http.post('visitors/new', data);
}

export async function closeVisitor(data: CloseVisitorDto) {
    await http.post('visitors/close', data);
}

export async function paidVisitor(data: PaidVisitorDto) {
    await http.post('visitors/paid', data);
}

