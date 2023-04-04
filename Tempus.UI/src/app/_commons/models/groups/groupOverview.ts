import { BaseGroup } from './baseGroup';

export interface GroupOverview extends BaseGroup {
  userPhotos: string[];
  createdAt: Date;
}
