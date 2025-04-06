import {
  Box,
  CircularProgress,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Typography,
} from "@mui/material";
import "./search.scss";
import { useState } from "react";
import { Search } from "@mui/icons-material";
import httpClient from "../../api/httpClient";
import { WeatherDto } from "../../api/apiTypes";
import { isAxiosError } from "axios";
import WeatherInfo from "../../components/WeatherInfo";

const fetchWeather = async (search: string): Promise<WeatherDto> => {
  const response = await httpClient.get<WeatherDto>(
    `/search/${encodeURIComponent(search)}`,
    {
      withCredentials: true,
    }
  );
  return response.data;
};

const SearchPage = () => {
  const [search, setSearch] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [weather, setWeather] = useState<WeatherDto | null>(null);
  const [loading, setLoading] = useState(false);

  const performSearch = async () => {
    setLoading(true);
    setError(null);
    setWeather(null);

    try {
      const result = await fetchWeather(search);
      setWeather(result);
    } catch (err) {
      if (isAxiosError(err) && err.status === 404) {
        setError(
          "Weather for this city was not found. Please check the spelling or try a different location."
        );
      } else {
        setError("Unkown error");
      }
    } finally {
      setLoading(false);
    }
  };

  const handleKeyDown = (
    event: React.KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (event.key.toLowerCase() === "enter") {
      performSearch();
    }
  };

  const handleSearchClick = () => {
    performSearch();
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        height: "100%",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <FormControl sx={{ m: 1, width: "25ch" }} variant="outlined">
        <InputLabel htmlFor="search">City</InputLabel>
        <OutlinedInput
          id="search"
          endAdornment={
            <InputAdornment position="end">
              <IconButton
                onClick={handleSearchClick}
                edge="end"
                disabled={loading}
              >
                {loading ? <CircularProgress size={24} /> : <Search />}
              </IconButton>
            </InputAdornment>
          }
          label="City"
          onKeyDown={handleKeyDown}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          disabled={loading}
        />
      </FormControl>

      {error && (
        <Typography color="error" mt={2}>
          {error}
        </Typography>
      )}

      {weather && !loading && <WeatherInfo weather={weather} />}
    </Box>
  );
};

export default SearchPage;
