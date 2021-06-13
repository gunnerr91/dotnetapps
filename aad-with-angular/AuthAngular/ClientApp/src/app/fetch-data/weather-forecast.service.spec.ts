import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { TestBed, inject } from "@angular/core/testing";
import { getBaseUrl } from "src/main";

import {
  WeatherForecast,
  WeatherForecastService,
} from "./weather-forecast.service";

describe("WeatherForecastService", () => {
  let service: WeatherForecastService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        WeatherForecastService,
        { provide: "BASE_URL", useValue: "http://example.com/api/" },
      ],
    });
    service = TestBed.get(WeatherForecastService);
    httpMock = TestBed.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("retrieves weather forecasts from the api", () => {
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

    service.getForecasts().subscribe((res) => {
      expect(res.length).toBe(2);
      expect(res).toEqual(dummyForecasts);
    });

    const request = httpMock.expectOne(
      "http://example.com/api/" + "weatherforecast"
    );
    expect(request.request.method).toBe("GET");

    request.flush(dummyForecasts);
  });
});
