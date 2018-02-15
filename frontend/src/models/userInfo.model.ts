export class UserInfoModel {
  fistName: string;
  lastName: string;
  userId: string;
  superiorId: string;
  email: string;
  passwordHash: string;
  securityStamp: string;
  userName: string;
  isAdmin: boolean;
  role: string;

  constructor (userInfoModel) {
    this.fistName = userInfoModel.Name;
    this.lastName = userInfoModel.Surname;
    this.userId = userInfoModel.userId;
    this.isAdmin = userInfoModel.IsAdmin;
    this.superiorId = userInfoModel.SuperiorId;
    this.email = userInfoModel.Email;
    this.passwordHash = userInfoModel.PasswordHash;
    this.securityStamp = userInfoModel.SecurityStamp;
    this.userName = userInfoModel.UserName;
    this.role = userInfoModel.role;


  }
}
