import {RegistrationsOverviewComponent} from "./registrations-overview.component";
import {fakeAsync, TestBed, tick} from "@angular/core/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatDialogModule} from "@angular/material/dialog";
import {RegistrationApiService} from "../../commons/services/registration.api.service";
import * as Rx from 'rxjs';
import {CategoryApiService} from "../../commons/services/category.api.service";
import {delay} from "rxjs/operators";
import {GenericResponse} from "../../commons/models/genericResponse";
import {DetailedRegistration} from "../../commons/models/registrations/detailedRegistration";

describe('Component: RegistrationsOverview', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrationsOverviewComponent],
      providers: [
         RegistrationApiService,
        CategoryApiService
      ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([]),
        MatDialogModule
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    let fixture = TestBed.createComponent(RegistrationsOverviewComponent);
    let app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  })

  it('should call getAll and response as empty array', fakeAsync(() => {
    const fixture = TestBed.createComponent(RegistrationsOverviewComponent);
    const component = fixture.debugElement.componentInstance;
    const service = fixture.debugElement.injector.get(RegistrationApiService);
    let response : GenericResponse<DetailedRegistration[]> = {
      resource: [
      ],
      errors: []
    }
    spyOn(service,"getAll").and.callFake(() => {
      return Rx.of(response).pipe(delay(100));
    });

    component.getAll();
    tick(100);
    expect(component.registrations).toEqual([]);
  }));

  it('should call getAll and response as array', fakeAsync(() => {
    const fixture = TestBed.createComponent(RegistrationsOverviewComponent);
    const component = fixture.debugElement.componentInstance;
    const service = fixture.debugElement.injector.get(RegistrationApiService);
    let response : GenericResponse<DetailedRegistration[]> = {
      resource: [
        {
          id: 'id',
          title: 'title',
          content: 'content',
          lastUpdatedAt: '22.12.2022',
          createdAt: '22.12.2022',
          categoryColor: 'color'
        }
      ],
      errors: []
    }
    spyOn(service,"getAll").and.callFake(() => {
      return Rx.of(response).pipe(delay(1000));
    });

    component.getAll();
    tick(1000);
    expect(component.registrations).toEqual([
      {
        id: 'id',
        title: 'title',
        content: 'content',
        lastUpdatedAt: '22.12.2022',
        createdAt: '22.12.2022',
        categoryColor: 'color'
      }
    ]);
  }));

})
