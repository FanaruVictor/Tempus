import {BaseRegistration} from "./baseRegistration";

export interface RegistrationDetails extends BaseRegistration{
  content: string,
  lastUpdatedAt: string
}
