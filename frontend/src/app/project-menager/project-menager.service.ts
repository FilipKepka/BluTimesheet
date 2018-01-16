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
  private dataStore: {
    projectManager: ProjectsModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.dataStore = {projectManager: []};
    this._projectModel = <BehaviorSubject<ProjectsModel[]>>new BehaviorSubject([]);
    this.projectModel$ = this._projectModel.asObservable();
  }


  loadAllProjects() {
    this.http.get('http://localhost:51107/api/Project', {headers: this.headers}).subscribe((data: ProjectsModel[]) => {
      this.dataStore.projectManager = data;
      this._projectModel.next(Object.assign({}, this.dataStore).projectManager);
    }, error => console.log('Load terminals error: ', error));
  }



}
