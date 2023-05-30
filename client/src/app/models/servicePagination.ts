export interface ServicePagination<T> {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: T;
}