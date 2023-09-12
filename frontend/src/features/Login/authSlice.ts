import { Account, Token } from 'models';
import { PayloadAction, createSlice } from '@reduxjs/toolkit';

interface authState {
  isLogin: boolean
  errorMessage: string
  loading: boolean
  username: string
  password: string
}

const initialState: authState = {
  isLogin: false,
  errorMessage: '',
  loading: false,
  username: '',
  password: ''
}

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    login(state, action: PayloadAction<Account>) {
      state.loading = true
    },
    loginSuccess(state) {
      state.isLogin = true
      state.loading = false
    },
    loginFailed(state, action: PayloadAction<string>) {
      state.loading = false
      state.errorMessage = action.payload
    },


    logout(state, action: PayloadAction<Token | undefined>) {},
    logoutSuccess(state) {
      state.isLogin = false
      state.loading = false
      state.errorMessage = ''
    }

  }
})

export const authAction = authSlice.actions

const authReducer = authSlice.reducer

export default authReducer