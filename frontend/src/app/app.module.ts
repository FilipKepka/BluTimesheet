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
import { ProjectTypeService } from './project-menager/project-type/project-type.service';

import { BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { MatButtonModule, MatCheckboxModule } from '@angular/material';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { ProjectTypeComponent } from './project-menager/project-type/project-type.component';
import { ReportGenerateService } from './report-generate/report-generate.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NewActivityComponent,
    ReportGenerateComponent,
    SigninComponent,
    ProjectMenagerComponent,
    RoleMenagerComponent,
    ConfirmationDialogComponent,
    ProjectTypeComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AppMaterialModule,
    BrowserAnimationsModule
  ],
  providers: [AuthGuardService, AuthService, NewActivityService, ProjectMenagerService, RoleMenagerService, ProjectTypeService, ReportGenerateService],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmationDialogComponent]
})
export class AppModule { }
