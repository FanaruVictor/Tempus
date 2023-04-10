import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  groupId: BehaviorSubject<string | undefined>;
  currentGroupId: Observable<string | undefined>;
  constructor() {
    this.groupId = new BehaviorSubject<string | undefined>(undefined);
    this.currentGroupId = this.groupId.asObservable();
  }

  setGroupId(groupId: string | undefined) {
    this.groupId.next(groupId);
  }
}
