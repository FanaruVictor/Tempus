import { Injectable } from '@angular/core';
import {Photo} from "../_commons/models/photo/photo";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {GenericResponse} from "../_commons/models/genericResponse";

@Injectable({
  providedIn: 'root'
})
export class ProfilePhotoApiService {
  apiUrl = `${environment.apiUrl}/v1/profilePhoto`;

  constructor(private httpClient: HttpClient) { }

  addPhoto(file: File) {
    let formData = new FormData();
    formData.append('image', file);

    return this.httpClient.post<any>(this.apiUrl, formData);
  }

  updatePhoto(currentPhotoId: string, file: File) {
    let formData = new FormData();
    formData.append('id', currentPhotoId);
    formData.append('image', file);

    return this.httpClient.put<any>(this.apiUrl, formData);
  }

  ChangeProfilePicture(file: File, photo: Photo | undefined) {
    if (!!photo) {
      return this.updatePhoto(photo.id, file);
    }
    return this.addPhoto(file);
  }

  deleteProfilePicture(id: string){
    return this.httpClient.delete<GenericResponse<any>>(this.apiUrl, {body: {id: id}});
  }
}
