import { Pipe, PipeTransform } from '@angular/core';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';

@Pipe({ name: 'dateRange' })
export class DateRangePipe implements PipeTransform {
  transform<T extends { lastUpdatedAt: string }>(
    items: T[],
    dateRange: { start: Date | null; end: Date | null }
  ): any[] {
    if (dateRange.start === null || dateRange.end === null) {
      return items;
    }

    const start = new Date(dateRange.start).getTime();
    const end = dateRange.end?.getTime() ?? new Date();

    items = items.filter((it) => {
      const date = new Date(it.lastUpdatedAt).getTime();
      return date >= start && date <= end;
    });

    return items;
  }
}
