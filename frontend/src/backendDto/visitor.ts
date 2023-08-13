export interface ShortVisitorDto {
    id: number;
    badgeNumber: string | null;
    name: string | null;
    tariffId: number;
    openDateTime: string;
    closeDateTime: string | null;
    billed: number | null;
    paid: number | null;
    status: string | null;
    durationSeconds: number | null;
}

export interface TariffDto {
    id: number;
    name: string;
    order: number;
    firstHour: number;
    secondHour: number | null;
    thirdHour: number | null;
    fourthHour: number | null;
    maxTimeBill: number | null;
}

export interface NewVisitorDto {
    badgeNumber: string | null;
    name: string | null;
    tariffId: number;
}

export interface CloseVisitorDto {
    id: number;
}

export interface PaidVisitorDto {
    id: number;
    paid: number;
}

export interface VisitorsIndexDto {
    visitors: ShortVisitorDto[];
    tariffs: TariffDto[];
}
