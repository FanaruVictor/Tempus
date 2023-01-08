import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {UntypedFormControl, Validators} from "@angular/forms";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";

@Component({
  selector: 'app-pick-category-dialog',
  templateUrl: './pick-category-dialog.component.html',
  styleUrls: ['./pick-category-dialog.component.scss']
})
export class PickCategoryDialogComponent {
  categoryControl = new UntypedFormControl(null, Validators.required);
  constructor(
    public dialogRef: MatDialogRef<PickCategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public categories:BaseCategory[],
    private router: Router
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
    this.router.navigate(['/registrations/overview']);
  }
}
