import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import {Observable} from 'rxjs/Observable';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {HttpClient} from '@angular/common/http';
import {ProjectsModel} from '../../models/projects.model';



@Injectable()
export class ProjectMenagerService {

  public projectModel$: Observable<ProjectsModel[]>;
  private _projectModel: BehaviorSubject<ProjectsModel[]>;
  private baseUrl: string;
  private dataStore: {
    projectManager: ProjectsModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.dataStore = {projectManager: []};
    this._projectModel = <BehaviorSubject<ProjectsModel[]>>new BehaviorSubject([]);
    this.projectModel$ = this._projectModel.asObservable();
    this.baseUrl = this.authService.url;
  }


  loadAllProjects() {
    this.http.get(`${this.baseUrl}/api/Project`, ).subscribe((data: ProjectsModel[]) => {
      this.dataStore.projectManager = data;
      this._projectModel.next(Object.assign({}, this.dataStore).projectManager);
    }, error => console.log('Load terminals error: ', error));
  }

  loadSingleProject(projectId) {
    return this.http.get(`${this.baseUrl}/api/Project/${projectId}`);
  }

  loadAllProjectTypes() {
    return this.http.get(`${this.baseUrl}/api/ProjectType`);
  }

  addProject(data) {
    return this.http.post(`${this.baseUrl}/api/Project/`, JSON.stringify(data));
  }

  deleteColumn(projectId: number) {
    this.http.delete(`${this.baseUrl}/api/Project/${projectId}`)
      .subscribe(response => {
        this.dataStore.projectManager.forEach((t, i) => {
          if (t.projectId === projectId) {
            this.dataStore.projectManager.splice(i, 1);
          }
        });
        this._projectModel.next(Object.assign({}, this.dataStore).projectManager);
      }, error => console.log('Could not delete todo.'));
  }

  editColumn(projectId) {
    return this.http.put(`${this.baseUrl}/api/Project`, JSON.stringify(projectId));
  }
}
