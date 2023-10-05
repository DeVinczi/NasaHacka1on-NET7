import { NavLink } from "react-router-dom";
import logo from "../../assets/img/logo.png";

const Navigation = () => {
  return (
    <header>
      <nav className="navbar navbar-expand-lg navbar-dark effect7">
        <div className="container-xxl">
          <NavLink className="navbar-brand col-md-5" to="/">
            <img src={logo} height="22" />
          </NavLink>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item mt-2 mt-lg-0">
                <NavLink
                  className={({ isActive }) =>
                    isActive ? "active nav-link mx-2" : "nav-link mx-2"
                  }
                  to="/projects"
                >
                  Projects
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink
                  className={({ isActive }) =>
                    isActive ? "active nav-link mx-2" : "nav-link mx-2"
                  }
                  to="/contribute"
                >
                  Contribute
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink
                  className={({ isActive }) =>
                    isActive ? "active nav-link mx-2" : "nav-link mx-2"
                  }
                  to="/get-started"
                >
                  Get Started
                </NavLink>
              </li>
            </ul>
            <ul className="navbar-nav mb-2 mb-lg-0">
              <li className="nav-item borderline">
                <NavLink className="nav-link" to="/log-in">
                  Log in
                </NavLink>
              </li>
            </ul>
            <button type="button" className="btn btn-light ms-lg-2">
              <NavLink className="nav-link text-dark" to="/sign-up">
                Sign up
              </NavLink>
            </button>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Navigation;
