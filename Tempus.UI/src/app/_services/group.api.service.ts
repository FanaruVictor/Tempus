import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddGroupData } from '../_commons/models/groups/addGroupData';

@Injectable({
  providedIn: 'root',
})
export class GroupApiService {
  apiUrl = `${environment.apiUrl}/v1/groups`;

  constructor(private httpClient: HttpClient) {}

  getAll() {
    return this.httpClient.get<any>(this.apiUrl);
  }

  add(group: AddGroupData) {
    return this.httpClient.post<any>(this.apiUrl, group);
  }
}
