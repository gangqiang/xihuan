using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.IO;
public partial class goodlist : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "goodlist.aspx");
        }

        if (!IsPostBack)
        {
            DataTable dt = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType();
            CommonMethod.BindDrop(ddlGoodType, dt, "TypeName", "Id");
            ddlGoodType.Items.Insert(0, new ListItem("不限", ""));
            CommonMethod.BindDrop(ddlGoodState, XiHuan_UserGoodsFacade.GetGoodsState(), "Text", "Value");
            ddlGoodState.Items.Insert(0, new ListItem("不限", ""));
            BindData();
        }
    }

    protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGoodChildType.Items.Clear();
        if (ddlGoodType.SelectedValue.Trim().Length > 0)
            CommonMethod.BindDrop(ddlGoodChildType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(ddlGoodType.SelectedValue), "Name", "Id");
        ddlGoodChildType.Items.Insert(0, new ListItem("不限子类别", ""));
        ddlGoodChildType.SelectedIndex = 0;
    }

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
        XiHuan_UserGoodsSearchFilter f = new XiHuan_UserGoodsSearchFilter();
        f.SelectFileds = " Id,Name,DefaultPhoto,GoodState,ViewCount,CreateDate,DetailUrl,IsChecked ";
        f.OwnerId = CurrentUserId;
        f.GoodsName = txtGoodName.Text;
        f.GoodsTypeId = CommonMethod.ConvertToInt(ddlGoodType.SelectedValue, int.MaxValue);
        f.GoodsSceondTypeId = CommonMethod.ConvertToInt(Request["ddlGoodChildType"], int.MaxValue);
        f.GoodsState = CommonMethod.ConvertToInt(ddlGoodState.SelectedValue, int.MaxValue);
        f.IsHavePhoto = CommonMethod.ConvertToInt(ddlImage.SelectedValue, int.MaxValue);
        if (!DateTime.TryParse(txtDateBegin.Value.Trim(), out f.CreateDateBegin))
            f.CreateDateBegin = DateTime.MinValue;
        if (!DateTime.TryParse(txtDateEnd.Value.Trim(), out f.CreateDateEnd))
            f.CreateDateEnd = DateTime.MaxValue;
        DataTable dt = XiHuan_UserGoodsFacade.SearchGoods(f);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptGoodsList.DataSource = pds;
        rptGoodsList.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void lnkDel_Click(object sender, EventArgs e)
    {
        int gid = CommonMethod.ConvertToInt(hidGoodId.Value.Trim(), 0);
        Transaction t = new Transaction();
        if (gid > 0)
        {
            XiHuan_UserGoodsEntity del = XiHuan_UserGoodsEntityAction.RetrieveAXiHuan_UserGoodsEntity(gid);
            if (del != null)
            {
                if (del.GoodState == (byte)XiHuan_UserGoodsFacade.GoodsState.交换中)
                {
                    Alert("此换品正处于交换中的状态不能删除！");
                    return;
                }
                else
                {
                    t.AddDeleteObject(del);

                    #region 删除换品信息的详细页面

                    if (File.Exists(Server.MapPath(del.DetailUrl)))
                    {
                        File.Delete(Server.MapPath(del.DetailUrl));
                    }

                    #endregion

                    #region 换品删除时，同时删除其图片信息,浏览信息

                    RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_GoodsImageEntity));
                    Condition c = rc.GetNewCondition();
                    c.AddEqualTo(XiHuan_GoodsImageEntity.__GOODSID, gid);
                    EntityContainer imagecontainer = rc.AsEntityContainer();
                    string imgphysicalsrc = string.Empty;
                    string imgname = string.Empty;
                    string thumphysicalsrc = string.Empty;
                    foreach (XiHuan_GoodsImageEntity goodimage in imagecontainer)
                    {
                        imgphysicalsrc = Server.MapPath(goodimage.ImgSrc);
                        if (File.Exists(imgphysicalsrc))
                        {
                            File.Delete(imgphysicalsrc);
                        }
                        imgname = Path.GetFileNameWithoutExtension(imgphysicalsrc);
                        thumphysicalsrc = imgphysicalsrc.Replace(imgname, imgname + GlobalVar.DefaultPhotoSize);
                        if (File.Exists(thumphysicalsrc))
                        {
                            File.Delete(thumphysicalsrc);
                        }
                        thumphysicalsrc = imgphysicalsrc.Replace(imgname, imgname + GlobalVar.BigPhotoSize);
                        if (File.Exists(thumphysicalsrc))
                        {
                            File.Delete(thumphysicalsrc);
                        }
                        t.AddDeleteObject(goodimage);
                    }

                    DeleteCriteria delhistory = new DeleteCriteria(typeof(XiHuan_GoodsViewUserEntity));
                    Condition chis = delhistory.GetNewCondition();
                    chis.AddEqualTo(XiHuan_GoodsViewUserEntity.__GOODSID, gid);
                    chis.AddEqualTo(XiHuan_GoodsViewUserEntity._TYPE, 0);
                    t.AddDeleteCriteria(delhistory);

                    #endregion

                    #region 更新用户换品数量

                    t.AddSqlString("update XiHuan_UserInfo set GoodsNumber=GoodsNumber-1 where Id=" + del.OwnerId, GlobalVar.DataBase_Name);

                    #endregion

                    t.Process();
                    Alert("恭喜：换品删除成功！");
                    BindData();
                }
            }
        }
    }
}
