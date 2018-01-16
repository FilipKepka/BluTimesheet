import { Component, OnInit } from '@angular/core';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { RoleMenagerModel } from '../../models/role-menager.model';
import { Observable } from 'rxjs/Observable';
import { RoleMenagerService } from './role-menager.service';


@Component({
  selector: 'app-role-menager',
  templateUrl: './role-menager.component.html',
  styleUrls: ['./role-menager.component.css']
})
export class RoleMenagerComponent implements OnInit {

  private roleMenager$: Observable<RoleMenagerModel[]>;
  private dataSource: MatTableDataSource<RoleMenagerModel>;
  private displayedColumns: Array<string> = ['id', 'name'];

  constructor( private roleMenagerService: RoleMenagerService ) { }

  ngOnInit() {
    this.roleMenager$ = this.roleMenagerService.roleMenager$;
    this.roleMenagerService.loadAllRole();
    this.roleMenager$
      .subscribe((data: RoleMenagerModel[]) => {
        console.log(data);
        this.dataSource = new MatTableDataSource<RoleMenagerModel>(data);
      });
  }


}
