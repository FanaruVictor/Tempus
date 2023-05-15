import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddGroupData } from '../../_commons/models/groups/addGroupData';
import { GenericResponse } from '../../_commons/models/genericResponse';
import { GroupOverview } from '../../_commons/models/groups/groupOverview';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';
import { withCache } from '@ngneat/cashew';
import { GroupDetails } from '../../_commons/models/groups/groupDetails';
import { UpdateGroupData } from '../../_commons/models/groups/udpateGroupData';

@Injectable({
  providedIn: 'root',
})
export class GroupApiService {
  apiUrl = `${environment.apiUrl}/v1/groups`;

  constructor(private httpClient: HttpClient) {}

  getAll() {
    return this.httpClient.get<GenericResponse<GroupOverview[]>>(this.apiUrl, {
      context: withCache(),
    });
  }

  add(group: AddGroupData) {
    let formData = new FormData();
    formData.append('name', group.name);
    if (!!group.image) formData.append('image', group.image);
    formData.append('members', JSON.stringify(group.members.join(',')));
    return this.httpClient.post<any>(this.apiUrl, formData);
  }

  edit(group: UpdateGroupData, id: string) {
    let formData = new FormData();
    formData.append('id', id);
    formData.append('name', group.name);
    if (!!group.image) formData.append('image', group.image);
    formData.append('members', JSON.stringify(group.members.join(',')));
    return this.httpClient.put<any>(this.apiUrl, formData);
  }

  delete(id: string) {
    return this.httpClient.delete<GenericResponse<string>>(
      `${this.apiUrl}/${id}`
    );
  }

  getAllRegistrations(groupId: string) {
    return this.httpClient.get<GenericResponse<RegistrationOverview[]>>(
      `${this.apiUrl}/${groupId}/registrations`
    );
  }

  getById(id: string) {
    return this.httpClient.get<GenericResponse<GroupDetails>>(
      `${this.apiUrl}/${id}`
    );
  }
}
