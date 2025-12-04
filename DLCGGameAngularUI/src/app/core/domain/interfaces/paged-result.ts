export interface PagedResult<T> {
    items: T[];
  totalItemCount: number;
  currentPage: number;
  pageSize: number;
  totalPages: number;
}
