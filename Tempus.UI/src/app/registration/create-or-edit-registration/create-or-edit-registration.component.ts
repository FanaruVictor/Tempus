import {Component, OnInit, ViewChild} from '@angular/core';
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
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

@Component({
  selector: 'app-create-or-edit-registration',
  templateUrl: './create-or-edit-registration.component.html',
  styleUrls: ['./create-or-edit-registration.component.scss']
})
export class CreateOrEditRegistrationComponent implements OnInit {
  categories?: BaseCategory[];
  id: string = '';
  isCreateMode: boolean = true;
  @ViewChild('editor') editor: QuillEditorComponent | undefined;
  content = '';
  format = 'html';
  createOrEditForm = new UntypedFormGroup({
    title: new UntypedFormControl('', [Validators.required]),
    content: new UntypedFormControl('', [Validators.required])
  });

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
    ['image']
  ];

  quillConfig = {
    toolbar: {
      container: this.toolbarOptions,
      handlers: {
        source: () => {
          this.formatChange();
        },
      },
    }
  };

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private registrationApiService: RegistrationApiService,
    private notificationService: NotificationService
  ) {

  }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.isCreateMode = !this.id;

    if (!this.isCreateMode) {
      this.registrationApiService
        .getById(this.id)
        .pipe(first())
        .subscribe({
          next: response => {
            this.createOrEditForm.patchValue(response.resource);
          }
        });
    }
  }

  formatChange() {
    this.format = this.format === 'html' ? 'text' : 'html';

    if (this.format === 'text' && this.editor) {
      const htmlText = this.createOrEditForm.controls['content'].value;
      this.editor.quillEditor.setText(htmlText);
    } else if (this.format === 'html' && this.editor) {
      const htmlText = this.createOrEditForm.controls['content'].value;
      this.editor.quillEditor.setText('');
      this.editor.quillEditor.pasteHTML(0, htmlText);
    }
  }

  cancel() {
    if (!this.isCreateMode) {
      this.router.navigate(['/registrations', this.id]);
      return
    }
    this.router.navigate(['/registrations/overview']);
  }

  submit() {
    this.validateForm();
    if (!this.isCreateMode) {
      this.update();
      return
    }
    this.create();
  }

  validateForm() {

  }

  update() {
    let updateRegistrationCommandData: UpdateRegistrationCommandData = {
      id: this.id,
      title: this.createOrEditForm.controls['title'].value,
      content: this.createOrEditForm.controls['content'].value
    };

    this.registrationApiService
      .update(updateRegistrationCommandData)
      .pipe(filter(x => !!x))
      .subscribe({
        next: result => {
          this.router.navigate(['/registrations', result.resource.id]);
          this.notificationService.succes('Registration updated successfully', 'Request completed')
        }
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

  createRegistration(registration
                       :
                       CreateRegistrationCommandData
  ) {
    this.registrationApiService.create(registration)
      .pipe(filter(x => !!x))
      .subscribe({
          next: result => {
            this.router.navigate(['/registrations', result.resource.id])
            this.notificationService.succes('Registration created succesfully', 'Request completed')
          }
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
