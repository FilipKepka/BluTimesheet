import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { AppRoutingModule } from './app-routing.module';
import { NewActivityComponent } from './new-activity/new-activity.component';
import { ReportGenerateComponent } from './report-generate/report-generate.component';
import { SigninComponent } from './auth/signin/signin.component';
import { AuthGuardService} from './auth/auth-guard.service';
import { AuthService} from './auth/auth.service';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule} from '@angular/forms';
import { AppMaterialModule } from './app-material.module';
import { NewActivityService } from './new-activity/new-activity.service';
import { ProjectMenagerComponent } from './project-menager/project-menager.component';
import { RoleMenagerComponent } from './role-menager/role-menager.component';
import { ProjectMenagerService } from './project-menager/project-menager.service';
import { RoleMenagerService } from './role-menager/role-menager.service';

import { BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { MatButtonModule, MatCheckboxModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NewActivityComponent,
    ReportGenerateComponent,
    SigninComponent,
    ProjectMenagerComponent,
    RoleMenagerComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AppMaterialModule,
    BrowserAnimationsModule
  ],
  providers: [AuthGuardService, AuthService, NewActivityService, ProjectMenagerService, RoleMenagerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
