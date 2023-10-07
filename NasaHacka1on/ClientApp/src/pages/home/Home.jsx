import { useNavigate } from "react-router-dom";
import  home  from "../../assets/img/home.svg";

const Home = () => {
  const navigate = useNavigate();

  const handleAddProject = () => {
    navigate("/add-project");
  };

  const handleFindProject = () => {
    navigate("/find-project");
  };

  return (
      <div className="container-xl mx-auto mt-5">
          <div className="row">
              <div className="col-lg-6">
                  <div className="d-flex flex-column mt-5">
                      <h1 className="mb-3">Sharing code matters!</h1>
                      <p>
                          Comunity Code Hub showcases Open Source projects powered by DeVinczi
                          Team.
                      </p>
                      <p>
                          DeVinczi Team helps contributors onboard to projects in no time, and
                          helps maintainers automate and review changes faster - so that we can
                          focus on providing value.
                      </p>
                      <div className="mt-3">
                          <button className="btn btn-outline-primary me-3" onClick={handleAddProject}>
                              Add project
                          </button>
                          <button className="btn btn-outline-primary" onClick={handleFindProject}>
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
