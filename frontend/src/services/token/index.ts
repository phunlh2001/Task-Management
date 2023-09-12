import { TOKEN_NAME } from '../../constants'
import { getCookieData, removeCookieData, setCookieData } from '../cookie/cookie'

const getAccessToken = () => {
  const token = getCookieData(TOKEN_NAME.ACCESS_TOKEN)
  return token
}

const getRefreshToken = () => {
  const token = getCookieData(TOKEN_NAME.REFRESH_TOKEN)
  return token
}

const setAccessToken = (accessToken: string) => {
  setCookieData(TOKEN_NAME.ACCESS_TOKEN, accessToken)
}

const setRefreshToken = (refreshToken: string) => {
  setCookieData(TOKEN_NAME.REFRESH_TOKEN, refreshToken)
}

const updateAccessToken = (accessToken: string) => {
  setCookieData(TOKEN_NAME.ACCESS_TOKEN, accessToken);
}

const updateRefreshToken = (refreshToken: string) => {
  setCookieData(TOKEN_NAME.REFRESH_TOKEN, refreshToken)
}

const removeAccessToken = () => {
  removeCookieData(TOKEN_NAME.ACCESS_TOKEN)
}

const removeRefreshToken = () => {
  removeCookieData(TOKEN_NAME.REFRESH_TOKEN)
}

const TokenService = {
  getAccessToken,
  updateAccessToken,
  setAccessToken,
  removeAccessToken,
  getRefreshToken,
  setRefreshToken,
  updateRefreshToken,
  removeRefreshToken
}

export default TokenService
