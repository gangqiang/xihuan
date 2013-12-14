using System;
using System.Data;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.IO;
using Microsoft.JScript;
public partial class Admin_sys_goods : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            if (CommonMethod.FinalString(Request["goodsname"]).Length > 0)
                txtGoodName.Text = CommonMethod.FinalString(GlobalObject.unescape(Request["goodsname"].Trim()));
            chkOnlyShow.Checked = CommonMethod.FinalString(Request["onlyshow"]).Equals("true");
            BindGoods();
            string action = CommonMethod.FinalString(Request["action"]);
            if (action.Length > 0)
            {
                switch (action)
                {
                    case "del": DelGoods(Request["id"]); break;
                    case "tj": TJGoods(Request["id"]); break;
                    case "generate": GenerateDetail(Request["id"], Request["detailurl"]); break;
                    case "check": CheckGoods(Request["id"], Request["ownerid"]); break;
                    default: break;
                }
            }
        }
    }

    private void BindGoods()
    {
        XiHuan_UserGoodsSearchFilter f = new XiHuan_UserGoodsSearchFilter();
        int rowcount;
        f.SelectFileds = "g.Id,Name,OwnerName,OwnerId,DefaultPhoto,GoodState,ViewCount,CreateDate,DetailUrl,IsTJ,IsChecked ";
        if (txtGoodName.Text.Trim().Length > 0)
            f.GoodsName = txtGoodName.Text;
        f.PageIndex = PageBar1.PageIndex;
        f.IsChecked = chkOnlyShow.Checked ? 0 : int.MinValue;
        DataTable dt = XiHuan_UserGoodsFacade.SearchGoods(f, out rowcount);
        rptGoodsList.DataSource = dt;
        rptGoodsList.DataBind();
        PageBar1.RecordCount = rowcount;
        PageBar1.Draw();
    }

    private void DelGoods(string id)
    {
        int gid = CommonMethod.ConvertToInt(id, 0);
        Transaction t = new Transaction();
        if (gid > 0)
        {
            XiHuan_UserGoodsEntity del = XiHuan_UserGoodsEntityAction.RetrieveAXiHuan_UserGoodsEntity(gid);
            if (del != null)
            {
                t.AddDeleteObject(del);

                #region 删除换品信息的详细页面

                if (File.Exists(Server.MapPath("../" + del.DetailUrl)))
                {
                    File.Delete(Server.MapPath("../" + del.DetailUrl));
                }

                #endregion

                #region 换品删除时，同时删除其图片信息,浏览信息

                string imgphysicalsrc = string.Empty;
                string imgname = string.Empty;
                string thumphysicalsrc = string.Empty;
                RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_GoodsImageEntity));
                Condition c = rc.GetNewCondition();
                c.AddEqualTo(XiHuan_GoodsImageEntity.__GOODSID, gid);
                EntityContainer imagecontainer = rc.AsEntityContainer();
                foreach (XiHuan_GoodsImageEntity goodimage in imagecontainer)
                {
                    imgphysicalsrc = Server.MapPath("../" + goodimage.ImgSrc);
                    if (File.Exists(imgphysicalsrc))
                    {
                        File.Delete(imgphysicalsrc);
                    }
                    imgname = Path.GetFileNameWithoutExtension(imgphysicalsrc);
                    //如果存在缩略图，一起进行删除
                    thumphysicalsrc = imgphysicalsrc.Replace(imgname, imgname + GlobalVar.DefaultPhotoSize);
                    if (File.Exists(thumphysicalsrc))
                    {
                        File.Delete(thumphysicalsrc);
                    }
                    //如果存在缩略图，一起进行删除
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
                BindGoods();

            }
        }
    }

    private void TJGoods(string id)
    {
        Query.ProcessSql("update XiHuan_UserGoods set IsTJ=1 where Id=" + id, GlobalVar.DataBase_Name);
        Alert("推荐成功！");
        BindGoods();
    }

    private void GenerateDetail(string id, string detailurl)
    {
        CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + id, "../" + Microsoft.JScript.GlobalObject.unescape(detailurl));
        Alert("生成成功！");
    }

    private void CheckGoods(string id, string ownerid)
    {
        #region 换品审核状态更改

        Query.ProcessSql("update XiHuan_UserGoods set IsChecked=1 where Id=" + id, GlobalVar.DataBase_Name);

        #endregion

        #region 生成页面

        DataTable dt = Query.ProcessSql("select Id,DetailUrl,GoodState from XiHuan_UserGoods with(nolock) where OwnerId= " + ownerid + " and IsChecked=1 ", GlobalVar.DataBase_Name);
        foreach (DataRow dr in dt.Rows)
        {
            CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + dr["Id"], "../" + dr["DetailUrl"].ToString());
        }

        Alert("已成功通过审核！");
        BindGoods();

        #endregion
    }
}
