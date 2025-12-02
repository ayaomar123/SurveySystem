export interface User {
  id: number,
  name: string,
  email: string,
  passwordHash: string,
  role: number
}

export interface CreateUser {
  id: number,
  name: string,
  email: string,
  passwordHash: string,
  role: number
}
