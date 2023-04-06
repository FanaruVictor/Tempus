import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {Color} from "@angular-material-components/color-picker";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";

@Component({
  selector: 'app-create-or-edit-category-dialog',
  templateUrl: './create-or-edit-category-dialog.component.html',
  styleUrls: ['./create-or-edit-category-dialog.component.scss']
})
export class CreateOrEditCategoryDialogComponent implements OnInit {
  createOrEditForm = new UntypedFormGroup({
    name: new UntypedFormControl('', [Validators.required]),
    color: new UntypedFormControl('', [Validators.required])
  });
  isCreateMode: boolean = true;
  result: BaseCategory = {
    name: '',
    color: '',
    lastUpdatedAt: '',
    userId: '',
    id: ''
  };

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditCategoryDialogComponent>,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public category: BaseCategory,
  ) {
  }

  ngOnInit() {
    this.isCreateMode = !this.category;

    if (!this.isCreateMode) {
      this.createOrEditForm.controls['name'].setValue(this.category.name);
      let color = this.hexToRgb(this.category.color);
      if (color)
        this.createOrEditForm.controls['color'].setValue(new Color(color.r, color.g, color.b));
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
    this.router.navigate(['/categories']);
  }

  submit() {
    if (this.category) {
      this.result.id = this.category.id;
    }

    this.result.name = this.createOrEditForm.controls['name'].value;
    this.result.color = `#${this.createOrEditForm.controls['color'].value.hex}`;
  }

  hexToRgb(hex: string) {
    const shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
    hex = hex.replace(shorthandRegex, (m, r, g, b) => {
      return r + r + g + g + b + b;
    });
    const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
      r: parseInt(result[1], 16),
      g: parseInt(result[2], 16),
      b: parseInt(result[3], 16)
    } : null;
  }
}
