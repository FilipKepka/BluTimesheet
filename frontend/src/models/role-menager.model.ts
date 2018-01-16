export class RoleMenagerModel {
  id: number;
  roleName: string;


  constructor (roleMenagerData) {
    this.id = roleMenagerData.id
    this.roleName = roleMenagerData.roleName;

  }
}
