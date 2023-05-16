import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
// import { environment } from "C:/Users/gigac/Documents/Projects/VB/DevOpsLearning/LearningDevOps/Angular/superheroes-v13/src/environments/environment.ts";
import { AntiHero } from "../models/anti-hero.interface";
import { environment } from "src/environments/environment.prod";


@Injectable({
  providedIn: "root",
})
export class AntiHeroService {
  constructor(private http: HttpClient) {}

  getAntiHeroes(): Observable<AntiHero[]> {
    return this.http.get<AntiHero[]>(`${environment.apiURL}/anti-heroes`).pipe(
      tap((data: AntiHero[]) => data),
      catchError((err) => throwError(() => err))
    );
  }

  getAntiHero(id: string): Observable<AntiHero> {
    return this.http
      .get<AntiHero>(`${environment.apiURL}/anti-heroes/${id}`)
      .pipe(
        tap((data: AntiHero) => data),
        catchError((err) => throwError(() => err))
      );
  }

  addAntiHero(antiHero: AntiHero): Observable<AntiHero> {
    return this.http
      .post<AntiHero>(`${environment.apiURL}/anti-heroes`, antiHero)
      .pipe(
        tap((data: AntiHero) => data),
        catchError((err) => throwError(() => err))
      );
  }

  updateAntiHero(id: string, antiHero: AntiHero): Observable<AntiHero> {
    return this.http
      .put<AntiHero>(`${environment.apiURL}/anti-heroes/${id}`, antiHero)
      .pipe(catchError((err) => throwError(() => err)));
  }

  deleteAntiHero(id: string): Observable<AntiHero> {
    return this.http
      .delete<AntiHero>(`${environment.apiURL}/anti-heroes/${id}`)
      .pipe(catchError((err) => throwError(() => err)));
  }
}
