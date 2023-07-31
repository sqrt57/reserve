export interface ShortVisitorDto {
    id: number;
    badgeNumber: string | null;
    name: string | null;
    openDateTime: string;
    closeDateTime: string | null;
    billed: number | null;
    payed: number | null;
    status: string | null;
    openDuration: string | null;
    openBill: number | null;
    closedDuration: string | null;
}

export interface NewVisitorDto {
    badgeNumber: string | null;
    name: string | null;
}
