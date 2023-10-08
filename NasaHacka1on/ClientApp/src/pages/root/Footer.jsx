import './footer.css'
const Footer = () => {
  return (
    <footer className="footer container-fluid py-3 mt-3 d-flex flex-row">
      <div className="row align-items-center gy-2 px-3 container-sm mx-auto">
              <div className="col-md-4 text-center color">
                 &copy; 2023 CommunityCodeHub
              </div>
              <div className="col-md-4 d-flex justify-content-center flex-column align-items-center">
                  <a href="/" className="text-decoration-none on-hover"> <span className="m-1 color">
                       Terms & Conditions </span></a>
              <a href="/" className="text-decoration-none on-hover"><span className="m-1 color">
                      Privacy Policy</span></a>
              </div>
        <div className="col-md-4 text-center">
                  <a href="https://www.facebook.com/slysz.dawid/" target="_blank">
            <i className="socialicons bi bi-facebook"></i>
          </a>
          <a href="https://github.com/DeVinczi" target="_blank">
            <i className="socialicons bi bi-github"></i>
          </a>
                  <a href="https://www.linkedin.com/in/dawid-slysz-733813268/">
            <i className="socialicons bi bi-linkedin" target="_blank"></i>
          </a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
