export class ProjectTypeModel {
  id: number;
  Name: string;


  constructor (projectTypes) {
    this.id = projectTypes.id;
    this.Name = projectTypes.Name;

  }
}
