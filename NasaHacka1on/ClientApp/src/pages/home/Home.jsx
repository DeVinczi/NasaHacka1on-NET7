import { useNavigate } from "react-router-dom";
import home from "../../assets/img/home.svg";
import './home.css'

const Home = () => {
  const navigate = useNavigate();

  const handleAddProject = () => {
    navigate("/add-project");
  };

  const handleFindProject = () => {
    navigate("/find-project");
  };

  return ( 
      <div className="container-xl mx-auto mt-2 ">
          <div className="row justify-content-center align-items-center items-align-home">
              <div className="col-lg-6 h-75 border-1 shadow home-container">
                  <div className="d-flex flex-column mt-5 mb-5 align-items-center px-3">
                      <h1 className="">Welcome to</h1><h1 className="mb-4"> Community Code Hub</h1>
                      <p className="text-center mb-0 home-color">Sharing Code for a Better Tomorrow!</p>
                      <p className="text-center home-color">
                          Discover and Contribute to Open Source Projects.
                      </p>
                      <p className="text-center">
                          Community Code Hub is your gateway to the world of open-source
                          software. We believe in the power of collaboration and the value
                          of open-source projects. Our platform connects
                          developers, contributors, and users, fostering a
                          community-driven approach towards software development.
                      </p>
                      <div className="mt-3">
                          <button className="btn btn btn-light me-3 button-home" onClick={handleAddProject}>
                              Add project
                          </button>
                          <button className="btn btn btn-light button-home" onClick={handleFindProject}>
                              Find project
                          </button>
                      </div>
                  </div>
              </div>
              <div className="col-lg-6">
                  <img src={home} alt="home" className="img-fluid" />
              </div>
          </div>
      </div>

  );
};

export default Home;
