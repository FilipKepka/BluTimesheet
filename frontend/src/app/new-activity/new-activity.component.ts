import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { AuthService} from '../auth/auth.service';


@Component({
  selector: 'app-new-activity',
  templateUrl: './new-activity.component.html',
  styleUrls: ['./new-activity.component.css']
})
export class NewActivityComponent implements OnInit {

  data: any = [];
  public headers;

  constructor(private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
  }



  ngOnInit() {
 /*   this.http.get('http://localhost:51107/api/Project', {headers: this.headers})
      .subscribe(res => {
        console.log(res);
        this.data = res;
      }); */
  }
}

