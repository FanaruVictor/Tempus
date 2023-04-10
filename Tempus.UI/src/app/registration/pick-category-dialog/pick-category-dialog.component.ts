import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';

@Component({
  selector: 'app-pick-category-dialog',
  templateUrl: './pick-category-dialog.component.html',
  styleUrls: ['./pick-category-dialog.component.scss'],
})
export class PickCategoryDialogComponent {
  categoryControl = new FormControl<BaseCategory | null>(
    null,
    Validators.required
  );

  constructor(
    public dialogRef: MatDialogRef<PickCategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public categories: BaseCategory[]
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
