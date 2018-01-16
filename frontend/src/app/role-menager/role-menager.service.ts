import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import {Observable} from 'rxjs/Observable';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {RoleMenagerModel} from '../../models/role-menager.model';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class RoleMenagerService {

  public headers;
  public roleMenager$: Observable<RoleMenagerModel[]>;
  private _roleMenager: BehaviorSubject<RoleMenagerModel[]>;
  private dataStore: {
    roleMenager: RoleMenagerModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.dataStore = {roleMenager: []};
    this._roleMenager = <BehaviorSubject<RoleMenagerModel[]>>new BehaviorSubject([]);
    this.roleMenager$ = this._roleMenager.asObservable();
  }



  loadAllRole() {
    this.http.get('http://localhost:51107/api/ProjectRoleType', {headers: this.headers}).subscribe((data: RoleMenagerModel[]) => {
      this.dataStore.roleMenager = data;
      this._roleMenager.next(Object.assign({}, this.dataStore).roleMenager);
    }, error => console.log('Load terminals error: ', error));
  }

}
