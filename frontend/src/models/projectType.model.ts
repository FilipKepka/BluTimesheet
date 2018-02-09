export class ProjectTypeModel {
  projectTypeId: number;
  Name: string;


  constructor (projectTypes) {
    this.projectTypeId = projectTypes.projectTypeId;
    this.Name = projectTypes.Name;

  }
}
