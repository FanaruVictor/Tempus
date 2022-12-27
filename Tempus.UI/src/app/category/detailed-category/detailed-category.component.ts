import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {BaseCategory} from "../../commons/models/categories/baseCategory";
import {GenericResponse} from "../../commons/models/genericResponse";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CreateCategory} from "../../commons/models/categories/CreateCategory";
import {UpdateCategory} from "../../commons/models/categories/updateCategory";

@Component({
  selector: 'app-category',
  templateUrl: './detailed-category.component.html',
  styleUrls: ['./detailed-category.component.scss']
})
export class DetailedCategoryComponent implements OnInit {
  isEditForm = false;
  isCreateForm = false;
  category?: BaseCategory;
  categoryForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    color: new FormControl('', [Validators.required])
  });

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    let url = this.router.url;
    if (this.isCreate(url)) {
      this.isCreateForm = true;
    } else if (this.isEdit(url)) {
      this.isEditForm = true;
      let id = url.substring(url.lastIndexOf('/') + 1);
      this.getCategory(id);
    } else {
      let id = url.substring(url.lastIndexOf('/') + 1);
      if (id && id != 'categories') {
        this.getCategory(id);
      }
    }
  }

  isCreate(url: string): boolean {
    return url.includes('/create');
  }

  isEdit(url: string): boolean {
    return url.includes('/edit');
  }

  getCategory(id: string) {
    this.httpClient.get<GenericResponse<BaseCategory>>(`https://localhost:7077/api/v1/categories/${id}`)
      .subscribe({
        next: response => {
          this.category = response.resource;
          if (this.category == undefined) {
            console.log("error")
          } else {
            this.categoryForm.controls['name'].setValue(this.category.name);
            this.categoryForm.controls['color'].setValue(this.category.color);
          }
        },
        error: errors => {
          console.log(errors.errors)
        }
      });
  }

  onSubmit() {
    if (this.category) {
      console.log(this.category);
      let category: UpdateCategory = {
        id: this.category.id,
        name: this.categoryForm.controls['name'].value,
        color: this.categoryForm.controls['color'].value
      }
      this.httpClient.put<GenericResponse<BaseCategory>>('`https://localhost:7077/api/v1/categories/', category)
        .subscribe({
          next: response => {
            this.router.navigate([`/categories/${response.resource.id}`])
          },
            error: errors => {
            console.log(errors.errors)
          }
        });
    } else {
      let category: CreateCategory = {
        userId: 'fa2c9efa-e576-44a9-a6e5-08dacd729e8d',
        name: this.categoryForm.controls['name'].value,
        color: this.categoryForm.controls['color'].value
      };
      this.httpClient.post<GenericResponse<BaseCategory>>('https://localhost:7077/api/v1/categories/', category)
        .subscribe(response => {
          this.router.navigate([`/categories/${response.resource.id}`]);
        });
    }
  }

  cancel() {
    console.log(this.isEditForm)
    if (this.isEditForm)
      this.router.navigate([`/categories/${this.category?.id}`])
    else
      this.router.navigate(['/categories/overview']);
  }
}
