import type { NewProductDto, ProductDto } from '@/backendDto/product';
import http from '../services/http';

export async function getAllProducts(): Promise<ProductDto[]> {
    return (await http.get('products/all')).data;
}

export async function newProduct(dto: NewProductDto): Promise<void> {
    return await http.post('products/new', dto);
}
