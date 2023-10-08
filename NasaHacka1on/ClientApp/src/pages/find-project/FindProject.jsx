import Projects from "../projects/Projects.jsx"
import './findproject.css'
const FindProject = () => {
    
    return (
        <div>

            <div className="container">
                <div className="search-container ">
                    <div className="d-flex input-group mb-3 mt-3 ms-2 style">
                        <input type="text" className="form-control" placeholder="Find your next open source project" aria-label="Search" aria-describedby="basic-addon2"/>
                            <div className="input-group-append">
                                <button className="btn btn-primary" type="button">Search</button>
                            </div>
                    </div>
                </div>
            </div>




            <Projects />
        </div>
    );
    
};

export default FindProject;

