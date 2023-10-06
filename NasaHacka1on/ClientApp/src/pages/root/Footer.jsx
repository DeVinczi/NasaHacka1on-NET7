const Footer = () => {
  return (
    <footer className="footer container-fluid py-3">
      <div className="row row-cols-1 row-cols-lg-2 align-items-center gy-2 px-3 container-sm mx-auto">
        <div className="col text-center text-lg-start">
          &copy; 2023 CommunityCodeHub
        </div>
        <div className="col text-center text-lg-end">
          <a href="https://rb.gy/3d3ov" target="_blank">
            <i className="socialicons bi bi-youtube"></i>
          </a>
          <a href="https://github.com/CoderFoundry" target="_blank">
            <i className="socialicons bi bi-github"></i>
          </a>
          <a href="#">
            <i className="socialicons bi bi-twitter" target="_blank"></i>
          </a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
