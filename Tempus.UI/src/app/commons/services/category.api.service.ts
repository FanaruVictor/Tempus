import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GenericResponse} from "../models/genericResponse";
import {BaseCategory} from "../models/categories/baseCategory";

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {

  constructor(private httpClient: HttpClient) { }

  getAll(){
    return this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/categories`);
  }
}
