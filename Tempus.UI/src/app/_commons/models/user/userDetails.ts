import {BaseUser} from "./baseUser";
import {Photo} from "../photo/photo";

export interface UserDetails extends BaseUser {
  id: string;
  photo?: Photo;
  isDarkTheme: boolean;
  externalId: string;
}
