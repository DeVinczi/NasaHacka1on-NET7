const Footer = () => {
  return (
    <footer className="footer container-fluid py-3 mt-3">
      <div className="row row-cols-1 row-cols-lg-2 align-items-center gy-2 px-3 container-sm mx-auto">
        <div className="col text-center text-lg-start">
          &copy; 2023 CommunityCodeHub
        </div>
        <div className="col text-center text-lg-end">
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
