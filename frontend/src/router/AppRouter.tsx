import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import SearchPage from "../pages/SearchPage/SearchPage";
import HistoryPage from "../pages/HistoryPage/HistoryPage";
import Layout from "../layout/Layout";

const router = createBrowserRouter([
  {
    element: <Layout />,
    //errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <Navigate to="/search" />,
      },
      {
        path: "/search",
        element: <SearchPage />,
      },
      {
        path: "/history",
        element: <HistoryPage />,
      },
    ],
  },
]);

export const AppRouter = () => {
  return <RouterProvider router={router} />;
};
