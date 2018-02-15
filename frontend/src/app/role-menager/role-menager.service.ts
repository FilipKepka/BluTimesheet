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
  private baseUrl: string;
  private dataStore: {
    roleMenager: RoleMenagerModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.dataStore = {roleMenager: []};
    this._roleMenager = <BehaviorSubject<RoleMenagerModel[]>>new BehaviorSubject([]);
    this.roleMenager$ = this._roleMenager.asObservable();
    this.baseUrl = 'http://localhost:51107/api';
  }



  loadAllRole() {
    this.http.get(`${this.baseUrl}/ProjectRoleType`, {headers: this.headers}).subscribe((data: RoleMenagerModel[]) => {
      this.dataStore.roleMenager = data;
      this._roleMenager.next(Object.assign({}, this.dataStore).roleMenager);
    }, error => console.log('Load terminals error: ', error));
  }

  loadSingleRole(roleId) {
    return this.http.get(`${this.baseUrl}/ProjectRoleType/${roleId}`, {headers: this.headers});
  }

  addRole(data) {
    return this.http.post(`${this.baseUrl}/ProjectRoleType/`, JSON.stringify(data), {headers: this.headers});
  }

  deleteColumn(roleId: number) {
     this.http.delete(`${this.baseUrl}/ProjectRoleType/${roleId}`, {headers: this.headers})
       .subscribe(response => {
         this.dataStore.roleMenager.forEach((t, i) => {
           if (t.roleId === roleId) {
             this.dataStore.roleMenager.splice(i, 1);
           }
         });
         this._roleMenager.next(Object.assign({}, this.dataStore).roleMenager);
       }, error => console.log('Could not delete todo.'));
     }

   editColumn(roleId) {
     return this.http.put(`${this.baseUrl}/ProjectRoleType`, JSON.stringify(roleId), {headers: this.headers});
   }

}
