import axios from "axios";
import { createContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const AuthContext = createContext()


export const AuthContextProvider = ({ children }) => {

    const [user, setUser] = useState()
    const navigate = useNavigate();


    // decode
    function parseJwt(token) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    }


    // post api username and password for login
    const login = async (payload) => {
        await axios.post('http://localhost:5000/api/auth/login', payload)
            .then((response) => {
                if (response.status === 200) {
                    navigate("/homepage")
                    let AccessToken = parseJwt(response.data.Data.AccessToken)
                    setUser(AccessToken)
                    console.log(response);
                    sessionStorage.setItem('token', JSON.stringify(response.data.Data))
                }
            }).catch(error => {
                console.log(error);
                if (error.response.status === 400 || error.response.status === 500) {
                    toast.warning("userName or password was wrong");
                }
            })
    }
    return <AuthContext.Provider value={{ login, user }}>{children}</AuthContext.Provider>
}

export default AuthContext