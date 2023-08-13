import type { NewProductDto, ProductDto, UpdateProductDto, UpdateProductInStockDto, UpdateProductOrderDto } from '@/backendDto/product';
import http from '../services/http';

export async function getAllProducts(): Promise<ProductDto[]> {
    return (await http.get('products/all')).data;
}

export async function newProduct(dto: NewProductDto): Promise<void> {
    return await http.post('products/new', dto);
}

export async function updateProduct(dto: UpdateProductDto): Promise<void> {
    return await http.post('products/update', dto);
}

export async function deleteProduct(id: number): Promise<void> {
    return await http.post('products/delete', { id: id });
}

export async function updateProductInStock(dto: UpdateProductInStockDto): Promise<void> {
    return await http.post('products/update-in-stock', dto);
}

export async function updateProductsOrder(dto: UpdateProductOrderDto[]): Promise<void> {
    return await http.post('products/update-order', dto);
}
