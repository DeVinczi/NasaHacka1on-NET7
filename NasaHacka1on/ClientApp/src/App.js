import { createBrowserRouter, RouterProvider } from "react-router-dom";

import Contribute from "./pages/contribute/Contribute";
import GetStarted from "./pages/get-started/GetStarted";
import Home from "./pages/home/Home";
import Projects from "./pages/projects/Projects";
import Root from "./pages/root/Root";
import NotFound from "./pages/not-found/NotFound";
import Error from "./pages/Error/Error";
import Signup from "./pages/auth/Signup";
import Login from "./pages/auth/Login";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <NotFound />,
    children: [
      {
        path: "/",
        element: <Home />,
      },
      {
        path: "/projects",
        element: <Projects />,
        errorElement: <Error />,
      },
      {
        path: "/contribute",
        element: <Contribute />,
        errorElement: <Error />,
      },
      {
        path: "/get-started",
        element: <GetStarted />,
        errorElement: <Error />,
      },
      {
        path: "/sign-up",
        element: <Signup />,
        errorElement: <Error />,
      },
      {
        path: "/log-in",
        element: <Login />,
        errorElement: <Error />,
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
