export interface ApiResponse<T = any> {
  totalCount: number;
  items: T[];
}
