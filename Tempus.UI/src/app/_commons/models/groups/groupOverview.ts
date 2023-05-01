import { BaseGroup } from './baseGroup';

export interface GroupOverview extends BaseGroup {
  userPhotos: string[];
  userCount: number;
  createdAt: Date;
}
