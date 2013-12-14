using System;
using BusinessEntity;
using PersistenceLayer;
public partial class usermanage : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GetDetail();
    }

    private void GetDetail()
    {
        RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserInfoEntity));
        Condition c = rc.GetNewCondition();
        c.AddEqualTo(XiHuan_UserInfoEntity.__USERNAME, txtUserName.Text.Trim());
        rc.AddSelect(XiHuan_UserInfoEntity.__ID);
        rc.AddSelect(XiHuan_UserInfoEntity.__ORIGNALPWD);
        rc.AddSelect(XiHuan_UserInfoEntity.__ISLOCKED);
        XiHuan_UserInfoEntity user = rc.AsEntity() as XiHuan_UserInfoEntity;
        if (user != null)
        {
            if (user.IsLocked == 0)
                lblResult.Text = string.Format("密码为：<span class=\"highlight\">{0}</span>，<a href=\"###\" onclick=\"LockUser({1});\">锁定此账号</a>", user.OrignalPwd, user.ID);
            if (user.IsLocked == 1)
                lblResult.Text = string.Format("密码为：<span class=\"highlight\">{0}</span>，<a href=\"###\" onclick=\"UnLockUser({1});\">撤消锁定此账号</a>", user.OrignalPwd, user.ID);
        }
        else
        {
            lblResult.Text = "<span class=\"highlight\">没有找到此会员的信息！</span>";
        }
    }

    protected void lnkLockUser_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (id > 0)
        {
            XiHuan_UserInfoEntity user = new XiHuan_UserInfoEntity();
            user.ID = id;
            user.Retrieve();
            if (user.IsPersistent)
            {
                user.IsLocked = 1;
                user.Save();
                Alert("已锁定此账号！");
                GetDetail();
            }
        }
    }

    protected void lnkUnLockUser_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (id > 0)
        {
            XiHuan_UserInfoEntity user = new XiHuan_UserInfoEntity();
            user.ID = id;
            user.Retrieve();
            if (user.IsPersistent)
            {
                user.IsLocked = 0;
                user.Save();
                Alert("已解除锁定此账号！");
                GetDetail();
            }
        }
    }
}
