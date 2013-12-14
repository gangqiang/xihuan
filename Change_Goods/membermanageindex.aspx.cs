using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
using BusinessEntity;
using PersistenceLayer;
using System.IO;
public partial class membermanageindex : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 页面呈现

        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermanageindex.aspx");
        }

        if (!IsPostBack)
        {
            CommonMethod.BindDrop(ddlProvince, ProvinceCityFacade.GetInstance().GetProvince(), "province", "provinceId");
            ddlProvince.Items.Insert(0, new ListItem("请选择省份", ""));
            LoadUserInfo();
            headImage.Attributes.Add("onchange", "Javascript:ShowImg(this.value);");
        }
        #endregion
    }

    #region 下拉框

    protected void ddlProvince_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlCity.Items.Clear();
        ddlArea.Items.Clear();
        ddlSchool.Items.Clear();
        if (ddlProvince.SelectedValue.Trim().Length > 0)
        {
            CommonMethod.BindDrop(ddlCity, ProvinceCityFacade.GetInstance().GetCityInfo(ddlProvince.SelectedValue), "city", "cityId");
            CommonMethod.BindDrop(ddlSchool, ProvinceCityFacade.GetInstance().GetSchoolInfo(ddlProvince.SelectedValue, ddlCity.SelectedValue), "SchoolName", "Id");
        }
        ddlCity.Items.Insert(0, new ListItem("不限城市", ""));
        ddlArea.Items.Insert(0, new ListItem("不限地区", ""));
        ddlArea.SelectedIndex = 0;
        ddlSchool.Items.Insert(0, new ListItem("不限学校", ""));
        ddlCity.SelectedIndex = ddlArea.SelectedIndex = ddlSchool.SelectedIndex = 0;
    }
    protected void ddlCity_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlArea.Items.Clear();
        ddlSchool.Items.Clear();
        if (ddlCity.SelectedValue.Trim().Length > 0)
        {
            CommonMethod.BindDrop(ddlArea, ProvinceCityFacade.GetInstance().GetAreaInfo(ddlCity.SelectedValue), "area", "areaId");
            CommonMethod.BindDrop(ddlSchool, ProvinceCityFacade.GetInstance().GetSchoolInfo(ddlProvince.SelectedValue, ddlCity.SelectedValue), "SchoolName", "Id");
        }
        ddlArea.Items.Insert(0, new ListItem("不限地区", ""));
        ddlSchool.Items.Insert(0, new ListItem("不限学校", ""));
        ddlArea.SelectedIndex = ddlSchool.SelectedIndex = 0;
    }

    #endregion

    #region 加载注册信息
    private void LoadUserInfo()
    {
        int uid = CurrentUserId;
        XiHuan_UserInfoEntity userinfo = new XiHuan_UserInfoEntity();
        userinfo.ID = uid;
        userinfo.Retrieve();
        if (userinfo.IsPersistent)
        {
            headPic.ImageUrl = CommonMethod.FinalString(userinfo.HeadImage).Length > 0 ? userinfo.HeadImage : "images/nophoto.gif";
            lblUserName.Text = userinfo.UserName;
            txtEmail.Text = userinfo.Email;
            rbtSex.SelectedValue = userinfo.Gender.ToString();
            CommonMethod.SelectFlg(ddlProvince, userinfo.ProvinceId.ToString());
            CommonMethod.BindDrop(ddlCity, ProvinceCityFacade.GetInstance().GetCityInfo(ddlProvince.SelectedValue), "city", "cityId");
            CommonMethod.SelectFlg(ddlCity, userinfo.CityId.ToString());
            CommonMethod.BindDrop(ddlArea, ProvinceCityFacade.GetInstance().GetAreaInfo(ddlCity.SelectedValue), "area", "areaId");
            CommonMethod.SelectFlg(ddlArea, userinfo.AreaId.ToString());
            CommonMethod.BindDrop(ddlSchool, ProvinceCityFacade.GetInstance().GetSchoolInfo(ddlProvince.SelectedValue, ddlCity.SelectedValue), "SchoolName", "Id");
            ddlSchool.Items.Insert(0, new ListItem("选择学校", ""));
            CommonMethod.SelectFlg(ddlSchool, userinfo.SchoolId.ToString());
            txtQuestion.Text = userinfo.Question;
            txtAnswer.Text = userinfo.Answer;
            txtTel.Value = userinfo.TelePhone;
            txtWangWang.Value = userinfo.WangWang;
            txt_msn.Value = userinfo.Msn;
            txt_qq.Value = userinfo.QQ;
            txtOtherLink.Text = userinfo.OtherLink;
            txtSingnNote.Text = userinfo.SignNote;
        }
    }
    #endregion

    #region 保存修改后的注册信息

    protected void btn_submitinfo_ServerClick(object sender, EventArgs e)
    {
        #region 服务器端验证



        #endregion

        #region 信息保存

        Transaction t = new Transaction();
        int uid = CurrentUserId;
        if (uid > 0)
        {
            XiHuan_UserInfoEntity updateRegInfo = XiHuan_UserInfoEntityAction.RetrieveAXiHuan_UserInfoEntity(uid);
            if (updateRegInfo != null)
            {
                string savepath = string.Empty;
                string filepath = string.Empty;
                string filename = string.Empty;
                if (headImage.HasFile)
                {
                    if (headImage.PostedFile.ContentLength < 50 * 1024)
                    {
                        string hz = System.IO.Path.GetExtension(headImage.PostedFile.FileName);

                        if (CommonMethod.IsUploadImageValid("", hz))
                        {
                            filepath = "images/userupload/headimage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                            filename = DateTime.Now.ToString("yyyymmddhhmmssfff") + "_" + uid + hz;
                            if (!Directory.Exists(Server.MapPath(filepath)))
                            {
                                Directory.CreateDirectory(Server.MapPath(filepath));
                            }
                            savepath = Server.MapPath(filepath + filename);
                            headImage.PostedFile.SaveAs(savepath);
                        }
                        else
                        {
                            Alert("头像格式不符合要求，请重新选择头像！");
                            return;
                        }
                    }
                    else
                    {
                        Alert("头像大小超出50k,请换个小点的图片吧！");
                        return;
                    }
                }

                if (savepath.Length > 0)
                {
                    if (updateRegInfo.HeadImage.Length > 0 && System.IO.File.Exists(Server.MapPath(updateRegInfo.HeadImage)))
                    {
                        if (!updateRegInfo.HeadImage.Equals("images/nophoto.gif"))
                        {
                            System.IO.File.Delete(Server.MapPath(updateRegInfo.HeadImage));
                        }
                    }
                    updateRegInfo.HeadImage = filepath + filename;
                }
                updateRegInfo.Email = txtEmail.Text.Trim();
                updateRegInfo.Gender = byte.Parse(rbtSex.SelectedValue.Trim());
                updateRegInfo.ProvinceId = CommonMethod.ConvertToInt(ddlProvince.SelectedValue, 0);
                updateRegInfo.ProvinceName = ddlProvince.SelectedItem.Text.Trim();
                updateRegInfo.CityId = CommonMethod.ConvertToInt(Request["ddlCity"], 0);
                updateRegInfo.CityName = CommonMethodFacade.GetCityNameById(updateRegInfo.CityId.ToString());
                updateRegInfo.AreaId = CommonMethod.ConvertToInt(Request["ddlArea"], 0);
                if (updateRegInfo.AreaId > 0)
                    updateRegInfo.AreaName = CommonMethodFacade.GetAreaNameById(updateRegInfo.AreaId.ToString());
                int schoolid = CommonMethod.ConvertToInt(Request["ddlSchool"], 0);
                updateRegInfo.SchoolId = schoolid;
                if (schoolid > 0)
                {
                    updateRegInfo.SchoolName = CommonMethodFacade.GetSchoolNameById(ddlProvince.SelectedValue, Request["ddlCity"], schoolid.ToString());
                }
                else
                {
                    updateRegInfo.SchoolName = "";
                }

                updateRegInfo.Question = txtQuestion.Text.Trim();
                updateRegInfo.Answer = txtAnswer.Text.Trim();
                updateRegInfo.TelePhone = txtTel.Value.Trim();
                updateRegInfo.WangWang = txtWangWang.Value.Trim();
                updateRegInfo.QQ = txt_qq.Value.Trim();
                updateRegInfo.Msn = txt_msn.Value.Trim();
                updateRegInfo.OtherLink = txtOtherLink.Text.Trim();
                updateRegInfo.SignNote = CommonMethod.ClearInputText(txtSingnNote.Text, 200);
                t.DoSaveObject(updateRegInfo);
                //更新换品浏览人信息里的头像地址
                t.DoSqlNonQueryString("update XiHuan_GoodsViewUser set VisitorHeadImage='" + updateRegInfo.HeadImage + "' where VisitorId=" + CurrentUserId, GlobalVar.DataBase_Name);
                try
                {
                    t.Commit();
                    headPic.ImageUrl = filepath + filename;
                    LoadUserInfo();
                    Alert("恭喜：您的注册信息已成功更改！");
                    DataTable dt = Query.ProcessSql("select Id,DetailUrl from XiHuan_UserGoods with(nolock) where OwnerId= " + CurrentUserId + " and IsChecked=1 ", GlobalVar.DataBase_Name);
                    foreach (DataRow dr in dt.Rows)
                    {
                        CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + dr["Id"], dr["DetailUrl"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Alert("抱歉：修改信息时出错，" + ex.Message + "请稍后重试！");
                    return;
                }

            }
        }
        else
        {
            Alert("登陆超时，请重新登陆再继续当前的操作！");
            return;
        }

        #endregion
    }

    #endregion
}
