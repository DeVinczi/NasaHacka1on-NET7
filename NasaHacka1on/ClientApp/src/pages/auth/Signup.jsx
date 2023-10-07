import { useNavigate } from 'react-router-dom';
import AuthWrapper from './AuthWrapper';
import useInput from '../../hooks/use-input';
import {
  isEmail,
  isMinFiveChars,
  isMinNineChars,
} from '../../utils/validators';
import authService from '../../services/auth.service';

const Signup = () => {
  const navigate = useNavigate();

  //displayName input
  const {
    value: username,
    isValid: isUsernameValid,
    hasError: hasUsernameError,
    handleValueChange: handleUsernameChange,
    handleInputBlur: handleUsernameBlur,
    reset: resetUsernameInput,
  } = useInput(isMinFiveChars);

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
  } = useInput(isMinNineChars);

  //form validation
  let isFormValid = false;

  if (isUsernameValid && isEmailValid && isPasswordValid) {
    isFormValid = true;
  }

  //handlers

  const handleSignup = (event) => {
    event.preventDefault();
    if (!isFormValid) {
      return;
    }

    const user = { displayName: username, email, password };

    const response = authService.createAccount(user);
    console.log('z komponentu: ', response);

    resetUsernameInput();
    resetEmailInput();
    resetPasswordInput();
    // navigate("/projects");
  };

  const usernameInputClasses = hasUsernameError
    ? 'form-control invalid'
    : 'form-control';

  const emailInputClasses = hasEmailError
    ? 'form-control invalid'
    : 'form-control';

  const passwordInputClasses = hasPasswordError
    ? 'form-control invalid'
    : 'form-control';

  const handleRedirect = () => {
    navigate('/log-in');
  };

  return (
    <AuthWrapper>
      <div className='mx-auto mt-2 col-11 col-md-10 col-xxl-7 d-flex justify-content-center align-items-center'>
        <div
          className='p-3 border-bottom border-1 border-dark login-item text-center pointer'
          role='button'
          onClick={handleRedirect}
        >
          Log in
        </div>
        <div className='p-3 border-bottom border-2 border-dark fw-bold login-item text-center'>
          Sign up
        </div>
      </div>
      <form
        className='col-11 col-md-10 col-xxl-7 mx-auto mt-5'
        onSubmit={handleSignup}
      >
        <div className='mb-3'>
          <label htmlFor='username' className='form-label'>
            Username
          </label>
          <input
            type='text'
            id='username'
            className={usernameInputClasses}
            aria-describedby='usernameHelp'
            value={username}
            onChange={handleUsernameChange}
            onBlur={handleUsernameBlur}
          />
          <div id='usernameHelp' className='form-text'>
            Must be min. 5 characters long.
          </div>
          {hasUsernameError && (
            <p className='text-danger mt-1'>Please provide a valid password.</p>
          )}
        </div>
        <div className='mb-3'>
          <label htmlFor='email' className='form-label'>
            Email address
          </label>
          <input
            type='email'
            className={emailInputClasses}
            id='email'
            value={email}
            onChange={handleEmailChange}
            onBlur={handleEmailBlur}
          />
          {hasEmailError && (
            <p className='text-danger mt-1'>Please provide a valid email.</p>
          )}
        </div>
        <div className='mb-3'>
          <label htmlFor='password' className='form-label'>
            Password
          </label>
          <input
            type='password'
            className={passwordInputClasses}
            id='password'
            aria-describedby='passwordHelp'
            value={password}
            onChange={handlePasswordChange}
            onBlur={handlePasswordBlur}
          />
          <div id='passwordHelp' className='form-text'>
            Must be min. 9 characters long and contain one lowercase, capital
            letter and special character.
          </div>
          {hasPasswordError && (
            <p className='text-danger mt-1'>Please provide a valid password.</p>
          )}
        </div>
        <button
          type='submit'
                  className='btn btn-dark btn-submit button button-border'
          disabled={!isFormValid}
        >
          Sign up
        </button>
      </form>
    </AuthWrapper>
  );
};

export default Signup;
