import axios from 'axios';
import React, { createContext, useContext, useState } from 'react'
import { LoadingContext } from './LoadingContext';

export const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const {setIsLoading} = useContext(LoadingContext);
    const [token, setToken] = useState('');

    const login = async (username, password) => {
        // setIsLoading(true);
        let result = await axios.post("http://EC2Co-EcsEl-Z57Y9UO0KY8Y-890817140.us-east-2.elb.amazonaws.com/api/v1/login", { username: username, password: password });
        setToken(result.data.token);
    };

    return (
        <AuthContext.Provider value={{token, login}}>
            {children}
        </AuthContext.Provider>
    );  
};