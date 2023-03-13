import { Pipe, PipeTransform } from '@angular/core';
import {RegistrationOverview} from "../models/registrations/registrationOverview";

@Pipe({ name: 'dateRange' })
export class DateRangePipe implements PipeTransform {
  transform(items: RegistrationOverview[], dateRange: { start: Date | null, end: Date | null }): any[] {
   if(dateRange.start === null){
     return items;
   }
   else if(dateRange.end === null){
     const start = dateRange.start?.getTime() ?? new Date();
     return items.filter(x => {
       const date = new Date(x.createdAt).getTime();
       return date == start;
     })
   }
   else{
     const start = new Date(dateRange.start).getTime();
     const end = dateRange.end?.getTime() ?? new Date();

     return items.filter(it => {
       let show = true;
       const date = new Date(it.createdAt).getTime();
       return date >= start && date <= end;
     });
   }
  }
}
