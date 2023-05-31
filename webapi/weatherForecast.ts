export interface WeatherForecast {
    date: DateOnly;
    temperatureC: number;
    temperatureF: number;
    summary: string | null;
}