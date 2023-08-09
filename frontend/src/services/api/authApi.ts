import { Account, Token } from '@/models';
import axiosClient from './axiosClient';

export const authApi = {
  login: (payload: Account) => {
    const url = '/auth/login'
    return axiosClient.post(url, payload)
  },

  logout: (refreshToken: string | undefined) => {
    const url = '/auth/logout'
    return axiosClient.post(url, { refreshToken })
  },

  register: (payload: Token) => {
    const url = '/auth/register'
    return axiosClient.post(url, { refreshToken: payload.refreshToken })
  }
}