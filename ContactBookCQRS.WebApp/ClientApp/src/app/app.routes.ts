import { Routes, CanActivate, RouterModule } from '@angular/router';
import { HomeComponent } from './modules/contact-book/components/home/home.component';
import { LoginComponent } from './modules/authentication/components/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { CategoryListComponent } from './modules/contact-book/components/category-list/category-list.component';
import { AccountComponent } from './modules/authentication/components/account/account.component';

export const APP_ROUTES: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'account',
    component: AccountComponent
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full'
  },
  {
    path: 'category-list',
    component: CategoryListComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full'
  },
  {
    path: '**',
    component: HomeComponent,
    canActivate: [AuthGuard],
    pathMatch: 'full'
  }
]
