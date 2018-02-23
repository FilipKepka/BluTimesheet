import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { HeaderComponent } from './header/header.component';
import { ReportGenerateComponent } from './report-generate/report-generate.component';
import { NewActivityComponent } from './new-activity/new-activity.component';
import {SigninComponent} from './auth/signin/signin.component';
import { ProjectMenagerComponent} from './project-menager/project-menager.component';
import { RoleMenagerComponent} from './role-menager/role-menager.component';
import {UserManagerComponent} from './user-manager/user-manager.component';
import {AuthGuardService} from './auth/auth-guard.service';


const routes: Routes = [
  { path: '', redirectTo: '/signin', pathMatch: 'full' },
  { path: 'header', component: HeaderComponent},
  { path: 'report-generate', component: ReportGenerateComponent, canActivate: [AuthGuardService] },
  { path: 'new-activity', component: NewActivityComponent, canActivate: [AuthGuardService] },
  { path: 'signin', component: SigninComponent},
  { path: 'project-menager', component: ProjectMenagerComponent, canActivate: [AuthGuardService]},
  { path: 'role-menager', component: RoleMenagerComponent, canActivate: [AuthGuardService]},
  { path: 'user-manager', component: UserManagerComponent, canActivate: [AuthGuardService] },

];


@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    CommonModule,
  ],

  exports: [
    RouterModule,
  ],

  declarations: []
})
export class AppRoutingModule { }
