const AuthWrapper = ({ children }) => {
  return (
    <div className="auth-container mx-auto mt-3 py-3 px-1 border border-1 shadow">
      <div className="d-flex flex-column align-items-center">
        <button className="btn btn-outline-dark border-2 col-11 col-md-10 col-xxl-7  d-flex align-items-center justify-content-center my-3">
          <i className="bi bi-facebook" style={{ fontSize: "1.4em" }}></i>
          <span className="ms-2">Continue using Facebook account</span>
        </button>

        <button className="btn btn-outline-dark border-2 col-11 col-md-10 col-xxl-7 d-flex align-items-center justify-content-center my-2">
          <i
            className="socialicons bi bi-github"
            style={{ fontSize: "1.4em", color: "#000" }}
          ></i>
          <span className="ms-2">Continue using GitHub account</span>
        </button>

        <button className="btn btn-outline-dark border-2 col-11 col-md-10 col-xxl-7 d-flex align-items-center justify-content-center my-3">
          <i className="bi bi-google" style={{ fontSize: "1.4em" }}></i>
          <span className="ms-2">Continue using Google account</span>
        </button>
      </div>

      <div className="mx-auto mt-2 col-11 col-md-10 col-xxl-7 d-flex justify-content-center align-items-center">
        <div className="auth-divider"></div>
        <div className="mx-2 text-uppercase fw-bold">or</div>
        <div className="auth-divider"></div>
      </div>

      {children}
    </div>
  );
};

export default AuthWrapper;
