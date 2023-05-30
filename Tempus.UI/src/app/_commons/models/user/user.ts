import { BaseUser } from './baseUser';

export interface User extends BaseUser {
  id: string;
  isDarkTheme: boolean;
}
