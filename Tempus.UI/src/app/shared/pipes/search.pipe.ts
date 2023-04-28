import { Pipe, PipeTransform } from '@angular/core';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';

@Pipe({ name: 'search' })
export class SearchPipe implements PipeTransform {
  transform(
    items: any,
    searchData: { searchText: string; property: string }
  ): any[] {
    let searchText = searchData.searchText.toLowerCase();
    searchText = searchText.trim();
    const property = searchData.property;

    if (!items) {
      return [];
    }
    if (!searchText) {
      return items;
    }

    items = items.filter((it) => {
      return it[property].toLowerCase().includes(searchText);
    });

    return items;
  }
}
