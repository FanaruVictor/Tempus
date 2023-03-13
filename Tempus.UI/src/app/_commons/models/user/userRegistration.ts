import {BaseUser} from "./baseUser";

export interface UserRegistration extends BaseUser{
  isExternal: boolean;
}
