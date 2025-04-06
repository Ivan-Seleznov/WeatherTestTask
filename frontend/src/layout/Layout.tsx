import { ToggleButton, ToggleButtonGroup } from "@mui/material";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import { useCallback, useMemo } from "react";
import "./layout.scss";

const Layout = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const currentPage = useMemo(() => {
    return location.pathname.includes("history") ? "history" : "search";
  }, [location.pathname]);

  const handleChange = useCallback(
    (_: React.MouseEvent<HTMLElement>, newPage?: string) => {
      if (!newPage || newPage === currentPage) {
        return;
      }

      navigate(`/${newPage}`);
    },
    [navigate, currentPage]
  );

  return (
    <div className="app-container">
      <ToggleButtonGroup value={currentPage} exclusive onChange={handleChange}>
        <ToggleButton value="search" aria-label="search">
          Search
        </ToggleButton>
        <ToggleButton value="history" aria-label="history">
          History
        </ToggleButton>
      </ToggleButtonGroup>

      <div className="content">
        <Outlet />
      </div>
    </div>
  );
};

export default Layout;
