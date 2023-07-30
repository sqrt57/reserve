import http from '../services/http';
import { type ShortVisitorDto } from '../backendDto/visitor';

export async function getVisitors() : Promise<ShortVisitorDto[]> {
    const result = await http.get('visitors');
    return result.data;
}
