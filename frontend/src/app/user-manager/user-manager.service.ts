import { Injectable } from '@angular/core';
import {AuthService} from '../auth/auth.service';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class UserManagerService {
  public headers;
  private baseUrl;

  constructor(private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.baseUrl = 'http://localhost:51107/api';
  }

// complete user info
  loadAllUsers() {
  return this.http.get(`${this.baseUrl}/UserInfo/AllUserInfo`, {headers: this.headers});
}

  loadSingleUser(id) {
    return this.http.get(`${this.baseUrl}/UserInfo/SingleUserInfo/${id}`, {headers: this.headers});
  }

  addNewUser(newUserData) {
    return this.http.post(`${this.baseUrl}/Account/Register`, JSON.stringify(newUserData), {headers: this.headers});
  }

  editUser(userToEditData) {
    return this.http.post(`${this.baseUrl}/Account/UserEdit`, JSON.stringify(userToEditData), {headers: this.headers});
  }

  removeUser(userId) {
    return this.http.post(`${this.baseUrl}/Account/RemoveUser/${userId}`, {headers: this.headers});
  }
}

