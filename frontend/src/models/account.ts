export interface Account {
  username: string
  password: string
}

export interface NewPassword {
  newPassword: string;
  confirmPassword: string;
  otpId?: string;
}
