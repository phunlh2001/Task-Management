import axios from "axios";
import { promises } from "dns";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const jwtInterceptor = axios.create({})

jwtInterceptor.interceptors.request.use(config => {
    let tokenData = sessionStorage.getItem("token")
    config.headers.common["Authorization"] = `Bearer ${tokenData.AccessToken}`
    return config
})

jwtInterceptor.interceptors.response.use(response => {
    return response
}, async (error) => {
    if (error.response.status === 401) {
        useNavigate("/")
        toast.error("Token het han hay dang nhap lai")
        let tokenData = sessionStorage.getItem("token")
        let payload = tokenData.RefreshToken
        let apiResponse = await axios.post('http://localhost:5000/api/auth/refreshToken', payload)
        sessionStorage.setItem("token", JSON.stringify(apiResponse.data))
        error.config.headers['Authorization'] = `Bearer ${apiResponse.data.AccessToken}`
        return axios(error)
    }
    else return promises.reject(error)
})