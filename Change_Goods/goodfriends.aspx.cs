using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
public partial class goodfriends : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "goodfriends.aspx");
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }

    #endregion

    #region 好友数据绑定
    protected override void Page_PreInit()
    {
        base.Page_PreInit();

        PageControl1.PageChanged += new PageChangedDelegate(PageControl1_PageChanged);
    }

    void PageControl1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DataTable dt =XiHuan_UserFriendsFacade.GetUserFriends(CurrentUserId);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptGoodsList.DataSource = pds;
        rptGoodsList.DataBind();
    }

    #endregion

    #region 删除好友
    protected void lnkDelFriend_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (id > 0)
        {
            XiHuan_UserFriendsEntity friend = new XiHuan_UserFriendsEntity();
            friend.Id = id;
            friend.Retrieve();
            if (friend.IsPersistent)
            {
                friend.Delete();
                Alert("恭喜：成功解除好友关系！");
                BindData();
            }
        }
    }
    #endregion
}
