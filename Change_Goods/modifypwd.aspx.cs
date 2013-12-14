using System;
using BusinessFacade;
using BusinessEntity;
public partial class modifypwd : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "modifypwd.aspx");
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (IsUserAlreadyLogin)
        {
            if (!XiHuan_UserFacade.IsUserValid(CurrentUserName, txtOldPassWord.Text))
            {
                Alert("原密码不正确");
                return;
            }
            else
            {
                XiHuan_UserInfoEntity modifypwd= XiHuan_UserInfoEntityAction.RetrieveAXiHuan_UserInfoEntity(CurrentUserId);
                if (modifypwd != null)
                {
                    modifypwd.OrignalPwd = txtNewPassWord.Text.Trim();
                    modifypwd.Md5Pwd =CommonMethod.MD5Encrypt(txtNewPassWord.Text.Trim());
                    modifypwd.Save();
                    Alert("恭喜：您的密码已成功修改！");
                }

            }
        }
        else
        {
            MemberCenterPageRedirect("", "modifypwd"); 
        }
    }
}
