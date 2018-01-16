import { Component, OnInit } from '@angular/core';
import { ProjectMenagerService } from './project-menager.service';
import { ProjectsModel } from '../../models/projects.model';
import { MatDialog, MatDialogRef, MatTableDataSource, MatButtonModule, MatCheckboxModule } from '@angular/material';
import { Observable} from 'rxjs/Observable';


@Component({
  selector: 'app-project-menager',
  templateUrl: './project-menager.component.html',
  styleUrls: ['./project-menager.component.css']
})
export class ProjectMenagerComponent implements OnInit {

  private projecteManager$: Observable<ProjectsModel[]>;
  private dataSource: MatTableDataSource<ProjectsModel>;
  private displayedColumns: Array<string> = ['id', 'name', 'projectType' , 'projectnumber' ];

  constructor(private projectManagerService: ProjectMenagerService) { }

  ngOnInit() {
    this.projecteManager$ = this.projectManagerService.projectModel$;
    this.projectManagerService.loadAllProjects();
    this.projecteManager$
      .subscribe((data: ProjectsModel[]) => {
        console.log(data);
        this.dataSource = new MatTableDataSource<ProjectsModel>(data);
      });
  }
  }



