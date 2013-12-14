using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
public partial class goodshistory : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "goodshistory.aspx");
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }
    #endregion

    #region 搜藏信息
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
        DataTable dt = XiHuan_FavoriteFacade.GetUserFavoriteGoods(CurrentUserId);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptGoodsList.DataSource = pds;
        rptGoodsList.DataBind();
    }
    #endregion

    #region 删除搜藏

    protected void lnkDelFavorite_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (id > 0)
        {
            XiHuan_UserFavorateEntity favorite = new XiHuan_UserFavorateEntity();
            favorite.Id = id;
            favorite.Retrieve();
            if (favorite.IsPersistent)
            {
                favorite.Delete();
                Alert("恭喜：成功删除搜藏信息！");
                BindData();
            }
        }
    }
    #endregion
}
