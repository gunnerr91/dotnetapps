import { HttpTestingController } from "@angular/common/http/testing";
import { FetchDataComponent } from "./fetch-data.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { DebugElement } from "@angular/core";
import {
  WeatherForecast,
  WeatherForecastService,
} from "./weather-forecast.service";
import { of } from "rxjs";

fdescribe("fetch data component", () => {
  let fetchDataComponent: FetchDataComponent;
  let fixture: ComponentFixture<FetchDataComponent>;
  let de: DebugElement;

  let serviceStub: any;
  let service: WeatherForecastService;
  let spy: jasmine.Spy;

  const dummyForecasts: WeatherForecast[] = [
    {
      date: "12/12/12",
      temperatureC: 15,
      temperatureF: 15,
      summary: "summay",
    },
    {
      date: "12/12/12",
      temperatureC: 15,
      temperatureF: 15,
      summary: "summay",
    },
  ];

  beforeEach(async () => {
    serviceStub = {
      getForecasts: () => of(dummyForecasts),
    };
    await TestBed.configureTestingModule({
      declarations: [FetchDataComponent],
      providers: [{ provide: WeatherForecastService, useValue: serviceStub }],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchDataComponent);
    fetchDataComponent = fixture.componentInstance;
    de = fixture.debugElement;
    service = de.injector.get(WeatherForecastService);

    spy = spyOn(service, "getForecasts").and.returnValue(of(dummyForecasts));
    fixture.detectChanges();
  });

  it("returns truthy for forecast property", () => {
    expect(fetchDataComponent.forecasts).toBeTruthy();
  });

  it("populates property value on init", () => {
    fetchDataComponent.ngOnInit();
    expect(fetchDataComponent.forecasts).toBe(dummyForecasts);
  });
});
