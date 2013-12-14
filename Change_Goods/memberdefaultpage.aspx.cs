using System;
using BusinessEntity;
public partial class memberdefaultpage : BaseWebPage
{
    private XiHuan_UserInfoEntity UserInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "default");
        }
    }

    protected XiHuan_UserInfoEntity UserInfoEntity
    {
        get
        {
            if (UserInfo == null)
            {
                UserInfo = XiHuan_UserInfoEntityAction.RetrieveAXiHuan_UserInfoEntity(CurrentUserId);
            }

            return UserInfo;
        }
    }
}
