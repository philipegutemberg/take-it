import axios from 'axios';
import React, { createContext, useContext, useState } from 'react'
import { LoadingContext } from './LoadingContext';
import jwtDecode from 'jwt-decode';

export const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const {setIsLoading} = useContext(LoadingContext);
    const [token, setToken] = useState('');

    const login = async (username, password, callBack) => {
        try {
            setIsLoading(true);
            let result = await axios.post("/api/v1/login", { username: username, password: password });
            let decodedJwt = jwtDecode(result.data.token);
            setToken({
                jwt: result.data.token,
                username: decodedJwt.name,
                role: decodedJwt.role
            });
            axios.defaults.headers.common["Authorization"] = "Bearer " + result.data.token;
            if (callBack)
                callBack(token);
        } catch (err) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
        
    };

    return (
        <AuthContext.Provider value={{token, login}}>
            {children}
        </AuthContext.Provider>
    );  
};