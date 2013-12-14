using System;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class findpass : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserInfoEntity));
        Condition c = rc.GetNewCondition();
        c.AddEqualTo(XiHuan_UserInfoEntity.__USERNAME, txtName.Text.Trim());
        rc.AddSelect(XiHuan_UserInfoEntity.__USERNAME);
        rc.AddSelect(XiHuan_UserInfoEntity.__ORIGNALPWD);
        rc.AddSelect(XiHuan_UserInfoEntity.__QUESTION);
        rc.AddSelect(XiHuan_UserInfoEntity.__ANSWER);
        XiHuan_UserInfoEntity user = rc.AsEntity() as XiHuan_UserInfoEntity;
        if (user != null)
        {
            if (user.Question == txtQuestion.Text.Trim() && user.Answer == txtAnswer.Text.Trim())
            {
                try
                {
                    SendMailFacade.sendEmail(txtEmail.Text.Trim(), "喜换网-找回密码", "<strong>" + txtName.Text.Trim() + "</strong>您好，<br/>您在喜换网注册的账号密码为" + user.OrignalPwd + ",请妥善保管，此邮件为系统邮件，请勿回复！");
                    Alert("您的密码已成功发送到您的邮箱，请注意查收！");
                    ExecScript("parent.ymPrompt.close();");
                }
                catch
                {
                    Alert("抱歉：邮件发送出现错误！");
                    return;
                }

            }
            else
            {
                Alert("很抱歉：您输入的安全提问问题和答案不符合，我们不能为您提供找回密码的服务！");
                return;
            }
        }
        else
        {
            Alert("不存在换客名为" + txtName.Text + "的用户！");
            return;
        }
    }
}
