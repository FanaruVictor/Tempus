import {BaseUser} from "./baseUser";

export interface UpdateUserData extends BaseUser{
  newPhoto?: File;
  isCurrentPhotoChanged: boolean;
}
