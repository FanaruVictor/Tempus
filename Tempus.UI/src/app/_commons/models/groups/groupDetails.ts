import { UserEmail } from '../user/userEmail';
import { BaseGroup } from './baseGroup';

export interface GroupDetails extends BaseGroup {
  members: UserEmail[];
}
