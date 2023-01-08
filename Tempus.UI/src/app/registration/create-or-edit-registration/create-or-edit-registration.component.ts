import {Component, OnInit} from '@angular/core';
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {first} from "rxjs";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";
import {CategoryApiService} from "../../_services/category.api.service";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {UpdateRegistrationCommandData} from "../../_commons/models/registrations/updateRegistrationCommandData";
import {CreateRegistrationCommandData} from "../../_commons/models/registrations/createRegistrationCommandData";

@Component({
  selector: 'app-create-or-edit-registration',
  templateUrl: './create-or-edit-registration.component.html',
  styleUrls: ['./create-or-edit-registration.component.scss']
})
export class CreateOrEditRegistrationComponent implements OnInit {
  categories?: BaseCategory[];
  id: string = '';
  isCreateMode: boolean = true;
  createOrEditForm = new UntypedFormGroup({
    title: new UntypedFormControl('', [Validators.required]),
    content: new UntypedFormControl('', [Validators.required])
  })

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private registrationApiService: RegistrationApiService
  ) {
  }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.isCreateMode = !this.id;

    if (!this.isCreateMode) {
      this.registrationApiService
        .getById(this.id)
        .pipe(first())
        .subscribe(response => {
          this.createOrEditForm.patchValue(response.resource);
        });
    }
  }

  cancel(){
    if(!this.isCreateMode){
      this.router.navigate(['/registrations', this.id]);
      return
    }
    this.router.navigate(['/registrations/overview']);
  }

  submit() {
    if(!this.isCreateMode){
      this.update();
      return
    }
    this.create();
  }

  update(){
    let updateRegistrationCommandData: UpdateRegistrationCommandData = {
      id: this.id,
      title: this.createOrEditForm.controls['title'].value,
      content: this.createOrEditForm.controls['content'].value
    };

    this.registrationApiService
      .update(updateRegistrationCommandData)
      .subscribe(result =>{
        this.router.navigate(['/registrations', result.resource.id]);
      })
  }

  create() {

    let catId = this.activatedRoute.snapshot.paramMap.get('categoryId') ?? '';
    if (catId !== '') {
      let registration: CreateRegistrationCommandData = {
        title: this.createOrEditForm.controls['title'].value,
        content: this.createOrEditForm.controls['content'].value,
        categoryId: catId
      }

      this.createRegistration(registration);
      return;
    }

    this.openDialog();
  }

  createRegistration(registration: CreateRegistrationCommandData) {
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
              let registration: CreateRegistrationCommandData = {
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
