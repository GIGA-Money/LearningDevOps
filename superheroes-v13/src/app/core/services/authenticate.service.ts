import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthenticateService {
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}

  // for login endpoint
  login(data: { email: string; password: string }): Observable<any> {
    return this.http
      .post<any>(`${environment.authURL}/authenticate`, data)
      .pipe(
        tap((data: any) => data),
        catchError((err) => throwError(() => err))
      );
  }

  // for register endpoint
  register(data: { email: string; password: string }): Observable<any> {
    return this.http.post<any>(`${environment.authURL}/register`, data).pipe(
      tap((data: any) => data),
      catchError((err) => throwError(() => err))
    );
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token') ?? '';
    return !this.jwtHelper.isTokenExpired(token);
  }

  doLogout() {
    let removeToken = localStorage.removeItem('token');
    if (removeToken == null) {
      this.router.navigate(['/login']);
    }
  }
}
