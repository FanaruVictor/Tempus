import {fakeAsync, TestBed, tick} from "@angular/core/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatDialogModule} from "@angular/material/dialog";
import * as Rx from 'rxjs';
import {delay} from "rxjs/operators";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {CategoryApiService} from "../../_services/category.api.service";
import {GenericResponse} from "../../_commons/models/genericResponse";
import {RegistrationOverview} from "../../_commons/models/registrations/registrationOverview";
import {RegistrationsComponent} from "./registrations.component";

describe('Component: RegistrationsOverview', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrationsComponent],
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
    let fixture = TestBed.createComponent(RegistrationsComponent);
    let app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  })

  it('should call getAll and response as empty array', fakeAsync(() => {
    const fixture = TestBed.createComponent(RegistrationsComponent);
    const component = fixture.debugElement.componentInstance;
    const service = fixture.debugElement.injector.get(RegistrationApiService);
    let response: GenericResponse<RegistrationOverview[]> = {
      resource: [],
      errors: []
    }
    spyOn(service, "getAll").and.callFake(() => {
      return Rx.of(response).pipe(delay(100));
    });

    component.getAll();
    tick(100);
    expect(component.registrations).toEqual([]);
  }));

  it('should call getAll and response as array', fakeAsync(() => {
    const fixture = TestBed.createComponent(RegistrationsComponent);
    const component = fixture.debugElement.componentInstance;
    const service = fixture.debugElement.injector.get(RegistrationApiService);
    let response: GenericResponse<RegistrationOverview[]> = {
      resource: [
        {
          id: 'id',
          description: 'title',
          createdAt: '22.12.2022',
          categoryColor: 'color'
        }
      ],
      errors: []
    }
    spyOn(service, "getAll").and.callFake(() => {
      return Rx.of(response).pipe(delay(1000));
    });

    component.getAll();
    tick(1000);
    expect(component.registrations).toEqual([
      {
        id: 'id',
        description: 'title',
        createdAt: '22.12.2022',
        categoryColor: 'color'
      }
    ]);
  }));

})
