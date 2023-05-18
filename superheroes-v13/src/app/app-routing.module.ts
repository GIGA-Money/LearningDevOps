import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { FormGuard } from './core/guards/form.guard';
import { FormComponent } from './anti-hero/pages/form/form.component';

const routes: Routes = [
  {
    path: "anti-heroes",
    loadChildren: () =>
      import("./anti-hero/anti-hero.module").then((m) => m.AntiHeroModule),
      canLoad: [AuthGuard],
  },
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full",
  },
  {
    path: "login",
    loadChildren: () =>
    import("./auth/auth.module").then((m) => m.AuthModule),
  },
  {
    path: "form",
    children: [
      {
        path: "",
        canDeactivate: [FormGuard],
        component: FormComponent
      },
      {
        path: ":id",
        canDeactivate: [FormGuard],
        component: FormComponent
      }
    ]    
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }