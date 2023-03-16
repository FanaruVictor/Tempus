import { Pipe, PipeTransform } from '@angular/core';
import {RegistrationOverview} from "../models/registrations/registrationOverview";

@Pipe({ name: 'search' })
export class SearchPipe implements PipeTransform {
  transform(items: RegistrationOverview[], searchText: string): any[] {
    if (!items) {
      return [];
    }
    if (!searchText) {
      return items;
    }

    searchText = searchText.toLocaleLowerCase();

    return items.filter(it => {
      return it.description.toLocaleLowerCase().startsWith(searchText);
    });
  }
}
