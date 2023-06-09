import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericResponse } from '../_commons/models/genericResponse';
import { BaseCategory } from '../_commons/models/categories/baseCategory';
import { CreateCategoryCommandData } from '../_commons/models/categories/createCategoryCommandData';
import { UpdateCategoryCommandData } from '../_commons/models/categories/updateCategoryCommandData';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoryApiService {
  apiUrl = `${environment.apiUrl}/v1/categories`;

  constructor(private httpClient: HttpClient) {}

  getAll(groupId: string | undefined) {
    if (!!groupId) {
      return this.httpClient.get<GenericResponse<BaseCategory[]>>(
        `${this.apiUrl}/groups/${groupId}`
      );
    }

    return this.httpClient.get<GenericResponse<BaseCategory[]>>(this.apiUrl);
  }

  getById(id: string) {
    return this.httpClient.get<GenericResponse<BaseCategory>>(
      `${this.apiUrl}/${id}`
    );
  }

  create(category: CreateCategoryCommandData, groupId: string | undefined) {
    if (!!groupId) {
      return this.httpClient.post<GenericResponse<BaseCategory>>(
        `${this.apiUrl}/groups/${groupId}`,
        category
      );
    }

    return this.httpClient.post<GenericResponse<BaseCategory>>(
      this.apiUrl,
      category
    );
  }

  update(category: UpdateCategoryCommandData, groupId: string | undefined) {
    if (!!groupId) {
      return this.httpClient.put<GenericResponse<BaseCategory>>(
        `${this.apiUrl}/groups/${groupId}`,
        category
      );
    }
    return this.httpClient.put<GenericResponse<BaseCategory>>(
      this.apiUrl,
      category
    );
  }

  delete(id: string) {
    return this.httpClient.delete<GenericResponse<any>>(`${this.apiUrl}/${id}`);
  }
}
