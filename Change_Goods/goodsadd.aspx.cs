using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.IO;
public partial class goodsadd : BaseWebPage
{
    #region 初始化

    private bool IsEdit
    {
        get
        {
            return CommonMethod.ConvertToInt(Request["id"], 0) > 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "goodsadd.aspx");
            return;
        }

        if (!IsPostBack)
        {
            DataTable dt = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType();
            CommonMethod.BindDrop(ddlGoodType, dt, "TypeName", "Id");
            ddlGoodType.Items.Insert(0, new ListItem("选择换品类别", ""));
            CommonMethod.BindDrop(ddlGoodType1, dt, "TypeName", "Id");
            ddlGoodType1.Items.Insert(0, new ListItem("不限", ""));
            if (CurrentUser.IsStarUser != 1)
            {
                chkTJ.Checked = false;
                chkTJ.Enabled = false;
                chkTJ.ToolTip = "明星换客可使用此功能，将换品推荐到首页显示！";
            }
            if (IsEdit)
            {
                LoadGoodInfo();
            }
        }
    }

    #endregion

    #region 下拉框

    protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGoodChildType.Items.Clear();
        if (ddlGoodType.SelectedValue.Trim().Length > 0)
            CommonMethod.BindDrop(ddlGoodChildType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(ddlGoodType.SelectedValue), "Name", "Id");
        ddlGoodChildType.Items.Insert(0, new ListItem("选择小类", ""));
    }

    protected void ddlGoodType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGoodChildType1.Items.Clear();
        if (ddlGoodType1.SelectedValue.Trim().Length > 0)
            CommonMethod.BindDrop(ddlGoodChildType1, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(ddlGoodType1.SelectedValue), "Name", "Id");
        ddlGoodChildType1.Items.Insert(0, new ListItem("选择小类", ""));
        ddlGoodChildType1.SelectedIndex = 0;
    }

    #endregion

    #region 保存换品信息

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "goodsadd.aspx" + (IsEdit ? "?id=" + CommonMethod.FinalString(Request["id"]) : ""));
        }
        else
        {

            #region 服务器端验证 以后完善 
           
            #endregion

            #region 保存换品信息

            Transaction t = new Transaction();
            XiHuan_UserGoodsEntity newgoods = null;
            if (IsEdit)
                newgoods = XiHuan_UserGoodsEntityAction.RetrieveAXiHuan_UserGoodsEntity(CommonMethod.ConvertToInt(Request["id"], 0));
            else
                newgoods = new XiHuan_UserGoodsEntity();
            newgoods.OwnerId = CurrentUserId;
            newgoods.OwnerName = CurrentUserName;
            newgoods.Name = txtGoodName.Text.Trim();
            newgoods.IsTJ = chkTJ.Checked ? (byte)1 : (byte)0;
            newgoods.TypeId = CommonMethod.ConvertToInt(ddlGoodType.SelectedValue, 0);
            newgoods.ChildId = CommonMethod.ConvertToInt(ddlGoodChildType.SelectedValue, 0);
            newgoods.IsHavePhoto = rbtYes.Checked ? (byte)XiHuan_UserGoodsFacade.IsGoodHavePhoto.有 : (byte)XiHuan_UserGoodsFacade.IsGoodHavePhoto.无;
            newgoods.Description = txtGoodDesc.Value.Trim();
            newgoods.NewDeep = byte.Parse(ddlNewOldDeep.SelectedValue.Trim());
            newgoods.OnlyCityChange = chkValidCity.Checked ? (byte)1 : (byte)0;
            newgoods.OnlySchoolChange = chkValidSchool.Checked ? (byte)1 : (byte)0;
            newgoods.HopeToChangeTypeId = CommonMethod.ConvertToInt(ddlGoodType1.SelectedValue, 0);
            newgoods.HopeToChangeChildTypeId = CommonMethod.ConvertToInt(ddlGoodChildType1.SelectedValue, 0);
            newgoods.HopeToChangeDesc = txtHopeToChangeDesc.Text.Trim();
            newgoods.ProvinceId = CurrentUser.ProvinceId;
            newgoods.ProvinceName = CurrentUser.ProvinceName;
            newgoods.CityId = CurrentUser.CityId;
            newgoods.CityName = CurrentUser.CityName;
            newgoods.AreaId = CurrentUser.AreaId;
            newgoods.AreaName = CurrentUser.AreaName;
            newgoods.SchoolId = CurrentUser.SchoolId;
            newgoods.SchoolName = CurrentUser.SchoolName;
            if (!IsEdit)
            {
                newgoods.CreateDate = DateTime.Now;
                newgoods.ViewCount = new Random().Next(10, 30);
                newgoods.GoodState = (byte)XiHuan_UserGoodsFacade.GoodsState.新登记;
            }
            newgoods.IsChecked = (byte)(SystemConfigFacade.Instance().IsGoodsAddNeedCheck ? 0 : 1);
            t.DoSaveObject(newgoods);

            if (!IsEdit)
            {
                #region 换品图片上传

                string gooddefaultphoto = string.Empty;

                if (rbtYes.Checked)
                {

                    string extention = string.Empty;
                    int filesize = 0;
                    string filepath = string.Empty;
                    string savepath = string.Empty;
                    string filename = string.Empty;
                    HttpFileCollection goodimages = HttpContext.Current.Request.Files;
                    for (int i = 0; i < goodimages.Count; i++)
                    {

                        HttpPostedFile currentfile = goodimages[i];
                        extention = Path.GetExtension(currentfile.FileName);
                        filesize = currentfile.ContentLength;
                        filepath = "images/userupload/goodsimage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                        //不符合条件的直接跳过，不进行保存
                        if (!(currentfile.FileName.Length > 0) || filesize == 0 || !CommonMethod.IsUploadImageValid("", extention) || filesize > 500 * 1024)
                            continue;
                        else
                        {
                            if (!Directory.Exists(Server.MapPath(filepath)))
                            {
                                Directory.CreateDirectory(Server.MapPath(filepath));
                            }

                            filename = newgoods.Id.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                            currentfile.SaveAs(Server.MapPath(filepath + filename + extention));
                            XiHuan_GoodsImageEntity newgoodimage = new XiHuan_GoodsImageEntity();
                            newgoodimage.GoodsId = newgoods.Id;
                            newgoodimage.GoodsName = newgoods.Name;
                            newgoodimage.ImgSrc = filepath + filename + extention;
                            newgoodimage.CreateDate = DateTime.Now;

                            if (i == 0)
                            {
                                gooddefaultphoto = filepath + filename + GlobalVar.DefaultPhotoSize + extention;
                                newgoodimage.IsDefaultPhoto = 1;
                                //生成不同规格的图片
                                PicHelper.MakeThumbnail(Server.MapPath(filepath + filename + extention), Server.MapPath(filepath + filename + GlobalVar.DefaultPhotoSize + extention), 85, 85);
                                PicHelper.MakeThumbnail(Server.MapPath(filepath + filename + extention), Server.MapPath(filepath + filename + GlobalVar.BigPhotoSize + extention), 200, 220);
                            }

                            t.DoSaveObject(newgoodimage);

                        }
                    }
                }

                #endregion

                #region 默认图片保存
                string sql = string.Format("update XiHuan_UserGoods set DefaultPhoto='{0}' where Id={1} ", gooddefaultphoto.Length > 0 ? gooddefaultphoto : "images/none.jpg", newgoods.Id);
                t.DoSqlNonQueryString(sql, GlobalVar.DataBase_Name);
                #endregion

                #region 更新用户换品数量和积分，换币

                string updategoodsnumber = string.Format(@"update XiHuan_UserInfo set GoodsNumber=GoodsNumber+1, Score=Score+{0},HuanBi=HuanBi+{1} where Id={2}"
                                                          , SystemConfigFacade.Instance().AddScoreByAddGoods(), SystemConfigFacade.Instance().AddHBByAddGoods(), CurrentUser.ID);
                t.DoSqlNonQueryString(updategoodsnumber, GlobalVar.DataBase_Name);

                #endregion

                #region 浏览人

                XiHuan_GoodsViewUserEntity view = new XiHuan_GoodsViewUserEntity();
                view.GoodsId = newgoods.Id;
                view.Type = 0;
                view.VisitDate = DateTime.Now;
                view.VisitorName = "喜换网";
                view.VisitorId = 1;
                view.VisitorHeadImage = "images/userupload/20092113032102_1.png";
                t.DoSaveObject(view);

                #endregion
            }


            try
            {
                string detailurl = "goods/" + newgoods.CreateDate.Year + "/" + newgoods.CreateDate.Month + "/" + newgoods.CreateDate.Day + "/goods" + newgoods.Id + ".html";
                t.DoSqlNonQueryString("update XiHuan_UserGoods set DetailUrl='" + detailurl + "' where Id=" + newgoods.Id, GlobalVar.DataBase_Name);
                t.Commit();
                if (!SystemConfigFacade.Instance().IsGoodsAddNeedCheck)
                {
                    DataTable dt = Query.ProcessSql("select Id,DetailUrl,GoodState from XiHuan_UserGoods with(nolock) where OwnerId= " + CurrentUserId + " and IsChecked=1 ", GlobalVar.DataBase_Name);
                    foreach (DataRow dr in dt.Rows)
                    {
                        CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + dr["Id"], dr["DetailUrl"].ToString());
                    }
                }
                if (IsEdit)
                {
                    Alert("恭喜：换品信息保存成功" + (SystemConfigFacade.Instance().IsGoodsAddNeedCheck ? ",我们会尽快进行审核" : string.Empty) + "^_^！");
                }
                else
                {
                    Alert("恭喜：换品登记成功" + (SystemConfigFacade.Instance().IsGoodsAddNeedCheck ? ",我们会尽快进行审核" : string.Empty) + "^_^！");
                    SendMailFacade.sendEmail("418921050@qq.com,86386740@qq.com", "有人在喜换网发换品了", "有人在喜换网发换品:" + txtGoodName.Text);
                }

                ExecScript("window.location='goodlist.aspx?s='+Math.random();");
            }

            catch (Exception ex)
            {
                t.RollBack();
                Alert("抱歉：换品保存出错，" + ex.Message);
                return;
            }
            #endregion
        }
    }
    #endregion

    #region 修改时加载换品信息

    private void LoadGoodInfo()
    {
        TitleName.Text = "修改换品";
        btnSubmit.Text = "保存修改";
        XiHuan_UserGoodsEntity goodinfo = XiHuan_UserGoodsEntityAction.RetrieveAXiHuan_UserGoodsEntity(CommonMethod.ConvertToInt(Request["id"], 0));
        if (goodinfo != null)
        {
            txtGoodName.Text = goodinfo.Name;
            chkTJ.Checked = (goodinfo.IsTJ == 1);
            CommonMethod.SelectFlg(ddlGoodType, goodinfo.TypeId.ToString());
            CommonMethod.BindDrop(ddlGoodChildType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(goodinfo.TypeId.ToString()), "Name", "Id");
            CommonMethod.SelectFlg(ddlGoodChildType, goodinfo.ChildId.ToString());
            CommonMethod.SelectFlg(ddlGoodType1, goodinfo.HopeToChangeTypeId.ToString());
            CommonMethod.BindDrop(ddlGoodChildType1, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(goodinfo.HopeToChangeChildTypeId.ToString()), "Name", "Id");
            CommonMethod.SelectFlg(ddlGoodChildType1, goodinfo.HopeToChangeChildTypeId.ToString());
            trimage.Visible = uploadimage.Visible = false;
            txtGoodDesc.Value = goodinfo.Description;
            CommonMethod.SelectFlg(ddlNewOldDeep, goodinfo.NewDeep.ToString());
            chkValidCity.Checked = (goodinfo.OnlyCityChange == 1);
            chkValidSchool.Checked = (goodinfo.OnlySchoolChange == 1);
            txtHopeToChangeDesc.Text = goodinfo.HopeToChangeDesc;
        }

    }

    #endregion
}
