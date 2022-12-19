import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {BaseCategory} from "../../commons/models/categories/baseCategory";
import {MatDialog} from "@angular/material/dialog";
import {CategoryApiService} from "../../commons/services/category.api.service";
import {RegistrationApiService} from "../../commons/services/registration.api.service";
import {CreateRegistration} from "../../commons/models/registrations/createRegistration";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {Observable, Subject, take} from "rxjs";

@Component({
  selector: 'app-create-or-edit-registration',
  templateUrl: './create-or-edit-registration.component.html',
  styleUrls: ['./create-or-edit-registration.component.scss']
})
export class CreateOrEditRegistrationComponent {
  categories?: BaseCategory[];
  createOrEditForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    content: new FormControl('', [Validators.required])
  })

  constructor(
    private router: Router,
    private activedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private registrationApiService: RegistrationApiService
  ) {
  }

  create() {
    if (!this.createOrEditForm.valid) {
      this.router.navigate(['/registrations/overview']);
    } else {
      let catId = this.activedRoute.snapshot.paramMap.get('categoryId') ?? '';
      if (catId !== '') {
        let registration : CreateRegistration = {
          title: this.createOrEditForm.controls['title'].value,
          content: this.createOrEditForm.controls['content'].value,
          categoryId: catId
        }

        this.createRegistration(registration);
        return;
      }

      this.openDialog();
    }
  }

  createRegistration(registration: CreateRegistration){
    this.registrationApiService.create(registration).subscribe(result =>
      this.router.navigate(['/registrations', result.resource.id])
    );
  }

  openDialog() {
    this.categoryApiService.getAll()
      .subscribe({
        next: response => {
          this.categories = response.resource;

          const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
            data: this.categories,
          });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              let registration : CreateRegistration = {
                title: this.createOrEditForm.controls['title'].value,
                content: this.createOrEditForm.controls['content'].value,
                categoryId: result.id
              };

              this.createRegistration(registration);
            }
          });
        }
      });
  }
}
