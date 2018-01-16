import {ProjectTypeModel} from './projectType.model';

export class ProjectsModel {
  id: number;
  name: string;
  projectType: ProjectTypeModel;
  projectnumber: string;

  constructor (projects) {
    this.id = projects.id;
    this.name = projects.name;
    this.projectType = projects.projectType;
    this.projectnumber = projects.projectnumber;
  }
}
