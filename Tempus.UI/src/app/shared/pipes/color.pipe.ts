import { Pipe, PipeTransform } from '@angular/core';
import {RegistrationOverview} from "../../_commons/models/registrations/registrationOverview";

@Pipe({
  name: 'color'
})
export class ColorPipe implements PipeTransform {

  transform(items: RegistrationOverview[], colors: any): any[] {
    if(colors.length === 0){
        return items;
    }

    items = items.filter(x => colors.includes(x.categoryColor));
    return items;
  }
}
