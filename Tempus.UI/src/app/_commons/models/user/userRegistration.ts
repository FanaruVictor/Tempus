import {BaseUser} from "./baseUser";

export interface UserRegistration extends BaseUser{
  password: string;
  isExternal: boolean;
}
