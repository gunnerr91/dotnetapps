import { ComponentFixture, TestBed } from "@angular/core/testing";
import { FetchDataComponent, WeatherForecast } from "./fetch-data.component";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { Type } from "@angular/core";
import { HttpClient } from "@angular/common/http";

// describe("fetch data component", () => {
//   let fixture: ComponentFixture<FetchDataComponent>;
//   let app: FetchDataComponent;
//   let httpMock: HttpTestingController;

//   beforeEach(async () => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule],
//       declarations: [FetchDataComponent],
//     });

//     await TestBed.compileComponents();

//     fixture = TestBed.createComponent(FetchDataComponent);
//     app = fixture.componentInstance;
//     httpMock = fixture.debugElement.injector.get<HttpTestingController>(HttpTestingController as Type<HttpTestingController>);

//     fixture.detectChanges();
//   });

//   afterEach(() => {
//     httpMock.verify();
//   });

//   it('testing http client request', () => {
//     const dummyResponse = [{dummy: "response"}];
//     const req = httpMock.expectOne()
//   });
// });

describe("testing isolated component", () => {
  let component: FetchDataComponent;
  let client: HttpClient;
  beforeEach(() => {
    client = jasmine.createSpyObj("HttpClient", ["get"]);
    component = new FetchDataComponent(client, "someurl");
  });

  it("should return expected response", () => {
    // client.get = () : WeatherForecast[] => [{date: "asd", summary: "asd", temperatureC: 12, temperatureF: 15}]
  });
});
