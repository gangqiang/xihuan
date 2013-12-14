using System;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.IO;
public partial class uploadgoodspic : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = CommonMethod.FinalString(Request["action"]);
            if (action.Equals("addNewPic"))
            {
                trOldPic.Visible = false;
                btnUpLoad.Text = " 上传图片 ";
                notice.Visible = false;
            }
            else if (action.Equals("ModifyPic"))
            {
                OldImage.ImageUrl = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["src"]));
                txtImgDesc.Text = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["desc"]));
                chkIsDefault.Checked = CommonMethod.FinalString(Request["isdefault"]).Equals("1");
                btnUpLoad.Text = " 保存修改 ";
            }
        }
    }
    #endregion

    #region 保存
    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        string action = CommonMethod.FinalString(Request["action"]);
        if (action.Equals("addNewPic") && !flpImage.HasFile)
        {
            Alert("您没有选择要上传的图片！");
            return;
        }
        else
        {
            XiHuan_GoodsImageEntity newgoodimage = null;
            if (action.Equals("addNewPic"))
            {
                newgoodimage = new XiHuan_GoodsImageEntity();
            }
            if (action.Equals("ModifyPic")) //删除旧图片
            {
                newgoodimage = XiHuan_GoodsImageEntityAction.RetrieveAXiHuan_GoodsImageEntity(CommonMethod.ConvertToInt(Request["id"], 0));
            }

            newgoodimage.GoodsId = CommonMethod.ConvertToInt(Request["gid"], 0);
            newgoodimage.GoodsName = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["gname"]));
            newgoodimage.ImgDesc = txtImgDesc.Text.Trim();
            newgoodimage.CreateDate = DateTime.Now;
            newgoodimage.IsDefaultPhoto = (byte)(chkIsDefault.Checked ? 1 : 0);

            if (flpImage.HasFile)
            {
                if (chkIsDefault.Checked)//把原来的默认图片更新
                {
                    Query.ProcessSqlNonQuery("update XiHuan_GoodsImage set IsDefaultPhoto=0 where IsDefaultPhoto=1 and GoodsId=" + CommonMethod.FinalString(Request["gid"]), GlobalVar.DataBase_Name);
                }
                string extention = Path.GetExtension(flpImage.FileName);
                int filesize = flpImage.PostedFile.ContentLength;
                string filepath = "images/userupload/goodsimage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                if (!CommonMethod.IsUploadImageValid("", extention))
                {
                    Alert("您选择的图片格式不正确！");
                    return;
                }
                if (filesize > 500 * 1024)
                {
                    Alert("您选择的图片太大了，超过500k了，换个小点的吧！");
                    return;
                }
                if (!Directory.Exists(Server.MapPath(filepath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filepath));
                }

                filepath += CommonMethod.FinalString(Request["gid"]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + extention;
                flpImage.PostedFile.SaveAs(Server.MapPath(filepath));
                if (action.Equals("ModifyPic"))
                {
                    string imgsrc = CommonMethod.FinalString(newgoodimage.ImgSrc);
                    if (imgsrc.Length > 0 && File.Exists(Server.MapPath(imgsrc)) && !imgsrc.Equals("images/none.jpg"))
                    {
                        File.Delete(Server.MapPath(imgsrc));
                        //如果有缩略图也要删除掉
                        string imgname = Path.GetFileNameWithoutExtension(Server.MapPath(imgsrc));
                        imgsrc = Server.MapPath(newgoodimage.ImgSrc.Replace(imgname, imgname + GlobalVar.DefaultPhotoSize));
                        if (File.Exists(imgsrc))
                        {
                            File.Delete(imgsrc);
                        }
                        imgsrc = Server.MapPath(newgoodimage.ImgSrc.Replace(imgname, imgname + GlobalVar.BigPhotoSize));
                        if (File.Exists(imgsrc))
                        {
                            File.Delete(imgsrc);
                        }
                    }
                }
                newgoodimage.ImgSrc = filepath;
            }

            newgoodimage.Save();
            if (chkIsDefault.Checked) //更改默认图片
            {
                string filename = Path.GetFileNameWithoutExtension(Server.MapPath(newgoodimage.ImgSrc));
                //生成缩略图
                PicHelper.MakeThumbnail(Server.MapPath(newgoodimage.ImgSrc), Server.MapPath(newgoodimage.ImgSrc.Replace(filename, filename + GlobalVar.DefaultPhotoSize)), 85, 85);
                PicHelper.MakeThumbnail(Server.MapPath(newgoodimage.ImgSrc), Server.MapPath(newgoodimage.ImgSrc.Replace(filename, filename + GlobalVar.BigPhotoSize)), 200, 220);

                Query.ProcessSqlNonQuery(string.Format("update XiHuan_UserGoods set IsHavePhoto=1,DefaultPhoto='{0}' where Id={1} ",
                    newgoodimage.ImgSrc.Replace(filename, filename + GlobalVar.DefaultPhotoSize), newgoodimage.GoodsId), GlobalVar.DataBase_Name);
            }

            Alert("恭喜：操作成功！");
            ExecScript(string.Format("parent.location='modifypic.aspx?id={0}&name={1}'", CommonMethod.FinalString(Request["gid"]), Server.UrlEncode(CommonMethod.FinalString(Request["gname"]))));
        }

    }
    #endregion
}
