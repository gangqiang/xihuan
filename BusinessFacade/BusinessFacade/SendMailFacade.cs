using System.Text;
using System.Net.Mail;
namespace BusinessFacade
{
    public class SendMailFacade
    {
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="maito">�����ˣ���������ˣ��ö��Ÿ���</param>
        /// <param name="subject">����</param>
        /// <param name="body">����</param>
        public static void sendEmail(string maito, string subject, string body)
        {
            //���ʼ����˺� 
            string mailsender = CommonMethodFacade.GetConfigValue("EmailSender");
            //��ʾ���˺�����
            string maildisplayname = CommonMethodFacade.GetConfigValue("EmailDisplayName");
            //ʹ�õ�SMTP����
            string mailhost = CommonMethodFacade.GetConfigValue("EmailSmtpHost");
            // �˺�����
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
            msg.IsBodyHtml = true; //���������Ƿ�Ϊhtml��ʽ��ֵ
            msg.Priority = System.Net.Mail.MailPriority.High; //���ô��ʼ����и����ȼ�

            SmtpClient smtp = new SmtpClient(mailhost); //����Ӧ�ó���ʹ��SMTP���ʼ�
            smtp.Credentials = new System.Net.NetworkCredential(mailsender, mailpwd); //������֤�����˵�ƾ�ݣ��ʼ���������Ҫ�����֤��
            smtp.Timeout = 60 * 1000;//�趨��ʱʱ��Ϊ1����

            try
            {
                smtp.Send(msg);//���� 
                msg.Dispose(); //�ͷ���MailMessageʹ�õ�������Դ
            }
            catch
            {
            }
        }

    }
}
