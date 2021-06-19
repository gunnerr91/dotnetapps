import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {
  WeatherForecast,
  WeatherForecastService,
} from "./weather-forecast.service";

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  constructor(private weatherService: WeatherForecastService) {}

  ngOnInit(): void {
    this.weatherService.getForecasts().subscribe((res) => {
      this.forecasts = res;
    });
  }
}
