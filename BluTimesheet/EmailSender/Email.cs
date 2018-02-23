using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Mail;
using System.Web;
using BluTimesheet.Authorization;
using BluTimesheet.Models.DbModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BluTimesheet.EmailSender
{
    public class Email
    {
        private readonly ApplicationUserManager userManager;
        public Email()
        {
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        public void EmailSender(Models.DbModels.Activity activity)
        {
            var user = userManager.FindById(activity.UserId);

            var superiorId = user.SuperiorId;

            var superior = userManager.FindById(superiorId);

            var Begining = activity.Begining.ToString().Substring(0, 10);

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(superior.Email);
                mail.From = new MailAddress("blunovationBluTimesheet@gmail.com", "New activity", System.Text.Encoding.UTF8);
                mail.Subject = String.Format("{0} {1} send new activity.", user.FirstName, user.LastName);
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = $"Data: {Begining} <br /> " + 
                    $"How many hours: {activity.HowManyHours} <br />" + 
                    $"Description: {activity.description}"; 
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("blunovationBluTimesheet@gmail.com", "blunovation123");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
               // client.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}