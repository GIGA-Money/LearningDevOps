import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';
import { selectError } from '../../state/auth.selectors';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from '../../models/user.interface';
import { AuthActions } from '../../state/auth.actions';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  error$ = this.store.select(selectError());
  errorSub: Subscription | undefined;
  // error: string = '';
  constructor(private store: Store, private _snackBar: MatSnackBar) {
    this.getError();
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  ngOnDestroy(): void {
    this.errorSub?.unsubscribe();
  }

  submit(data: User) {
    this.store.dispatch({ type: AuthActions.CREATE_USER, payload: data });
  }

  getError() {
    this.errorSub = this.error$.subscribe((data) => {
      if (data) {
        this._snackBar.open(data.message, 'Error');
      }
    });
  }
}
