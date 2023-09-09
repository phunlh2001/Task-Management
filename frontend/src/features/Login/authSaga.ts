import { Account, Token } from 'models';
import { authApi } from 'services/api/authApi';
import TokenService from 'services/token';
import { PayloadAction } from '@reduxjs/toolkit';
import { all, call, put, takeLatest } from 'redux-saga/effects';
import { authAction } from './authSlice';
import { AxiosError } from 'axios';

function* handleLogin(action: PayloadAction<Account>) {
  try {
    const { data }: { data: Token } = yield call(authApi.login, {
      username: action.payload.username,
      password: action.payload.password
    })

    const { accessToken, refreshToken } = data

    yield all(
      [
        TokenService.setAccessToken(accessToken),
        TokenService.setRefreshToken(refreshToken),
        put(authAction.loginSuccess())
      ]
    )
  } catch (error) {
    if (error instanceof AxiosError) {
      yield put(authAction.loginFailed(error.response?.data ? error.response?.data.message : 'wrong something'))
    }    
  }
}

function* apiLogout(refreshToken: string | undefined) {
  try {
      yield call(authApi.logout, refreshToken)
  } catch (error) {
      console.log(error)
  }
}

function* handleLogout(action: PayloadAction<Token | undefined>) {

  if (action.payload?.accessToken) {
      yield call(apiLogout, action.payload?.refreshToken)
  }
  yield all(
    [
      TokenService.removeAccessToken(),
      TokenService.removeRefreshToken(), 
      put(authAction.logoutSuccess())
    ]
  )
}

export default function* authSaga() {
  yield takeLatest(authAction.logout.type, handleLogout)
  yield takeLatest(authAction.login.type, handleLogin)
}