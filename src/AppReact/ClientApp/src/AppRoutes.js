import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import LoginCallback from "./components/LoginCallback";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/login-callback',
    element: <LoginCallback />
  }
];

export default AppRoutes;
