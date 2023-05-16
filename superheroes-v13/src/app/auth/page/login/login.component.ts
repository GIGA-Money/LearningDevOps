import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';
import { User } from '../../models/user.interface';
import { AuthActions } from '../../state/auth.actions';
import { selectError } from '../../state/auth.selectors';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  constructor(
    private authService: AuthenticateService,
    private router: Router
  ) {
    this.checkJWT();
  }
  //   this.authService.login(data).subscribe((data) => {
  //     this.router.navigate(['/anti-heroes']);
  //     localStorage.setItem('token', data.token);
  //   });
  // }

  checkJWT() {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/anti-heroes']);
    }
  }
}
