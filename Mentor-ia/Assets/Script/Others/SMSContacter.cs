using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SMSContacter
{
    public Candidato cand_alvo;
    private string bodyMessage;//corpo do texto do email
    private string recipientEmail;//email alvo

    private void Start(){
        recipientEmail = cand_alvo.email;
    }

    public void SetTargetCand(Candidato c){
        this.cand_alvo = c;
    }

    public void SendEmail()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("mentor.ia.noreply@gmail.com");
        mail.To.Add(new MailAddress(recipientEmail));

        mail.Subject = "Email teste do APP Mentor-ia";
        mail.Body = bodyMessage;


        SmtpServer.Credentials = new System.Net.NetworkCredential("mentor.ia.noreply@gmail.com", "testedoapp") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }


    public void SendText()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;

        mail.From = new MailAddress("mentor.ia.noreply@gmail.com");

        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@txt.att.net"));//See carrier destinations below
                                                                   //message.To.Add(new MailAddress("5551234568@txt.att.net"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@vtext.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@messaging.sprintpcs.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@tmomail.net"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@vmobl.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@messaging.nextel.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@myboostmobile.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@message.alltel.com"));
        mail.To.Add(new MailAddress(cand_alvo.telNumber + "@mms.ee.co.uk"));



        mail.Subject = "Email teste do APP Mentor-ia";
        mail.Body = "Mensagem exemplo";

        SmtpServer.Port = 587;

        SmtpServer.Credentials = new System.Net.NetworkCredential("mentor.ia.noreply@gmail.com", "testedoapp") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }
}

