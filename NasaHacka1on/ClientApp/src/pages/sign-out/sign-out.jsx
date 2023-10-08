import { useNavigate } from 'react-router-dom';
import authService from '../../services/auth.service';
import React, { useEffect } from 'react';

const Signout = () => {
    const navigate = useNavigate();
    const handleSignout = async () => {

        await authService.logout();

        navigate("/");
        window.location.reload();
    }

    useEffect(() => {
        handleSignout();
    }, []);

    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-8">
                    <div className="card py-2 px-4 text-center">
                        <div className="card-body">
                            <h5 className="card-title">Logging out...</h5>
                            <h5 >Thank you for being with us!</h5>
                            
                        </div></div></div></div></div>
    );
        
};

export default Signout;