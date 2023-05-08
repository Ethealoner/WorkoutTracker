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
