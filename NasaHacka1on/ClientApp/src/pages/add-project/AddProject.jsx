import { useNavigate } from "react-router-dom";
import useInput from "../../hooks/use-input";
import { isNotEmpty } from "../../utils/validators";
import Select from "react-select";
import { useState } from "react";
import './addproject.css'

const AddProject = () => {
  const navigate = useNavigate();

  //project name input
  const {
    value: projectName,
    isValid: isProjectNameValid,
    hasError: hasProjectNameError,
    handleValueChange: handleProjectNameChange,
    handleInputBlur: handleProjectNameBlur,
    reset: resetProjectNameInput,
  } = useInput(isNotEmpty);

  //description input
  const {
    value: description,
    isValid: isDescriptionValid,
    hasError: hasDescriptionError,
    handleValueChange: handleDescriptionChange,
    handleInputBlur: handleDescriptionBlur,
    reset: resetDescriptionInput,
  } = useInput(isNotEmpty);

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

  if (isProjectNameValid && isDescriptionValid && isPasswordValid) {
    isFormValid = true;
  }

  //handlers

  const handleRedirect = () => {
    navigate("/sign-up");
  };

  const handleAddProject = (event) => {
    event.preventDefault();
    if (!isFormValid) {
      return;
    }
    //project model to send to BE
    const project = { projectName, password };
    //action to create projecy
    //.....

    resetProjectNameInput();
    resetDescriptionInput();
    resetPasswordInput();
    // navigate("/projects");
  };

  const projectNameInputClasses = hasProjectNameError
    ? "form-control invalid"
    : "form-control";

  const githubRepoInputClasses = hasPasswordError
    ? "form-control invalid"
    : "form-control";

  const descriptionInputClasses = hasDescriptionError
    ? "form-control invalid"
    : "form-control";

  const techOptions = [
    { value: "Angular", label: "Angular" },
    { value: "Bash", label: "Bash" },
    { value: "C / C++", label: "C / C++" },
    { value: "C#", label: "C#" },
    { value: "CouchDB", label: "CouchDB" },
    { value: "Dart", label: "Dart" },
    { value: "Gatsby", label: "Gatsby" },
    { value: "Go", label: "Go" },
    { value: "GraphQL", label: "GraphQL" },
    { value: "Haskel", label: "Haskel" },
    { value: "HTML / CSS", label: "HTML / CSS" },
    { value: "Java", label: "Java" },
    { value: "JavaScript", label: "JavaScript" },
    { value: "JQuery", label: "JQuery" },
    { value: "Lua", label: "Lua" },
    { value: "MarkDown", label: "MarkDown" },
    { value: "MongoDB", label: "MongoDB" },
    { value: "MySQL", label: "MySQL" },
    { value: "NextJS", label: "NextJS" },
    { value: "NodeJS", label: "NodeJS" },
    { value: "PHP", label: "PHP" },
    { value: "PostgreSQL", label: "PostgreSQL" },
    { value: "Python", label: "Python" },
    { value: "React", label: "React" },
    { value: "Ruby", label: "Ruby" },
    { value: "Rust", label: "Rust" },
    { value: "Shell", label: "Shell" },
    { value: "SQLite", label: "SQLite" },
    { value: "Svelte", label: "Svelte" },
    { value: "TypeScript", label: "TypeScript" },
    { value: "GraphQL", label: "GraphQL" },
  ];

  const [chosenTechOptions, setChosenTechOptions] = useState(null);

  const handleTechOptions = (selectedOptions) => {
    setChosenTechOptions(selectedOptions.map((option) => option.value));
  };

  const contributorOptions = [
    { value: "Backend Developers", label: "Backend Developers" },
    { value: "Code Reviewers", label: "Code Reviewers" },
    { value: "Designers", label: "Designers" },
    { value: "Developers", label: "Developers" },
    { value: "DevOps", label: "DevOps" },
    { value: "Frontend Developers", label: "Frontend Developers" },
    { value: "Issue Triage", label: "Issue Triage" },
    { value: "Maintainers", label: "Maintainers" },
    { value: "Mentors", label: "Mentors" },
    { value: "Project Owners", label: "Project Owners" },
    { value: "Researchers", label: "Researchers" },
    { value: "Technical Writers", label: "Technical Writers" },
    { value: "Testers", label: "Testers" },
    { value: "UX", label: "UX" },
  ];

  const [chosenContributorOptions, setChosenContributorOptions] =
    useState(null);

  const handleContributorOptions = (selectedOptions) => {
    setChosenContributorOptions(selectedOptions.map((option) => option.value));
  };

  const subjectsOptions = [
    { value: "Accessibility", label: "Accessibility" },
    { value: "Android", label: "Android" },
    { value: "Automation", label: "Automation" },
    { value: "Browser Extension", label: "Browser Extension" },
    { value: "CLI", label: "CLI" },
    { value: "Code Framework", label: "Code Framework" },
    { value: "Cryptography", label: "Cryptography" },
    { value: "Data Visualization", label: "Data Visualization" },
    { value: "DevOps", label: "DevOps" },
    { value: "E-Commerce", label: "E-Commerce" },
    { value: "Education", label: "Education" },
    { value: "Embedded Systems", label: "Embedded Systems" },
    { value: "Environment", label: "Environment" },
    { value: "Finance", label: "Finance" },
    { value: "First Timer Friendly", label: "First Timer Friendly" },
    { value: "Gaming", label: "Gaming" },
    { value: "Git", label: "Git" },
    { value: "Health", label: "Health" },
    { value: "Infosec", label: "Infosec" },
    { value: "iOS", label: "iOS" },
    { value: "Machine learning", label: "Machine learning" },
    { value: "Media", label: "Media" },
    { value: "Productivity", label: "Productivity" },
    { value: "Raspberry Pi", label: "Raspberry Pi" },
    { value: "Scraping", label: "Scraping" },
    { value: "Social", label: "Social" },
    { value: "Social Activism", label: "Social Activism" },
    { value: "Starter Template", label: "Starter Template" },
    { value: "Tools", label: "Tools" },
    { value: "Web Application", label: "Web Application" },
  ];

  const [chosenSubjectOptions, setChosenSubjectOptions] = useState(null);

  const handleSubjectOptions = (selectedOptions) => {
    console.log(selectedOptions);
    setChosenSubjectOptions(selectedOptions.map((option) => option.value));
  };

  const [overviewText, setOverviewText] = useState("");

  const handleOverviewChange = (event) => {
    setOverviewText(event.target.value);
  };

  const [contributingText, setContributingText] = useState("");

  const handleContributingChange = (event) => {
    setContributingText(event.target.value);
  };

    return (
        <div>
            <div className="project-container mx-auto py-2 px-3 mt-5 border border-1 shadow ">
    <form
      className="container-sm mx-auto my-1 mt-4 d-flex flex-column justify-content-center align-items-center"
      onSubmit={handleAddProject}
                ><p className="mb-2 h1 fs-3 col-12 col-md-10 fw-bold">Post your project</p>
                    <p className="mb-2 fs-6 col-12 col-md-10 pb-3 border-line">Fill form below to place your project on global board.</p>
                    <p className="mb-2 mt-2 fs-5 col-12 col-md-10 fw-bold">
        Common information
      </p>
      <div className="mb-2 col-12 col-md-10">
        <label htmlFor="projectName" className="form-label">
          Project name
        </label>
        <input
          type="text"
          className={projectNameInputClasses}
          id="projectName"
          value={projectName}
          onChange={handleProjectNameChange}
          onBlur={handleProjectNameBlur}
        />
        {hasProjectNameError && (
          <p className="text-danger mt-1">Please enter project name.</p>
        )}
      </div>

      <div className="mb-2 col-12 col-md-10">
        <label htmlFor="githubRepo" className="form-label">
          <i
            className="socialicons bi bi-github ps-0 me-2"
            style={{ fontSize: "1.1em", color: "#000" }}
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

      <div className="mb-2 col-12 col-md-10">
        <label htmlFor="description" className="form-label ps-0">
          Description
        </label>
        <input
          type="text"
          className={descriptionInputClasses}
          id="description"
          value={description}
          onChange={handleDescriptionChange}
          onBlur={handleDescriptionBlur}
        />
        {hasDescriptionError && (
          <p className="text-danger mt-1">Please provide a description.</p>
        )}
      </div>

      <div className="mb-2 col-12 col-md-10">
        <label htmlFor="description" className="form-label">
          <i className="bi bi-link-45deg ps-0 me-1"></i>Website URL
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
      <div className="mb-2 col-12 col-md-10">
        <label htmlFor="description" className="form-label">
          <i className="bi bi-linkedin ps-1 me-2"></i>Linkedin URL
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

      <div className="mb-2 col-12 col-md-10 row gx-1 mt-4">
        <div className="col-12 col-xl-10">
          <p className="mb-2 fs-5 fw-bold">Project tags</p>
          <div className="mb-2">
            <label htmlFor="techFocusSelect" className="form-label">
              Tech Focus
            </label>
            <Select
              id="techFocusSelect"
              options={techOptions}
              isMulti
              placeholder="What technologies does your project cover?"
              hideSelectedOptions
              onChange={handleTechOptions}
            />
          </div>

          <div className="mb-2">
            <label htmlFor="contributorSelect" className="form-label">
              Contributor roles
            </label>
            <Select
              id="contributorSelect"
              options={contributorOptions}
              isMulti
              placeholder="What kind of contributors are you looking for?"
              hideSelectedOptions
              onChange={handleContributorOptions}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="subjectSelect" className="form-label">
              Subjects
            </label>
            <Select
              id="subjectSelect"
              options={subjectsOptions}
              isMulti
              placeholder="What is your project about?"
              hideSelectedOptions
              onChange={handleSubjectOptions}
            />
          </div>
        </div>

        <div className="col-12 col-xl-8 mt-4">
          <p className="mb-2 fs-5 fw-bold">Contributing overview</p>
          <div className="mb-2">
            <label htmlFor="description" className="form-label">
              Development Environment
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

      <p className="mt-3 mb-2 fs-5 col-12 col-md-10 fw-bold">Content</p>

      <div className="mb-3 col-12 col-md-10">
        <label htmlFor="overviewTextArea" className="form-label">
          Overview
        </label>
        <textarea
          className="form-control"
          id="overviewTextArea"
          placeholder="What is your project about? What does it do?"
          rows="3"
          onChange={handleOverviewChange}
        ></textarea>
      </div>

      <div className="mb-3 col-12 col-md-10">
        <label htmlFor="contributingTextArea" className="form-label">
          Contributing
        </label>
        <textarea
          className="form-control"
          id="contributingTextArea"
          placeholder="How can potencial contributors onboard effeciently?"
          rows="3"
          onChange={handleContributingChange}
        ></textarea>
      </div>

      <button
        type="submit"
                        className="btn btn-dark btn-submit-project button button-border"
        disabled={!isFormValid}
      >
        Create project
      </button>
            </form>
            </div>
        </div>
  );
};

export default AddProject;
