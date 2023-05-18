import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';
import { User } from '../../models/user.interface';
import { AuthActions } from '../../state/auth.actions';
import { selectError } from '../../state/auth.selectors';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  error$ = this.store.select(selectError());
  errorSub: Subscription | undefined;
  constructor(
    private store: Store,
    private _snackBar: MatSnackBar,
    private authService: AuthenticateService,
    private router: Router
  ) {
    this.checkJWT();
    this.getError();
  }

  submit(data: User) {
    this.store.dispatch({ type: AuthActions.LOGIN, payload: data });
  }

  ngOnDestroy(): void {
    this.errorSub?.unsubscribe();
  }

  getError() {
    this.error$.subscribe((data) => {
      if (data) {
        this._snackBar.open(data.message, 'Error');
      }
    });
  }

  checkJWT() {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/anti-heroes']);
    }
  }
}
