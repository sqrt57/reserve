import type { ProductDto } from '@/backendDto/product';
import http from '../services/http';

export async function getAllProducts(): Promise<ProductDto[]> {
    return await http.get('products/all');
}
