using System.Text;
using System.Net.Mail;
namespace BusinessFacade
{
    public class SendMailFacade
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="maito">接收人，多个接收人，用逗号隔开</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        public static void sendEmail(string maito, string subject, string body)
        {
            //发邮件的账号 
            string mailsender = CommonMethodFacade.GetConfigValue("EmailSender");
            //显示的账号名称
            string maildisplayname = CommonMethodFacade.GetConfigValue("EmailDisplayName");
            //使用的SMTP主机
            string mailhost = CommonMethodFacade.GetConfigValue("EmailSmtpHost");
            // 账号密码
            string mailpwd = CommonMethodFacade.GetConfigValue("EmailSmtpPassword");

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(mailsender, maildisplayname, Encoding.UTF8);
            string[] mailto = maito.Split(';');
            MailAddressCollection addcollection = new MailAddressCollection();
            for (int i = 0; i < mailto.Length; i++)
            {
                if (ValidatorHelper.IsEmail(mailto[i]))
                {
                    msg.To.Add(new MailAddress(mailto[i]));
                }
            }
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true; //设置正文是否为html格式的值
            msg.Priority = System.Net.Mail.MailPriority.High; //设置此邮件具有高优先级

            SmtpClient smtp = new SmtpClient(mailhost); //允许应用程序使用SMTP发邮件
            smtp.Credentials = new System.Net.NetworkCredential(mailsender, mailpwd); //设置验证发件人的凭据（邮件服务器需要身份验证）
            smtp.Timeout = 60 * 1000;//设定超时时间为1分钟

            try
            {
                smtp.Send(msg);//发信 
                msg.Dispose(); //释放有MailMessage使用的所有资源
            }
            catch
            {
            }
        }

    }
}
