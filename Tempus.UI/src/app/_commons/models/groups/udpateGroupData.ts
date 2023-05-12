import { AddGroupData } from './addGroupData';

export interface UpdateGroupData extends AddGroupData {
  id: string;
  isCurrentImageChanged: boolean;
}
