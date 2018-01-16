import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { HttpModule } from '@angular/http';

interface AuthResponse {
  '.expires': string;
  '.issued': string;
  access_token: string;
  expires_in: number;
  token_type: string;
  userName: string;
}


@Injectable()
export class AuthService {
  public token: string;
  public url: string = 'http://localhost:51107';
  public error: boolean = false;
  public errorMessage: string;

  constructor(private router: Router, private http: HttpClient) {}


  getTokenFromStorage() {
    const token = sessionStorage.getItem('token');
    if (token) {
      return token;
    }
    return false;
  }

  saveTokentToStorage(token = '') {
    sessionStorage.setItem('token', token);
  }

  removeTokenFromStorage() {
    sessionStorage.removeItem('token');
  }

  getAuthorizationHeaders() {
    const token = this.getTokenFromStorage();
    let headers = null;

    if (token) {
      console.log(token);
      headers = new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json')
    }
    console.log(headers);

    return headers;
  }

  signInUser(login: string, password: string) {
    this.error = false;
    this.errorMessage = '';
    const body = new HttpParams()
      .set('grant_type', 'password')
      .set('userName', login)
      .set('password', password);

    this.http.post(this.url + '/Token', body.toString(),
      {
        headers: new HttpHeaders()
          .set('Content-Type', 'x-www-form-urlencoded')
      })
      .subscribe((res: AuthResponse) => {
        console.log(res);
        this.saveTokentToStorage(res.access_token);
        this.router.navigateByUrl('/new-activity');
      }, err => {
        this.error = true;
        this.errorMessage = err.error.error_description;
      });
  }

  logout() {
    this.removeTokenFromStorage();
    this.router.navigateByUrl('/signin');
  }

  isAuthenticated() {
    const token = this.getTokenFromStorage();
    if (token) return true;
    return false;
  }


}
