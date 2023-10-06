import { useNavigate } from "react-router-dom";

const Home = () => {
  const navigate = useNavigate();

  const handleAddProject = () => {
    navigate("/add-project");
  };

  const handleFindProject = () => {
    navigate("/find-project");
  };

  return (
    <div className="container-sm mx-auto d-flex flex-column justify-content-center align-items-center text-center mt-5">
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
        <button
          className="btn btn-outline-primary me-3"
          onClick={handleAddProject}
        >
          Add project
        </button>
        <button className="btn btn-outline-primary" onClick={handleFindProject}>
          Find project
        </button>
      </div>
    </div>
  );
};

export default Home;
