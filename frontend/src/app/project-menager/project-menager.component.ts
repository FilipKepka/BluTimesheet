import { Component, OnInit } from '@angular/core';
import { ProjectMenagerService } from './project-menager.service';
import { ProjectsModel } from '../../models/projects.model';
import { MatDialog, MatDialogRef, MatTableDataSource, MatButtonModule, MatCheckboxModule } from '@angular/material';
import { Observable} from 'rxjs/Observable';
import {RoleMenagerModel} from '../../models/role-menager.model';
import {ConfirmationDialogComponent} from '../confirmation-dialog/confirmation-dialog.component';
import {NgForm} from '@angular/forms';
import {ProjectTypeModel} from '../../models/projectType.model';
import { ProjectTypeComponent} from './project-type/project-type.component';
import {ProjectTypeService} from './project-type/project-type.service';

@Component({
  selector: 'app-project-menager',
  templateUrl: './project-menager.component.html',
  styleUrls: ['./project-menager.component.css']
})
export class ProjectMenagerComponent implements OnInit {

  private projecteManager$: Observable<ProjectsModel[]>;
  private dataSource: MatTableDataSource<ProjectsModel>;
  private displayedColumns: Array<string> = ['id', 'name', 'projectType' , 'projectnumber' , 'settings'];
  dialogRef: MatDialogRef<ConfirmationDialogComponent>;
  data;
  selectValues: ProjectTypeModel[];

  constructor(private projectManagerService: ProjectMenagerService,
              private projectTypeService: ProjectTypeService, private dialog: MatDialog) { }

  ngOnInit() {
    this.projecteManager$ = this.projectManagerService.projectModel$;
    this.projectManagerService.loadAllProjects();
    this.projecteManager$
      .subscribe((data: ProjectsModel[]) => {
        this.data = data;
        this.dataSource = new MatTableDataSource<ProjectsModel>(data);
      });
    this.projectTypeService.loadAllPeojectsType()
      .subscribe((res: ProjectTypeModel[]) => {
        this.selectValues = res;
      });
  }

  addNewProject(form: NgForm) {
    const body: any = {};
    body.Name = form.value.newProject;
    body.ProjectType = { ProjectTypeId: form.value.projectType } ;
    body.Projectnumber = form.value.projectNumber;
    console.log("BODY",body);

    this.projectManagerService.addProject(body)
      .subscribe((res: ProjectsModel) => {
        this.data.push({projectId: res.projectId, name: res.name, projectType: res.projectType, projectnumber: res.projectnumber });
        console.log(this.data);
        this.dataSource = new MatTableDataSource<ProjectsModel>(this.data);
      });
    form.reset();
  }


  removeProject(id) {
    this.dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: false
    });
    this.dialogRef.componentInstance.confirmMessage = "Are you sure you want to delete?";

    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.projectManagerService.deleteColumn(id);
      }
      this.dialogRef = null;
    });
  }


  }



