import * as cookie from 'js-cookie';
import { TOKEN_NAME } from '@/constants';

const accessTokenConfig: cookie.CookieAttributes | undefined = {
  sameSite: 'strict'
}

const refreshTokenConfig: cookie.CookieAttributes | undefined = {
  sameSite: 'strict'
}

export const setCookieData = (key: string, value: string) => {
  const config = key === TOKEN_NAME.REFRESH_TOKEN ? refreshTokenConfig : accessTokenConfig
  cookie.set(key, value, config)
}

export const getCookieData = (key: string) => {
  return cookie.get(key)
}

export const removeCookieData = (key: string) => {
  return cookie.remove(key)
}