import {Component, Inject} from '@angular/core';
import {Photo} from "../../../_commons/models/photo/photo";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {FileItem, FileUploader} from 'ng2-file-upload';
import {environment} from "../../../../environments/environment";
import {DomSanitizer, SafeUrl} from "@angular/platform-browser";

@Component({
  selector: 'app-add-or-update-profile-photo',
  templateUrl: './add-or-update-profile-photo.component.html',
  styleUrls: ['./add-or-update-profile-photo.component.scss']
})
export class AddOrUpdateProfilePhotoComponent {
  result: Photo = {
    id: "",
    publicId: "",
    url: ""
  };
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean = false;
  response: string = "";
  photoUrl = `${environment.apiUrl}/v1/profilePhoto`
  item?: FileItem;
  url?: SafeUrl;

  constructor(
    public dialogRef: MatDialogRef<AddOrUpdateProfilePhotoComponent>,
    @Inject(MAT_DIALOG_DATA) public photo: Photo,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {
    {
      this.uploader = new FileUploader({
        url: this.photoUrl,
        authToken: `Bearer ${localStorage.getItem("authorizationToken")}`,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024
      });


      this.uploader.onAfterAddingFile = (file) => {
        file.withCredentials = false;
        this.item = file;
        this.url = this.getUrl();
      };
    }
  }

  fileOverBase(e: any):
    void {
    this.hasBaseDropZoneOver = e;
  }


  onNoClick(): void {
    this.dialogRef.close();
    this.router.navigate(['/account/profile']);
  }

  getUrl(){
    if(!!this.item){
      const file : any = this.item.file.rawFile;
      return this.sanitizer.bypassSecurityTrustUrl(window.URL.createObjectURL(file))
    }

    return "";
  }


}
