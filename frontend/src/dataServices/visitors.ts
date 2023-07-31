import http from '../services/http';
import type { NewVisitorDto, ShortVisitorDto } from '../backendDto/visitor';

export async function getVisitors() : Promise<ShortVisitorDto[]> {
    const result = await http.get('visitors');
    return result.data;
}

export async function createNewVisitor(visitor: NewVisitorDto) {
    await http.post('visitors/new', visitor);
}
