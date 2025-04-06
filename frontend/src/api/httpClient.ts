import axios, { AxiosInstance } from "axios";
const API_URL = import.meta.env.VITE_API_URL ?? "";

export const configureHttpClient = (client: AxiosInstance) => {
  console.log("API_URL = ", API_URL);
  client.defaults.baseURL = API_URL;
};

const httpClient = axios.create();
configureHttpClient(httpClient);

export default httpClient;
