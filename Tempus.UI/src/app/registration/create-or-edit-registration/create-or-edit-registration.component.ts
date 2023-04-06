import {AfterViewInit, Component, HostBinding, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {filter, first} from "rxjs";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";
import {CategoryApiService} from "../../_services/category.api.service";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {UpdateRegistrationCommandData} from "../../_commons/models/registrations/updateRegistrationCommandData";
import {CreateRegistrationCommandData} from "../../_commons/models/registrations/createRegistrationCommandData";
import {NotificationService} from "../../_services/notification.service";
import {QuillEditorComponent} from "ngx-quill";
import Quill from 'quill'
import ImageResize from 'quill-image-resize-module'
import {RegistrationDetails} from "../../_commons/models/registrations/registrationDetails";
import {RegistrationOverview} from "../../_commons/models/registrations/registrationOverview";
import {log} from "console";

Quill.register('modules/imageResize', ImageResize)

@Component({
  selector: 'app-create-or-edit-registration',
  templateUrl: './create-or-edit-registration.component.html',
  styleUrls: ['./create-or-edit-registration.component.scss']
})
export class CreateOrEditRegistrationComponent implements OnInit, AfterViewInit {
  @HostBinding('class.full-view') isActive = false;
  categories?: BaseCategory[];
  initialRegistration!: RegistrationOverview;
  id: string | undefined;
  isCreateMode: boolean = true;
  @ViewChild('editor') editor: QuillEditorComponent | undefined;
  content = '';
  format = 'html';
  createOrEditForm: FormGroup;
  lastUpdatedAt?: Date;
  categoryColor!: string;
  categoryId: string | null = null;
  page: string | undefined;

  toolbarOptions = [
    [{'font': []}],
    [{size: ['small', false, 'large', 'huge']}],
    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
    ['code-block'],
    [{'list': 'ordered'}, {'list': 'bullet'}, {'list': 'check'}],
    [{'script': 'sub'}, {'script': 'super'}],      // superscript/subscript
    [{'indent': '-1'}, {'indent': '+1'}],          // outdent/indent
    [{'color': []}, {'background': []}],          // dropdown with defaults from theme
    [{'align': []}],
    ['image'],
    ['formula']
  ];

  quillConfig = {
    imageResize: {},
    toolbar: {
      container: this.toolbarOptions,
    },

  };

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private registrationApiService: RegistrationApiService,
    private notificationService: NotificationService,
    private fb: FormBuilder
  ) {
    this.createOrEditForm = this.fb.group({
      description: ['', Validators.required],
      content: ['', Validators.required],
    });
  }

  ngOnInit() {
    const url = this.activatedRoute.snapshot.url;
    this.page = url[url.length - 1].path;
    if (this.page == 'edit-registrations-view') {
      this.isActive = true;
    }
    this.registrationApiService.registration
      .subscribe(x => {
          this.id = x.id;
          this.initialRegistration = x;
          if (this.id === '' || this.id === undefined) {
            this.activatedRoute.params.subscribe(params => {
              this.id = params['id'];
            });
          }

          this.isCreateMode = !this.id;

          if (!this.isCreateMode) {
            this.getById();
            return;
          }

          this.activatedRoute.queryParamMap.subscribe(params => {
            this.categoryId = params.get('categoryId');
            if (this.categoryId !== null) {
              this.getCategory();
            }
          });
        }
      );
  }

  getCategory( ) {
    if (!this.categoryId) {
      return;
    }

    this.categoryApiService
      .getById(this.categoryId)
      .pipe(first())
      .subscribe({
        next: response => {
          this.categoryColor = response.resource.color;
        }
      });
  }

  getById() {
    if (this.id === undefined) {
      return;
    }

    this.registrationApiService
      .getById(this.id)
      .pipe(first())
      .subscribe(response => {
        this.initialRegistration.content = response.resource.content;
        this.initialRegistration.description = response.resource.description;
        this.createOrEditForm.patchValue(response.resource);
        this.lastUpdatedAt = new Date(response.resource.lastUpdatedAt);
        this.categoryColor = response.resource.categoryColor;
      });
  }

  submit() {
    if (!this.isFormValid()) {
      this.notificationService.warn('No changes detected', 'Request completed');
      return;
    }
    if (!this.isCreateMode) {
      this.updateRegistration();
      return
    }
    this.createRegistration();
  }

  isFormValid() {
    if (!this.isCreateMode) {
      return this.initialRegistration?.content != this.createOrEditForm.get('content')?.value
        || this.initialRegistration?.description != this.createOrEditForm.get('description')?.value;
    }
    return this.createOrEditForm.valid;
  }

  updateRegistration() {
    let updateRegistrationCommandData: UpdateRegistrationCommandData = {
      id: this.id || '',
      description: this.createOrEditForm.get('description')?.value,
      content: this.createOrEditForm.get('content')?.value
    };

    this.update(updateRegistrationCommandData)
  }

  private

  update(updateRegistrationCommandData
           :
           UpdateRegistrationCommandData
  ) {
    this.registrationApiService
      .update(updateRegistrationCommandData)
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.lastUpdatedAt = new Date(result.resource.lastUpdatedAt);
        this.notificationService.succes('Registration updated successfully', 'Request completed');
        this.initialRegistration = {
          id: result.resource.id,
          description: result.resource.description,
          content: result.resource.content,
          categoryColor: result.resource.categoryColor,
          createdAt: this.initialRegistration.createdAt
        };
        this.registrationApiService.setRegistration(this.initialRegistration);

      });
  }

  createRegistration() {
    if (this.categoryId && this.categoryId !== '') {
      let registration: CreateRegistrationCommandData = {
        description: this.createOrEditForm.get('description')?.value,
        content: this.createOrEditForm.get('content')?.value,
        categoryId: this.categoryId
      }

      this.create(registration);
      return;
    }

    this.openDialog();
  }

  create(registration
           :
           CreateRegistrationCommandData
  ) {
    this.registrationApiService.create(registration)
      .pipe(filter(x => !!x))
      .subscribe(result => {
          this.router.navigate(['/registrations'])
          this.notificationService.succes('Registration created succesfully', 'Request completed')
        }
      );
  }

  openDialog() {
    this.categoryApiService.getAll()
      .pipe(filter(x => !!x))
      .subscribe({
        next: response => {
          this.categories = response.resource;

          const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
            data: this.categories,
          });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              let registration: CreateRegistrationCommandData = {
                description: this.createOrEditForm.get('description')?.value,
                content: this.createOrEditForm.get('content')?.value,
                categoryId: result.id
              };

              this.create(registration);
            }
          });
        }
      });
  }

  ngAfterViewInit()
    :
    void {
    let element = document.querySelector('.header') as HTMLElement;
    element.style.boxShadow = `0 4px 2 -2px ${this.categoryColor}`;
  }
}
