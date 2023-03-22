import {BaseUser} from "./baseUser";
import {Photo} from "../photo/photo";

export interface UpdateUserData extends BaseUser{
  newPhoto?: File;
  isCurrentPhotoChanged: boolean;
}
