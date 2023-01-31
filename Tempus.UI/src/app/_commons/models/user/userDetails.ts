import {BaseUser} from "./baseUser";
import {Photo} from "../photo/photo";

export interface UserDetails extends BaseUser {
  photoDetails: Photo;
  isDarkTheme: boolean;
}
