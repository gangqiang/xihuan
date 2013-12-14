using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.IO;
public partial class modifypic : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "modifypic.aspx?id=" + CommonMethod.FinalString(Request["id"]) + "&name=" + CommonMethod.FinalString(Request["name"]));
        }
        else
        {
            BindReceive();
        }
    }

    #endregion

    #region  图片数据绑定
    protected override void Page_PreInit()
    {
        base.Page_PreInit();
        PageControl1.PageChanged += new PageChangedDelegate(PageControl1_PageChanged);

    }

    void PageControl1_PageChanged(object sender, EventArgs e)
    {
        BindReceive();
    }

    private void BindReceive()
    {
        DataTable dt = Query.ProcessSql("select *from XiHuan_GoodsImage with(nolock) where GoodsId=" + CommonMethod.ConvertToInt(Request["id"], 0) + " order by CreateDate desc;", GlobalVar.DataBase_Name);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptGoodsList.DataSource = pds;
        rptGoodsList.DataBind();
    }

    #endregion

    #region 操作链接
    protected string ActionString(string isdefault, string id)
    {
        isdefault = CommonMethod.FinalString(isdefault);
        id = CommonMethod.FinalString(id);
        if (isdefault.Equals("1"))
            return "<a title=\"取消该图片做为换品的默认图片\" href=\"###\" onclick=\"CancleDefaultPhoto(" + id + ")\" >取消默认图片</a>";
        else
            return "<a title=\"设置该图片做为换品的默认图片\" href=\"###\" onclick=\"SetDefaultPhoto(" + id + ");\" >设为默认图片</a>";
    }
    #endregion

    #region 删除图片
    protected void lnkDelMessage_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);

        if (id > 0)
        {
            XiHuan_GoodsImageEntity note = new XiHuan_GoodsImageEntity();
            note.Id = id;
            note.Retrieve();
            if (note.IsPersistent)
            {
                string phypath = Server.MapPath(note.ImgSrc);
                string filename = Path.GetFileNameWithoutExtension(phypath);
                string thumpath = phypath.Replace(filename, filename + GlobalVar.DefaultPhotoSize);
                if (File.Exists(phypath))
                {
                    File.Delete(phypath);
                }
                if (File.Exists(thumpath))
                {
                    File.Delete(thumpath);
                }
                thumpath = phypath.Replace(filename, filename + GlobalVar.BigPhotoSize);
                if (File.Exists(thumpath))
                {
                    File.Delete(thumpath);
                }
                note.Delete();
                BindReceive();
                if (rptGoodsList.Items.Count == 0)
                    Query.ProcessSqlNonQuery("update XiHuan_UserGoods set IsHavePhoto=0 ,DefaultPhoto='images/none.jpg' where Id=" + Request["id"], GlobalVar.DataBase_Name);
                Alert("恭喜：图片已成功删除！");
            }
        }
    }

    protected void lnkDelMultiMessage_Click(object sender, EventArgs e)
    {
        string[] mid = hidId.Value.Trim().TrimEnd(',').Split(',');
        string physicalpath = string.Empty;
        string thumpath = string.Empty;
        string filename = string.Empty;
        if (mid.Length > 0)
        {
            for (int i = 0; i < mid.Length; i++)
            {
                int id = CommonMethod.ConvertToInt(mid[i], 0);
                if (id > 0)
                {
                    XiHuan_GoodsImageEntity note = new XiHuan_GoodsImageEntity();
                    note.Id = id;
                    note.Retrieve();
                    if (note.IsPersistent)
                    {
                        physicalpath = Server.MapPath(note.ImgSrc);
                        filename = Path.GetFileNameWithoutExtension(physicalpath);
                        if (File.Exists(physicalpath))
                        {
                            File.Delete(physicalpath);
                        }
                        thumpath = physicalpath.Replace(filename, GlobalVar.DefaultPhotoSize);
                        if (File.Exists(thumpath))
                        {
                            File.Delete(thumpath);
                        }
                        thumpath = physicalpath.Replace(filename, GlobalVar.BigPhotoSize);
                        if (File.Exists(thumpath))
                        {
                            File.Delete(thumpath);
                        }
                        note.Delete();
                    }
                }
            }

            BindReceive();
            if (rptGoodsList.Items.Count == 0)
            {
                Query.ProcessSqlNonQuery("update XiHuan_UserGoods set IsHavePhoto=0 ,DefaultPhoto='images/none.jpg' where Id=" + Request["id"], GlobalVar.DataBase_Name);
            }
            Alert("恭喜：选中的图片已成功删除！");
        }
    }
    #endregion

    #region 设为默认图片
    protected void lnkSetDefault_Click(object sender, EventArgs e)
    {
        int imgid = CommonMethod.ConvertToInt(hidId.Value, 0);
        string filename = string.Empty;
        string physicalpath = string.Empty;
        string thumpath = string.Empty;
        if (imgid > 0)
        {
            XiHuan_GoodsImageEntity defaultimage = new XiHuan_GoodsImageEntity();
            defaultimage.Id = imgid;
            defaultimage.Retrieve();
            if (defaultimage.IsPersistent)
            {
                physicalpath = Server.MapPath(defaultimage.ImgSrc);
                filename = Path.GetFileNameWithoutExtension(physicalpath);
                thumpath = physicalpath.Replace(filename, filename + GlobalVar.DefaultPhotoSize);
                if (!File.Exists(physicalpath))//没有缩略图，需要生成缩略图
                {
                    PicHelper.MakeThumbnail(physicalpath, thumpath, 85, 85);
                    thumpath = physicalpath.Replace(filename, filename + GlobalVar.BigPhotoSize);
                    PicHelper.MakeThumbnail(physicalpath, thumpath, 200, 220);
                }

                Query.ProcessSqlNonQuery(@"update XiHuan_GoodsImage set IsDefaultPhoto=0 where IsDefaultPhoto=1 and GoodsId=" + Request["id"]
                                           + ";update XiHuan_UserGoods set DefaultPhoto='" + defaultimage.ImgSrc.Replace(filename, filename + GlobalVar.DefaultPhotoSize) + "' where Id=" + Request["id"],
                                           GlobalVar.DataBase_Name);
                defaultimage.IsDefaultPhoto = 1;
                defaultimage.Save();
                CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + defaultimage.GoodsId, CommonMethod.FinalString(Request["detailurl"]));
                Alert("恭喜：成功设置为默认图片！");
                BindReceive();
            }
        }
    }
    #endregion

    #region 取消默认图片
    protected void lnkCancleDefault_Click(object sender, EventArgs e)
    {
        int imgid = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (imgid > 0)
        {
            XiHuan_GoodsImageEntity defaultimage = new XiHuan_GoodsImageEntity();
            defaultimage.Id = imgid;
            defaultimage.Retrieve();
            if (defaultimage.IsPersistent)
            {
                defaultimage.IsDefaultPhoto = 0;
                defaultimage.Save();
                Alert("恭喜：已成功取消默认图片！");
                BindReceive();
            }
        }
    }
    #endregion
}
