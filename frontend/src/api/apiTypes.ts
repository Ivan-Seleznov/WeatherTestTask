export interface WeatherQueryEntityDto {
  dateRequested: Date;
  city: string;
  temperature: number;
  weatherDescription: string;
}
export interface WeatherDto {
  city: string;
  temperature: number;
  feelsLike: number;
  description: string;
  humidity: number;
}
export interface PagedList<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
