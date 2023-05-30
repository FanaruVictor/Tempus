export interface BaseUser {
  externalId?: string;
  email: string;
  displayName: string;
  photoURL?: string;
  emailVerified: boolean;
}
