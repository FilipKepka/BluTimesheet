import { Injectable } from '@angular/core';
import {AuthService} from '../auth/auth.service';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class UserManagerService {

  private baseUrl;

  constructor(private http: HttpClient, private authService: AuthService) {

    this.baseUrl = this.authService.url;
  }

// complete user info
  loadAllUsers() {
  return this.http.get(`${this.baseUrl}/api/UserInfo/AllUserInfo`);
}

  loadSingleUser(id) {
    return this.http.get(`${this.baseUrl}/api/UserInfo/SingleUserInfo/${id}`);
  }

  addNewUser(newUserData) {
    return this.http.post(`${this.baseUrl}/api/Account/Register`, JSON.stringify(newUserData));
  }

  editUser(userToEditData) {
    return this.http.post(`${this.baseUrl}/api/Account/UserEdit`, JSON.stringify(userToEditData));
  }

  removeUser(userId) {
    return this.http.get(`${this.baseUrl}/api/Account/RemoveUser/${userId}`);
  }

  changePasswordWithoutOld(changePasswordData) {
    return this.http.post(`${this.baseUrl}/api/Account/ChangePasswordWithoutOld`, JSON.stringify(changePasswordData));
  }

  changePassword(changePasswordData) {
    return this.http.post(`${this.baseUrl}/api/Account/ChangePassword`, JSON.stringify(changePasswordData));
  }
}

