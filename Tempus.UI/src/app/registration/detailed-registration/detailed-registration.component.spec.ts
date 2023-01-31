import {fakeAsync, TestBed, tick} from "@angular/core/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {RegistrationApiService} from "../../commons/services/registration.api.service";
import {DetailedRegistrationComponent} from "./detailed-registration.component";
import {ActivatedRoute} from "@angular/router";
import * as Rx from 'rxjs';
import {BaseRegistration} from "../../commons/models/registrations/baseRegistration";
import {GenericResponse} from "../../commons/models/genericResponse";
import {delay} from "rxjs/operators";

describe('Component: DetailedRegistration', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetailedRegistrationComponent],
      providers: [
        RegistrationApiService,
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {params: {id: '1'}}
          }
        }
      ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([]),
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    let fixture = TestBed.createComponent(DetailedRegistrationComponent);
    let app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  })

  it('should call ngOnInit', () => {
    const fixture = TestBed.createComponent(DetailedRegistrationComponent);
    const component = fixture.componentInstance;
    let spy_getRegistration = spyOn(component,"getRegistration").and.returnValue(undefined);
    component.ngOnInit();
    expect(component.id).toEqual('1');
  })

  it('should call getRegistration and response with object', fakeAsync(() => {
    const fixture = TestBed.createComponent(DetailedRegistrationComponent);
    const component = fixture.componentInstance;
    const service = fixture.debugElement.injector.get(RegistrationApiService);
    let response : GenericResponse<BaseRegistration> = {
      resource: {
        id: 'id',
        title: 'title',
        content: 'content',
        lastUpdatedAt: '22.12.2022'
      },
      errors: []
    }
    spyOn(service,"getById").and.callFake(() => {
      return Rx.of(response).pipe(delay(100));
    });

    component.getRegistration('1');
    tick(100);
    expect(component.registration).toEqual(response.resource);
  }));

  // it('should call getAll and response as array', fakeAsync(() => {
  //   const fixture = TestBed.createComponent(RegistrationsOverviewComponent);
  //   const component = fixture.debugElement.componentInstance;
  //   const service = fixture.debugElement.injector.get(RegistrationApiService);
  //   let response : GenericResponse<DetailedRegistration[]> = {
  //     resource: [
  //       {
  //         id: 'id',
  //         title: 'title',
  //         content: 'content',
  //         lastUpdatedAt: '22.12.2022',
  //         createdAt: '22.12.2022',
  //         categoryColor: 'color'
  //       }
  //     ],
  //     errors: []
  //   }
  //   spyOn(service,"getAll").and.callFake(() => {
  //     return Rx.of(response).pipe(delay(1000));
  //   });
  //
  //   component.getAll();
  //   tick(1000);
  //   expect(component.registrations).toEqual([
  //     {
  //       id: 'id',
  //       title: 'title',
  //       content: 'content',
  //       lastUpdatedAt: '22.12.2022',
  //       createdAt: '22.12.2022',
  //       categoryColor: 'color'
  //     }
  //   ]);
  // }));

})
