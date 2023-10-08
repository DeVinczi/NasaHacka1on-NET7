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
import FindProject from "./pages/find-project/FindProject";
import AddProject from "./pages/add-project/AddProject";
import SignOut from "./pages/sign-out/sign-out";
import MyAccount from "./pages/account/MyAccount"

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
                element: <AddProject />,
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
            {
                path: "/find-project",
                element: <FindProject />,
                errorElement: <Error />,
            },
            {
                path: "/add-project",
                element: <AddProject />,
                errorElement: <Error />,
            },
            {
                path: "/signout",
                element: <SignOut />,
                errorElement: <Error />,
            },
            {
                path: "/my-account",
                element: <MyAccount />,
                errorElement: <Error />
            }
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
