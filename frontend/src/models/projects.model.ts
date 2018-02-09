import {ProjectTypeModel} from './projectType.model';

export class ProjectsModel {
  projectId: number;
  name: string;
  projectType: ProjectTypeModel;
  projectnumber: string;

  constructor (projects) {
    this.projectId = projects.projectId;
    this.name = projects.name;
    this.projectType = projects.projectType;
    this.projectnumber = projects.projectnumber;
  }
}
