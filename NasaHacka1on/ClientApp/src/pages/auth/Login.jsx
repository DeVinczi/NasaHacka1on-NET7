import { useNavigate } from "react-router-dom";
import AuthWrapper from "./AuthWrapper";
import { isNotEmpty, isEmail } from "../../utils/validators";
import useInput from "../../hooks/use-input";

const Login = () => {
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

    resetEmailInput();
    resetPasswordInput();
    // navigate("/projects");
  };

  const emailInputClasses = hasEmailError
    ? "form-control invalid"
    : "form-control";

  const passwordInputClasses = hasPasswordError
    ? "form-control invalid"
    : "form-control";

  return (
    <AuthWrapper>
      <div className="mx-auto mt-2 col-11 col-md-10 col-xxl-7 d-flex justify-content-center align-items-center">
        <div className="p-3 border-bottom border-2 border-dark fw-bold login-item text-center">
          Log in
        </div>
        <div
          className="p-3 border-bottom border-1 border-dark login-item text-center"
          role="button"
          onClick={handleRedirect}
        >
          Sign up
        </div>
      </div>
      <form
        className="col-11 col-md-10 col-xxl-7 mx-auto mt-5"
        onSubmit={handleLogin}
      >
        <div className="mb-3">
          <label htmlFor="email" className="form-label">
            Email address
          </label>
          <input
            type="email"
            className={emailInputClasses}
            id="email"
            value={email}
            onChange={handleEmailChange}
            onBlur={handleEmailBlur}
          />
          {hasEmailError && (
            <p className="text-danger mt-1">Please provide a valid email.</p>
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">
            Password
          </label>
          <input
            type="password"
            className={passwordInputClasses}
            id="password"
            value={password}
            onChange={handlePasswordChange}
            onBlur={handlePasswordBlur}
          />
          {hasPasswordError && (
            <p className="text-danger mt-1">Please provide a valid password.</p>
          )}
        </div>
        <div className="fw-bold mb-4" role="button">
          Forgot password ?
        </div>
        <button
          type="submit"
          className="btn btn-dark btn-submit"
          disabled={!isFormValid}
        >
          Log in
        </button>
      </form>
    </AuthWrapper>
  );
};

export default Login;
