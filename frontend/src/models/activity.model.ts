import {ProjectTypeModel} from './projectType.model';
import {ProjectsModel} from './projects.model';
import {RoleMenagerModel} from './role-menager.model';


export class ActivityModel {
  id: number;
  Begining: string;
  End: string;
  description: string;
  ActivityType: ProjectTypeModel;
  Project: ProjectsModel;
  ApprovedByManager: string;
  UserId: string;
  CurrentProjectRoleType: RoleMenagerModel;
  HowManyHours: number;

  constructor (projects) {
    this.id = projects.id;
    this.Begining = projects.Begining;
    this.End = projects.End;
    this.description = projects.description;
    this.ActivityType = projects.ActivityType;
    this.Project = projects.Project;
    this.ApprovedByManager = projects.ApprovedByManager;
    this.UserId = projects.UserId;
    this.CurrentProjectRoleType = projects.CurrentProjectRoleType;
    this.HowManyHours = projects.HowManyHours;

  }
}
