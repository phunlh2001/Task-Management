import axios from 'axios';
import TokenService from '../token';
import { stringify } from 'qs';


const BASE_URL = process.env.BASE_URL

const axiosClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  }
})

axiosClient.interceptors.request.use(async (config) => {
  const _token = TokenService.getAccessToken()

  if (_token) {
    config.headers.common = { Authorization: `Bearer ${_token}` }
    config.paramsSerializer = (params) => stringify(params, {indices: false})
  }
  
  return config
})

axiosClient.interceptors.response.use((response) => {
  if (response && response.data) {
    return response.data
  }

  return response
})

export default axiosClient