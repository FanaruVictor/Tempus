import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddGroupData } from '../_commons/models/groups/addGroupData';
import { GenericResponse } from '../_commons/models/genericResponse';
import { GroupOverview } from '../_commons/models/groups/groupOverview';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GroupApiService {
  apiUrl = `${environment.apiUrl}/v1/groups`;

  constructor(private httpClient: HttpClient) {}

  getAll() {
    return this.httpClient.get<GenericResponse<GroupOverview[]>>(this.apiUrl);
  }

  add(group: AddGroupData) {
    let formData = new FormData();
    formData.append('name', group.name);
    if (!!group.image) formData.append('image', group.image);
    formData.append('members', JSON.stringify(group.members.join(',')));
    return this.httpClient.post<any>(this.apiUrl, formData);
  }
}
