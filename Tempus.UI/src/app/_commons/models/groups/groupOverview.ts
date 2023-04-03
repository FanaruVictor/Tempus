import { BaseGroup } from './baseGroup';

export interface GroupOverview extends BaseGroup {
  usersPhotos: string[] | undefined;
  createdAt: Date;
}
