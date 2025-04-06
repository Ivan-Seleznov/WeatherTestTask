import { Card, CardContent, Typography } from "@mui/material";
import { WeatherQueryEntityDto } from "../api/apiTypes";

interface WeatherQueryCardProps {
  weatherQuery: WeatherQueryEntityDto;
}

const WeatherQueryCard = ({ weatherQuery }: WeatherQueryCardProps) => {
  return (
    <Card
      elevation={3}
      sx={{
        width: "100%",
        maxWidth: "600px",
        margin: "0 auto",
        boxSizing: "border-box",
      }}
    >
      <CardContent>
        <Typography variant="h6" color="primary">
          {weatherQuery.city}
        </Typography>
        <Typography variant="body2" color="textSecondary">
          {new Date(weatherQuery.dateRequested).toLocaleDateString()}
        </Typography>
        <Typography variant="body1" sx={{ marginTop: 2 }}>
          Temperature: {weatherQuery.temperature}°C
        </Typography>
        <Typography variant="body2" color="textSecondary">
          {weatherQuery.weatherDescription}
        </Typography>
      </CardContent>
    </Card>
  );
};

export default WeatherQueryCard;
