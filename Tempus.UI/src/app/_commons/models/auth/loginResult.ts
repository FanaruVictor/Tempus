import {UserDetails} from "../user/userDetails";

export interface LoginResult{
  user: UserDetails,
  authorizationToken: string
}
