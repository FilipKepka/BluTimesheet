export class UserHoursModel {
  Working: number;
  Sickness: number;
  Holidays: number;
  BankHolidays: number;
  Event: number;
  Other: number;


  constructor (userHoursModel) {
    this.Working = userHoursModel.Working;
    this.Sickness = userHoursModel.Sickness;
    this.Holidays = userHoursModel.Holidays;
    this.BankHolidays = userHoursModel.BankHolidays;
    this.Event = userHoursModel.Event;
    this.Other = userHoursModel.Other;


  }
}
