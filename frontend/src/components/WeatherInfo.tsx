import { Box, Typography } from "@mui/material";
import { WeatherDto } from "../api/apiTypes";
import {
  DeviceThermostat,
  LocationOn,
  ThermostatAuto,
  WaterDrop,
  WbCloudy,
} from "@mui/icons-material";

interface WeatherInfoProps {
  weather: WeatherDto;
}

const WeatherDetail = ({
  icon,
  label,
  value,
}: {
  icon: React.ReactNode;
  label: string;
  value: string | number;
}) => (
  <Typography sx={{ display: "flex", alignItems: "center", gap: 1 }}>
    {icon}
    {label}: {value}
  </Typography>
);

const WeatherInfo = ({ weather }: WeatherInfoProps) => {
  return (
    <Box mt={3} sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
      <WeatherDetail
        icon={<LocationOn color="primary" />}
        label="City"
        value={weather.city}
      />
      <WeatherDetail
        icon={<DeviceThermostat />}
        label="Temperature"
        value={`${weather.temperature}°C`}
      />
      <WeatherDetail
        icon={<ThermostatAuto />}
        label="Feels Like"
        value={`${weather.feelsLike}°C`}
      />
      <WeatherDetail
        icon={<WbCloudy />}
        label="Description"
        value={weather.description}
      />
      <WeatherDetail
        icon={<WaterDrop />}
        label="Humidity"
        value={`${weather.humidity}%`}
      />
    </Box>
  );
};

export default WeatherInfo;
