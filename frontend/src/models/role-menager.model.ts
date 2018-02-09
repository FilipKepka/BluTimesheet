export class RoleMenagerModel {
  roleId: number;
  roleName: string;


  constructor (roleMenagerData) {
    this.roleId = roleMenagerData.id;
    this.roleName = roleMenagerData.roleName;

  }
}
