const GetStarted = () => {
    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-8">
                    <div className="card py-4 px-4">
                        <div className="card-body">
                            <h5 className="card-title mb-4">Getting Started with Your Open Source Library</h5>
                            <p className="card-text">
                                Creating an open source library is an exciting venture! Follow these steps to ensure a successful start:
                            </p>
                            <ol>
                                <li><strong>Define Your Project's Goal:</strong> Clearly define the problem your library will solve and its goals. Keeping a focused purpose helps in efficient development.</li>
                                <li><strong>Pick a License:</strong> Choose an appropriate open-source license (e.g., MIT, Apache) that aligns with your project's vision and promotes collaboration.</li>
                                <li><strong>Set Up Version Control:</strong> Use Git for version control. Create a repository on platforms like GitHub or GitLab to manage and track changes in your codebase.</li>
                                <li><strong>Create a README:</strong> Craft a detailed README file. Include information about the library, installation instructions, usage examples, and how to contribute.</li>
                                {/* Additional steps can be added here */}
                            </ol>
                            <p className="mt-4">
                                Now, you're all set to dive into building your open source library! Don't forget to engage with the community, encourage contributions, and celebrate your progress along the way.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default GetStarted;
