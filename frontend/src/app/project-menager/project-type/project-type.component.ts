import { Component, OnInit } from '@angular/core';
import { ProjectTypeModel} from '../../../models/projectType.model';
import { Observable} from 'rxjs/Observable';
import { NgForm} from '@angular/forms';
import {MatDialog, MatDialogRef, MatTableDataSource} from '@angular/material';
import {ConfirmationDialogComponent} from '../../confirmation-dialog/confirmation-dialog.component';
import {ProjectsModel} from '../../../models/projects.model';
import {ProjectMenagerService} from '../project-menager.service';
import { ProjectTypeService} from './project-type.service';

@Component({
  selector: 'app-project-type',
  templateUrl: './project-type.component.html',
  styleUrls: ['./project-type.component.css']
})
export class ProjectTypeComponent implements OnInit {
  private projectType$: Observable<ProjectTypeModel[]>;
  private dataSource: MatTableDataSource<ProjectTypeModel>;
  private displayedColumns: Array<string> = ['id', 'name'];
  dialogRef: MatDialogRef<ConfirmationDialogComponent>;
  data;

  constructor(private projectTypeService: ProjectTypeService, private dialog: MatDialog) { }

  ngOnInit() {
    // this.projectType$ = this.projectTypeService.projectTypeModel$;
    // this.projectTypeService.loadAllProjectsType()
    // this.projectType$
    //   .subscribe((data: ProjectTypeModel[]) => {
    //     this.data = data;
    //     this.dataSource = new MatTableDataSource<ProjectTypeModel>(data);
    //   });
  }

}
