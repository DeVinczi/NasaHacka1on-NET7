import { useNavigate } from "react-router-dom";
import useInput from "../../hooks/use-input";
import { isEmail, isNotEmpty } from "../../utils/validators";
import authService from "../../services/auth.service";

const AddProject = () => {
  const navigate = useNavigate();

  //email input
  const {
    value: email,
    isValid: isEmailValid,
    hasError: hasEmailError,
    handleValueChange: handleEmailChange,
    handleInputBlur: handleEmailBlur,
    reset: resetEmailInput,
  } = useInput(isEmail);

  //password input
  const {
    value: password,
    isValid: isPasswordValid,
    hasError: hasPasswordError,
    handleValueChange: handlePasswordChange,
    handleInputBlur: handlePasswordBlur,
    reset: resetPasswordInput,
  } = useInput(isNotEmpty);

  //form validation
  let isFormValid = false;

  if (isEmailValid && isPasswordValid) {
    isFormValid = true;
  }

  //handlers

  const handleRedirect = () => {
    navigate("/sign-up");
  };

  const handleLogin = (event) => {
    event.preventDefault();
    if (!isFormValid) {
      return;
    }

    const user = { email, password };
    const response = authService.login(user);
    console.log("login comp onent: ", response);

    resetEmailInput();
    resetPasswordInput();
    // navigate("/projects");
  };

  const projectNameInputClasses = hasEmailError
    ? "form-control invalid"
    : "form-control";

  const githubRepoInputClasses = hasPasswordError
    ? "form-control invalid"
    : "form-control";

  const descriptionInputClasses = hasPasswordError
    ? "form-control invalid"
    : "form-control";

  const techOptions = [
    "Angular",
    "Bash",
    "C / C++",
    "C#",
    "CouchDB",
    "Dart",
    "Gatsby",
    "Go",
    "GraphQL",
    "Haskel",
    "HTML / CSS",
    "Java",
    "JavaScript",
    "JQuery",
    "Lua",
    "MarkDown",
    "MongoDB",
    "MySQL",
    "NextJS",
    "NodeJS",
    "PHP",
    "PostgreSQL",
    "Python",
    "React",
    "Ruby",
    "Rust",
    "Shell",
    "SQLite",
    "Svelte",
    "TypeScript",
    "VueJS",
  ];

  const handleTechOptions = (event) => {
    console.log(event.target.value);
  };

  return (
    <form
      className="container-sm mx-auto my-3 d-flex flex-column justify-content-center align-items-center"
      onSubmit={handleLogin}
    >
      <p className="mb-2 fs-5 col-12 col-md-10 col-lg-8 fw-bold">
        List your project
      </p>
      <div className="mb-2 col-12 col-md-10 col-lg-8">
        <label htmlFor="projectName" className="form-label ps-2">
          Project name
        </label>
        <input
          type="text"
          className={projectNameInputClasses}
          id="projectName"
          value={email}
          onChange={handleEmailChange}
          onBlur={handleEmailBlur}
        />
        {hasEmailError && (
          <p className="text-danger mt-1">Please enter project name.</p>
        )}
      </div>

      <div className="mb-2 mb-3 col-12 col-md-10 col-lg-8">
        <label htmlFor="githubRepo" className="form-label">
          <i
            className="socialicons bi bi-github me-2"
            style={{ fontSize: "1.2em", color: "#000" }}
          ></i>
          GitHub repository
        </label>
        <input
          type="text"
          className={githubRepoInputClasses}
          id="githubRepo"
          value={password}
          onChange={handlePasswordChange}
          onBlur={handlePasswordBlur}
        />
        {hasPasswordError && (
          <p className="text-danger mt-1">Please provide a valid password.</p>
        )}
      </div>

      <div className="mb-2 col-12 col-md-10 col-lg-8">
        <label htmlFor="description" className="form-label ps-2">
          Description
        </label>
        <input
          type="text"
          className={descriptionInputClasses}
          id="description"
          value={password}
          onChange={handlePasswordChange}
          onBlur={handlePasswordBlur}
        />
        {hasPasswordError && (
          <p className="text-danger mt-1">Please provide a valid password.</p>
        )}
      </div>
      <div className="mb-2 col-12 col-md-10 col-lg-8">
        <label htmlFor="description" className="form-label">
          <i className="bi bi-link-45deg ps-2 me-2"></i>Website URL
        </label>
        <input
          type="text"
          className={descriptionInputClasses}
          id="description"
          value={password}
          onChange={handlePasswordChange}
          onBlur={handlePasswordBlur}
        />
        {hasPasswordError && (
          <p className="text-danger mt-1">Please provide a valid password.</p>
        )}
      </div>
      <div className="mb-2 col-12 col-md-10 col-lg-8">
        <label htmlFor="description" className="form-label">
          <i className="bi bi-twitter ps-2 me-2"></i>Twitter URL
        </label>
        <input
          type="text"
          className={descriptionInputClasses}
          id="description"
          value={password}
          onChange={handlePasswordChange}
          onBlur={handlePasswordBlur}
        />
        {hasPasswordError && (
          <p className="text-danger mt-1">Please provide a valid password.</p>
        )}
      </div>

      <div className="mb-2 col-12 col-md-10 col-lg-8 row gx-1">
        <div className="col-12 col-xl-8">
          <p className="mb-2 fs-5 fw-bold">Project tags</p>
          <div className="mb-2">
            <label htmlFor="techFocusSelect" className="form-label">
              Tech Focus
            </label>
            <select
              className="form-select"
              id="techFocusSelect"
              multiple="multiple"
              onChange={handleTechOptions}
            >
              {/* <option value={"none"}>
                What technologies does your project cover?
              </option> */}
              {techOptions.map((option, index) => {
                return (
                  <option key={index} value={option}>
                    {option}
                  </option>
                );
              })}
              <option value>What technologies does your project cover?</option>
            </select>
          </div>
          <div className="mb-2">
            <label htmlFor="contributorRoleSelect" className="form-label">
              Contributor roles
            </label>
            <select className="form-select" id="contributorRoleSelect">
              <option selected>
                What kind of contributors are you looking for?
              </option>
            </select>
          </div>
          <div className="mb-2">
            <label htmlFor="subjectSelect" className="form-label">
              Subjects
            </label>
            <select className="form-select" id="subjectSelect">
              <option>What is your project about?</option>
            </select>
          </div>
        </div>

        <div className="col-12 col-xl-4">
          <p className="mb-2 fs-5 fw-bold">Contributing overview</p>
          <div className="mb-2">
            <label htmlFor="description" className="form-label">
              Automated dev environment
            </label>
            <input
              type="text"
              className={descriptionInputClasses}
              id="description"
              value={password}
              onChange={handlePasswordChange}
              onBlur={handlePasswordBlur}
              placeholder="milkyway.pl"
            />
            {hasPasswordError && (
              <p className="text-danger mt-1">
                Please provide a valid password.
              </p>
            )}
          </div>

          <div className="mb-2">
            <label htmlFor="description" className="form-label">
              Maintainer location
            </label>
            <input
              type="text"
              className={descriptionInputClasses}
              id="description"
              value={password}
              onChange={handlePasswordChange}
              onBlur={handlePasswordBlur}
              placeholder="Europe"
            />
            {hasPasswordError && (
              <p className="text-danger mt-1">
                Please provide a valid password.
              </p>
            )}
          </div>

          <div className="mb-2">
            <label htmlFor="description" className="form-label">
              Ideal contributor effort
            </label>
            <input
              type="text"
              className={descriptionInputClasses}
              id="description"
              value={password}
              onChange={handlePasswordChange}
              onBlur={handlePasswordBlur}
              placeholder="1/month"
            />
            {hasPasswordError && (
              <p className="text-danger mt-1">
                Please provide a valid password.
              </p>
            )}
          </div>
        </div>
      </div>

      <p className="mb-3 fs-5 col-12 col-md-10 col-lg-8 fw-bold">Content</p>

      <div className="mb-3 col-12 col-md-10 col-lg-8">
        <label htmlFor="overviewTextArea" className="form-label ps-2">
          Overview
        </label>
        <textarea
          className="form-control"
          id="overviewTextArea"
          placeholder="What is your project about? What does it do?"
          rows="3"
        ></textarea>
      </div>

      <div className="mb-3 col-12 col-md-10 col-lg-8">
        <label htmlFor="contributingTextArea" className="form-label ps-2">
          Contributing
        </label>
        <textarea
          className="form-control"
          id="contributingTextArea"
          placeholder="How can potencial contributors onboard effeciently?"
          rows="3"
        ></textarea>
      </div>

      <button
        type="submit"
        className="btn btn-dark btn-submit-project"
        disabled={!isFormValid}
      >
        Create project
      </button>
    </form>
  );
};

export default AddProject;
