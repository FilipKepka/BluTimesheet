import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import {Observable} from 'rxjs/Observable';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {HttpClient} from '@angular/common/http';
import {ProjectsModel} from '../../models/projects.model';



@Injectable()
export class ProjectMenagerService {

  public headers;
  public projectModel$: Observable<ProjectsModel[]>;
  private _projectModel: BehaviorSubject<ProjectsModel[]>;
  private baseUrl: string;
  private dataStore: {
    projectManager: ProjectsModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.dataStore = {projectManager: []};
    this._projectModel = <BehaviorSubject<ProjectsModel[]>>new BehaviorSubject([]);
    this.projectModel$ = this._projectModel.asObservable();
    this.baseUrl = 'http://localhost:51107/api';
  }


  loadAllProjects() {
    this.http.get(`${this.baseUrl}/Project`, {headers: this.headers}).subscribe((data: ProjectsModel[]) => {
      this.dataStore.projectManager = data;
      this._projectModel.next(Object.assign({}, this.dataStore).projectManager);
    }, error => console.log('Load terminals error: ', error));
  }

  addProject(data) {
    return this.http.post(`${this.baseUrl}/Project/`, JSON.stringify(data), {headers: this.headers});
  }

  deleteColumn(projectId: number) {
    console.log(projectId);
    this.http.delete(`${this.baseUrl}/Project/${projectId}`, {headers: this.headers})
      .subscribe(response => {
        this.dataStore.projectManager.forEach((t, i) => {
          if (t.projectId === projectId) {
            this.dataStore.projectManager.splice(i, 1);
          }
        });
        this._projectModel.next(Object.assign({}, this.dataStore).projectManager);
      }, error => console.log('Could not delete todo.'));
  }

}
