import http from '../services/http';
import type { NewVisitorDto } from '../backendDto/visitor';

export async function createNewVisitor(visitor: NewVisitorDto) {
    await http.post('visitors/new', visitor);
}
