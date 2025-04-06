import { Box, CircularProgress, Pagination, Typography } from "@mui/material";
import WeatherQueryCard from "../../components/WeatherQueryCard";
import { useHistoryQuery } from "../../hooks/useHistoryQuery";
import { useState } from "react";

const HistoryPage = () => {
  const [page, setPage] = useState(1);
  const { data, isLoading, isError } = useHistoryQuery(page);

  const handlePageChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  if (isLoading || !data) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center">
        <CircularProgress />
      </Box>
    );
  }

  if (isError) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center">
        <Typography variant="h6" color="error">
          Something went wrong. Please try again later.
        </Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ display: "flex", flexDirection: "column", width: "100%" }}>
      {data.items.length > 0 ? (
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 3,
            width: "100%",
          }}
        >
          {data.items.map((query, index) => (
            <Box key={index} sx={{ display: "flex", justifyContent: "center" }}>
              <WeatherQueryCard weatherQuery={query} />
            </Box>
          ))}
        </Box>
      ) : (
        <Typography variant="h6" sx={{ textAlign: "center" }}>
          History is empty
        </Typography>
      )}

      <Box display="flex" justifyContent="center" mt={2}>
        <Pagination
          count={Math.ceil(data?.totalCount / data.pageSize || 0)}
          page={page}
          onChange={handlePageChange}
          color="primary"
        />
      </Box>
    </Box>
  );
};
export default HistoryPage;
