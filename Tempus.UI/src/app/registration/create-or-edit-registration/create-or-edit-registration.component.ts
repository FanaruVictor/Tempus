import { Component, HostBinding, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PickCategoryDialogComponent } from '../pick-category-dialog/pick-category-dialog.component';
import { filter, first } from 'rxjs';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';
import { CategoryApiService } from '../../_services/category.api.service';
import { RegistrationApiService } from '../../_services/registration.api.service';
import { UpdateRegistrationCommandData } from '../../_commons/models/registrations/updateRegistrationCommandData';
import { CreateRegistrationCommandData } from '../../_commons/models/registrations/createRegistrationCommandData';
import { NotificationService } from '../../_services/notification.service';
import { QuillEditorComponent } from 'ngx-quill';
import Quill from 'quill';
import ImageResize from 'quill-image-resize-module';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';
import { GroupService } from 'src/app/_services/group/group.service';

Quill.register('modules/imageResize', ImageResize);

export interface Page {
  id: string;
  registrationId: string;
}

@Component({
  selector: 'app-create-or-edit-registration',
  templateUrl: './create-or-edit-registration.component.html',
  styleUrls: ['./create-or-edit-registration.component.scss'],
})
export class CreateOrEditRegistrationComponent implements OnInit {
  @HostBinding('class.full-view') isActive = true;
  categories?: BaseCategory[];
  initialRegistration!: RegistrationOverview;
  id: string | undefined;
  isCreateMode: boolean = true;
  @ViewChild('editor') editor: QuillEditorComponent | undefined;
  content = '';
  format = 'html';
  createOrEditForm: FormGroup;
  createdAt?: Date;
  categoryColor!: string;
  categoryId: string | null = null;
  mode: string | undefined;

  groupId: string | undefined;
  toolbarOptions = [
    [{ font: [] }],
    [{ size: ['small', false, 'large', 'huge'] }],
    ['bold', 'italic', 'underline', 'strike'], // toggled buttons
    ['code-block'],
    [{ list: 'ordered' }, { list: 'bullet' }, { list: 'check' }],
    [{ script: 'sub' }, { script: 'super' }], // superscript/subscript
    [{ indent: '-1' }, { indent: '+1' }], // outdent/indent
    [{ color: [] }, { background: [] }], // dropdown with defaults from theme
    [{ align: [] }],
    ['image'],
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
    private fb: FormBuilder,
    private groupService: GroupService
  ) {
    this.createOrEditForm = this.fb.group({
      description: ['', Validators.required],
      content: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.groupService.currentGroupId.subscribe((x) => {
      this.groupId = x;
    });

    this.activatedRoute.url.subscribe((x) => {
      this.mode = x[x.length - 1].path;
      if (this.mode == 'edit-registrations-view') {
        this.isActive = false;
      }
    });

    this.activatedRoute.queryParamMap.subscribe((params) => {
      this.categoryId = params.get('categoryId');
      if (this.categoryId !== null) {
        this.getCategory();
      }
    });

    this.registrationApiService.registration.subscribe((x) => {
      if (this.mode == 'create') {
        return;
      }
      this.id = x.id;
      this.initialRegistration = x;

      if (this.id === '' || this.id === undefined) {
        this.id = this.activatedRoute.snapshot.params['id'];
        this.isCreateMode = !this.id;

        if (!this.isCreateMode) {
          this.getById();
          return;
        }
      }
      this.isCreateMode = !this.id;
      this.createOrEditForm.patchValue(this.initialRegistration);
    });
  }

  getCategory() {
    if (!this.categoryId) {
      return;
    }

    this.categoryApiService
      .getById(this.categoryId)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.categoryColor = response.resource.color;
        },
      });
  }

  getById() {
    if (this.id === undefined) {
      return;
    }

    this.registrationApiService
      .getById(this.id, this.groupId)
      .pipe(first())
      .subscribe((response) => {
        this.initialRegistration.content = response.resource.content;
        this.initialRegistration.description = response.resource.description;
        this.createOrEditForm.patchValue(response.resource);
        this.createdAt = new Date(response.resource.createdAt);
        this.categoryColor = response.resource.categoryColor;
      });
  }

  submit() {
    if (!this.isFormChanged() && !this.isCreateMode) {
      this.notificationService.warn('No changes detected', 'Request completed');
      return;
    }

    if (!this.isFormValid()) {
      return;
    }

    if (!this.isCreateMode) {
      this.updateRegistration();
      return;
    }
    this.createRegistration();
  }

  isFormValid() {
    if (!this.createOrEditForm.valid) {
      let messages: string[] = [];
      if (this.createOrEditForm.get('description')?.errors?.['required']) {
        messages.push('Description is required.');
      }
      if (this.createOrEditForm.get('content')?.errors?.['required']) {
        messages.push('Content is required.');
      }
      this.notificationService.error(
        messages,
        'Please complete all the fields before submitting.'
      );
      return false;
    }
    return true;
  }

  isFormChanged() {
    if (!this.isCreateMode) {
      return (
        this.initialRegistration?.content !=
          this.createOrEditForm.get('content')?.value ||
        this.initialRegistration?.description !=
          this.createOrEditForm.get('description')?.value
      );
    }
    return this.createOrEditForm.valid;
  }

  updateRegistration() {
    let updateRegistrationCommandData: UpdateRegistrationCommandData = {
      id: this.id || '',
      description: this.createOrEditForm.get('description')?.value,
      content: this.createOrEditForm.get('content')?.value,
    };
    this.update(updateRegistrationCommandData);
  }

  private update(updateRegistrationCommandData: UpdateRegistrationCommandData) {
    this.registrationApiService
      .update(updateRegistrationCommandData)
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        this.createdAt = new Date(result.resource.createdAt);
        this.notificationService.succes(
          'Registration updated successfully',
          'Request completed'
        );
        this.initialRegistration = {
          id: result.resource.id,
          description: result.resource.description,
          content: result.resource.content,
          categoryColor: result.resource.categoryColor,
          lastUpdatedAt: new Date().toString(),
        };
        this.registrationApiService.setRegistration(this.initialRegistration);
        this.redirect();
      });
  }

  createRegistration() {
    if (this.categoryId && this.categoryId !== '') {
      let registration: CreateRegistrationCommandData = {
        description: this.createOrEditForm.get('description')?.value,
        content: this.createOrEditForm.get('content')?.value,
        categoryId: this.categoryId,
      };

      this.create(registration);
      return;
    }

    this.openDialog();
  }

  create(registration: CreateRegistrationCommandData) {
    this.registrationApiService
      .create(registration)
      .pipe(filter((x) => !!x))
      .subscribe((_) => {
        if (!!this.groupId) {
          this.router.navigate(['/groups', this.groupId, 'registrations']);
        } else {
          this.router.navigate(['/registrations']);
        }
        this.notificationService.succes(
          'Registration created succesfully',
          'Request completed'
        );
      });
  }

  redirect() {
    if (this.mode == 'edit-full-view') {
      if (!!this.groupId) {
        this.router.navigate(['/groups', this.groupId, 'registrations']);
      } else {
        this.router.navigate(['/registrations']);
      }
    }
  }

  openDialog() {
    //TODO: it should be groupId insead of undefined
    this.categoryApiService
      .getAll(undefined)
      .pipe(filter((x) => !!x))
      .subscribe({
        next: (response) => {
          this.categories = response.resource;

          const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
            data: this.categories,
          });
          dialogRef.afterClosed().subscribe((result) => {
            if (result) {
              let registration: CreateRegistrationCommandData = {
                description: this.createOrEditForm.get('description')?.value,
                content: this.createOrEditForm.get('content')?.value,
                categoryId: result.id,
              };

              this.create(registration);
            }
          });
        },
      });
  }

  cancel() {
    if (!!this.groupId) {
      this.router.navigate(['/groups', this.groupId, 'registrations']);
    } else {
      this.router.navigate(['/registrations']);
    }
  }
}
