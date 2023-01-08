import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { GenericResponse } from '../_commons/models/genericResponse';
import {BaseCategory} from "../_commons/models/categories/baseCategory";
import {CreateCategoryCommandData} from "../_commons/models/categories/createCategoryCommandData";
import {UpdateCategoryCommandData} from "../_commons/models/categories/updateCategoryCommandData";

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {

  constructor(private httpClient: HttpClient) {
  }

  getAll() {
    return this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/v1/categories`);
  }

  getById(id: string) {
    return this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/v1/categories/${id}`);
  }

  create(category: CreateCategoryCommandData) {
    return this.httpClient.post<GenericResponse<BaseCategory>>(`https://localhost:7077/api/v1/categories`, category);
  }

  update(category: UpdateCategoryCommandData){
      return this.httpClient.put<GenericResponse<BaseCategory>>(`https://localhost:7077/api/v1/categories`, category);
  }
}
