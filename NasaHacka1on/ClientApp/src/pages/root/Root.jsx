import { Outlet } from "react-router-dom";
import Footer from "./Footer";
import Navigation from "./Navigation";

const Root = () => {
  return (
    <>
      <Navigation />
      <main>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default Root;
