import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import {Observable} from 'rxjs/Observable';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {RoleMenagerModel} from '../../models/role-menager.model';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class RoleMenagerService {

  public roleMenager$: Observable<RoleMenagerModel[]>;
  private _roleMenager: BehaviorSubject<RoleMenagerModel[]>;
  private baseUrl: string;
  private dataStore: {
    roleMenager: RoleMenagerModel[];
  };

  constructor( private http: HttpClient, private authService: AuthService) {
    this.dataStore = {roleMenager: []};
    this._roleMenager = <BehaviorSubject<RoleMenagerModel[]>>new BehaviorSubject([]);
    this.roleMenager$ = this._roleMenager.asObservable();
    this.baseUrl = this.authService.url;
  }



  loadAllRole() {
    this.http.get(`${this.baseUrl}/api/ProjectRoleType`).subscribe((data: RoleMenagerModel[]) => {
      this.dataStore.roleMenager = data;
      this._roleMenager.next(Object.assign({}, this.dataStore).roleMenager);
    }, error => console.log('Load terminals error: ', error));
  }

  loadSingleRole(roleId) {
    return this.http.get(`${this.baseUrl}/api/ProjectRoleType/${roleId}`);
  }

  addRole(data) {
    return this.http.post(`${this.baseUrl}/api/ProjectRoleType/`, JSON.stringify(data));
  }

  deleteColumn(roleId: number) {
     this.http.delete(`${this.baseUrl}/api/ProjectRoleType/${roleId}`)
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
     return this.http.put(`${this.baseUrl}/api/ProjectRoleType`, JSON.stringify(roleId));
   }

}
