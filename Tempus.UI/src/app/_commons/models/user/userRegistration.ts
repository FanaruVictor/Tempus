import { BaseUser } from './baseUser';

export interface UserRegistration extends BaseUser {
  externalId?: string;
  photoURL?: string;
}
