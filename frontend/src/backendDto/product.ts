export interface ProductDto {
    id: number;
    createdDateTime: string;
    createdByUserId: number;
    order: number;
    name: string;
    price: number;
    inStock: boolean;
}

export interface NewProductDto {
    name: string;
    price: number;
}

export interface UpdateProductDto {
    id: number;
    name: string;
    price: number;
}

export interface UpdateProductInStockDto {
    id: number;
    inStock: boolean;
}

export interface UpdateProductOrderDto {
    id: number;
    order: number;
}
