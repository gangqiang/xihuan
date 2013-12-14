using System;
using PersistenceLayer;
using BusinessEntity;
using System.IO;
using BusinessFacade;
using System.Data;
using System.Text;
public partial class Admin_dealgoodsimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDealGoodsImage_Click(object sender, EventArgs e)
    {
        string opath = string.Empty;
        string thumpath = string.Empty;
        string despath = string.Empty;
        string filename = string.Empty;
        string filepath = string.Empty;
        string fixpath = "images/userupload/goodsimage/";
        string extention = string.Empty;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        #region 默认图片的处理

        RetrieveCriteria rcGooos = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        Condition c = rcGooos.GetNewCondition();
        if (txtGoodsId.Text.Trim().Length > 0)
        {
            c.AddEqualTo(XiHuan_UserGoodsEntity.__ID, txtGoodsId.Text);
        }
        rcGooos.AddSelect(XiHuan_UserGoodsEntity.__ID);
        rcGooos.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rcGooos.OrderBy(XiHuan_UserGoodsEntity.__ID, false);
        c.AddNotEqualTo(XiHuan_UserGoodsEntity.__DEFAULTPHOTO, "images/none.jpg");
        EntityContainer goodscontainer = rcGooos.AsEntityContainer();
        foreach (XiHuan_UserGoodsEntity goods in goodscontainer)
        {
            if (goods.DefaultPhoto != null && goods.DefaultPhoto.Length > 0)
            {
                opath = Server.MapPath(goods.DefaultPhoto);
                filename = Path.GetFileNameWithoutExtension(opath);
                extention = Path.GetExtension(opath);
                if (File.Exists(opath))
                {
                    filepath = fixpath + goods.CreateDate.Year + "/" + goods.CreateDate.Month + "/" + goods.CreateDate.Day + "/";
                    despath = Server.MapPath(filepath + filename + extention);
                    if (!Directory.Exists(Server.MapPath(filepath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filepath));
                    }
                    if (!File.Exists(despath))
                    {
                        File.Copy(opath, despath, true);//复制原来的图片到新的路径
                        //File.Delete(opath);
                    }

                    thumpath = Server.MapPath(filepath + filename + GlobalVar.DefaultPhotoSize + extention);
                    PicHelper.MakeThumbnail(despath, thumpath, 85, 85);
                    thumpath = Server.MapPath(filepath + filename + GlobalVar.BigPhotoSize + extention);
                    PicHelper.MakeThumbnail(despath, thumpath, 200, 220);
                    sb.Append("Update XiHuan_UserGoods set DefaultPhoto='" + filepath + filename + GlobalVar.DefaultPhotoSize + extention + "' WHERE Id=" + goods.Id + ";");
                }
            }
        }

        #endregion

        if (sb.ToString().Length > 0)
        {
            Query.ProcessSqlNonQuery(sb.ToString(), GlobalVar.DataBase_Name);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //图片表还需要处理，不是默认图片的，需要将图片复制到新的路径，更新路径到数据库
        RetrieveCriteria rcgimages = new RetrieveCriteria(typeof(XiHuan_GoodsImageEntity));
        Condition c = rcgimages.GetNewCondition();
        if (txtGoodsId.Text.Trim().Length > 0)
        {
            c.AddEqualTo(XiHuan_GoodsImageEntity.__GOODSID, txtGoodsId.Text);
        }
        rcgimages.AddSelect(XiHuan_GoodsImageEntity.__ID);
        rcgimages.AddSelect(XiHuan_GoodsImageEntity.__CREATEDATE);
        rcgimages.AddSelect(XiHuan_GoodsImageEntity.__IMGSRC);
        EntityContainer imagecontainer = rcgimages.AsEntityContainer();
        string opath = string.Empty;
        string filename = string.Empty;
        string extention = string.Empty;
        string despath = string.Empty;
        string filepath = string.Empty;
        string fixpath = "images/userupload/goodsimage/";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (XiHuan_GoodsImageEntity goodsimage in imagecontainer)
        {
            opath = Server.MapPath(goodsimage.ImgSrc);
            filename = Path.GetFileNameWithoutExtension(opath);
            extention = Path.GetExtension(opath);
            if (File.Exists(opath))
            {
                filepath = fixpath + goodsimage.CreateDate.Year + "/" + goodsimage.CreateDate.Month + "/" + goodsimage.CreateDate.Day + "/";
                despath = Server.MapPath(filepath + filename + extention);
                if (!Directory.Exists(Server.MapPath(filepath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filepath));
                }
                if (!File.Exists(despath))
                {
                    File.Copy(opath, despath, true);//复制原来的图片到新的路径
                    //File.Delete(opath);
                }
                sb.Append("Update XiHuan_GoodsImage set ImgSrc='" + filepath + filename + extention + "' WHERE Id=" + goodsimage.Id + ";");
            }
        }
        if (sb.ToString().Length > 0)
        {
            Query.ProcessSqlNonQuery(sb.ToString(), GlobalVar.DataBase_Name);
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        #region 更新头像数据

        StringBuilder sbSql = new StringBuilder();
        string orignalpath = string.Empty;
        string newpath = string.Empty;
        DateTime dthead = DateTime.Now;
        DataTable dtUserHead = Query.ProcessSql(string.Format("SELECT  Id,HeadImage,RegisterDate FROM XiHuan_UserInfo  WHERE Id>={0} AND  Id<={1} AND HeadImage!='images/nophoto.gif';", txtStartId.Text, txtEndId.Text), GlobalVar.DataBase_Name);
        foreach (DataRow dr in dtUserHead.Rows)
        {
            orignalpath = CommonMethod.FinalString(dr["HeadImage"]);
            dthead = CommonMethod.ConvertToDateTime(dr["RegisterDate"], DateTime.Now);
            newpath = "images/userupload/headimage/" + dthead.Year + "/" + dthead.Month + "/" + dthead.Day + "/";
            if (orignalpath.Length > 0 && orignalpath.IndexOf("headimage") == -1 && File.Exists(Server.MapPath(orignalpath)))//头像图片存在
            {
                if (!Directory.Exists(Server.MapPath(newpath))) //新的目录不存在，需要创建目录
                {
                    Directory.CreateDirectory(Server.MapPath(newpath));
                }
                //将头像图片复制到新的路径
                File.Copy(Server.MapPath(orignalpath), Server.MapPath(orignalpath.Replace("images/userupload/", newpath)), true);
                //更新浏览记录里的用户头像地址
                sbSql.AppendFormat(@"UPDATE XiHuan_UserInfo SET HeadImage='{0}'  WHERE Id={1};UPDATE XiHuan_GoodsViewUser
                                                     SET VisitorHeadImage='{0}' WHERE VisitorId={1} ", orignalpath.Replace("images/userupload/", newpath), dr["Id"]);
            }
        }

        if (sbSql.ToString().Length > 0)//更新 
        {
            Query.ProcessSqlNonQuery(sbSql.ToString(), GlobalVar.DataBase_Name);
        }

        #endregion
    }
}
