export interface ClientEvent {
  innerEventJson: any;
  responseType: ResponseType;
}

export enum ResponseType {
  AddRegistration,
  DeleteRegistration,
  UpdateRegistration,
}
