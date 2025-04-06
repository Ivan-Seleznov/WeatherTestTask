import { useQuery } from "@tanstack/react-query";
import { PagedList, WeatherQueryEntityDto } from "../api/apiTypes";
import httpClient from "../api/httpClient";

const DEFAULT_PAGE_SIZE = 5;

const fetchHistory = async (page: number, signal: AbortSignal) => {
  const { data } = await httpClient.get<PagedList<WeatherQueryEntityDto>>(
    `/history?page=${page}&pageSize=${DEFAULT_PAGE_SIZE}`,
    {
      signal: signal,
      withCredentials: true,
    }
  );

  return data;
};

export const useHistoryQuery = (page: number) => {
  return useQuery<PagedList<WeatherQueryEntityDto>>({
    queryKey: ["history", page],
    queryFn: async ({ signal }) => await fetchHistory(page, signal),
    enabled: true,
    refetchOnWindowFocus: true,
    staleTime: 0,
  });
};
