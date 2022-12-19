import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {GenericResponse} from "../../commons/models/genericResponse";
import {BaseCategory} from "../../commons/models/categories/baseCategory";

@Component({
  selector: 'app-categories-overview',
  templateUrl: './categories-overview.component.html',
  styleUrls: ['./categories-overview.component.scss']
})
export class CategoriesOverviewComponent {
  categories?: BaseCategory[] ;
  constructor(private httpClient: HttpClient, private router: Router) {
  }
  ngOnInit(): void {
    this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/categories`)
      .subscribe({
        next: response => {
          this.categories = response.resource;
        }
      });
  }

  delete(id: string){
    this.httpClient.delete<GenericResponse<string>>(`https://localhost:7077/api/categories/${id}`)
      .subscribe(response => {
        let id = response.resource;
        this.categories = this.categories?.filter(x => x.id !== id);
        let last = this.categories?.slice(-1)[0];
        this.router.navigate([`/categories`, last?.id || 'create'])
      });
  }
}
