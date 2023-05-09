export interface User {
  userId: string;
  userName: string;
}

export interface UserForAuthentication {
  userName: string;
  password: string;
}

export interface AuthResponse {
  message: string;
  token: string;
}

export interface UserForRegistration {
  UserName: string;
  Password: string;
  ConfirmPassword: string;
}

export interface ExternalAuthentication {
  provider: string;
  idToken: string;
}
