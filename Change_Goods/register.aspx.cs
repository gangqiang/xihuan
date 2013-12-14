using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
using System.Text.RegularExpressions;
public partial class register : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonMethod.BindDrop(ddlProvince, ProvinceCityFacade.GetInstance().GetProvince(), "province", "provinceId");
            ddlProvince.Items.Insert(0, new ListItem("请选择省份", ""));
        }

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

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {

        #region 服务器端验证

        int provinceid = CommonMethod.ConvertToInt(ddlProvince.SelectedValue, 0);
        int cityid = CommonMethod.ConvertToInt(ddlCity.SelectedValue, 0);
        int areaid = CommonMethod.ConvertToInt(ddlArea.SelectedValue, 0);
        int schoolid = CommonMethod.ConvertToInt(ddlSchool.SelectedValue, 0);
        if (txtUserName.Text.Trim().Length == 0)
        {
            Alert(" 请您填写换客名！");
            Select(txtUserName);
            return;
        }

        else if (XiHuan_UserFacade.IsUserNameAlreayUse(txtUserName.Text))
        {
            Alert("您填写的换客名已经被占用，请您换个换客名重试！");
            Select(txtUserName);
            return;
        }

        if (txtPassWord.Text.Trim().Length == 0)
        {
            Alert("请您填写密码！");
            Select(txtPassWord);
            return;
        }

        if (txtPassWord2.Text.Trim().Length == 0)
        {
            Alert("请您再次输入密码！");
            Select(txtPassWord2);
            return;
        }

        if (!txtPassWord.Text.Trim().Equals(txtPassWord2.Text.Trim()))
        {
            Alert("两次密码输入不一致，请重新输入！");
            Select(txtPassWord2);
            return;
        }

        if (txtEmail.Text.Trim().Length > 0)
        {
            Regex reg = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!reg.IsMatch(txtEmail.Text.Trim()))
            {
                Alert("您的邮箱格式填写不正确！");
                Select(txtEmail);
                return;
            }
        }

        if (provinceid == 0)
        {
            Alert("请您选择省份！");
            return;
        }

        if (cityid == 0)
        {
            Alert("请您选择城市！");
            return;
        }

        if (txtQuestion.Text.Trim().Length == 0)
        {
            Alert("请您填写安全提问问题！");
            Select(txtQuestion);
            return;
        }

        if (txtAnswer.Text.Trim().Length == 0)
        {
            Alert("请您填写安全提问答案！");
            Select(txtAnswer);
            return;
        }

        #endregion

        #region 用户基本信息

        Transaction t = new Transaction();
        XiHuan_UserInfoEntity NewUser = new XiHuan_UserInfoEntity();
        NewUser.UserType = (int)XiHuan_UserFacade.UserType.个人注册;
        NewUser.UserName = txtUserName.Text.Trim();
        NewUser.OrignalPwd = txtPassWord.Text.Trim();
        NewUser.Md5Pwd = CommonMethod.MD5Encrypt(txtPassWord.Text.Trim());
        NewUser.Email = txtEmail.Text.Trim();
        NewUser.Gender = Rad_sex_0.Checked ? (byte)1 : (byte)0;
        NewUser.ProvinceId = provinceid;
        NewUser.ProvinceName = ddlProvince.SelectedItem.Text;
        NewUser.CityId = cityid;
        if (cityid > 0)
            NewUser.CityName = CommonMethodFacade.GetCityNameById(cityid.ToString());
        NewUser.AreaId = areaid;
        if (areaid > 0)
            NewUser.AreaName = CommonMethodFacade.GetAreaNameById(areaid.ToString());
        if (schoolid > 0)
        {
            NewUser.SchoolId = schoolid;
            NewUser.SchoolName = CommonMethodFacade.GetSchoolNameById(provinceid.ToString(), cityid.ToString(), schoolid.ToString());
        }

        NewUser.HuanBi = SystemConfigFacade.Instance().RegisterAddHuanBi();
        NewUser.Score = SystemConfigFacade.Instance().RegisterAddScore();
        NewUser.RegisterDate = NewUser.LastLoginTime = DateTime.Now;
        NewUser.Question = txtQuestion.Text.Trim();
        NewUser.Answer = txtAnswer.Text.Trim();
        NewUser.HeadImage = "images/nophoto.gif";
        t.DoSaveObject(NewUser);

        #endregion

        #region 推荐人积分换币更新
        if (txtTuiJianMember.Text.Trim().Length > 0)
        {
            string updateTJ = string.Format("update XiHuan_UserInfo set Score=Score+{0},HuanBi=HuanBi+{1} where UserName='{2}' ",
                SystemConfigFacade.Instance().TuiJianAddScore(), SystemConfigFacade.Instance().TuiJianAddHuanBi(), txtTuiJianMember.Text.Trim());

            t.DoSqlNonQueryString(updateTJ, GlobalVar.DataBase_Name);
        }
        #endregion

        #region 给新注册用户发送短消息

        XiHuan_MessageFacade.SendNewMessage(1, NewUser.ID, "喜换网", NewUser.UserName, "尊敬的会员" + NewUser.UserName + "," + SystemConfigFacade.Instance().RegMesContent(), t, true);

        #endregion

        try
        {
            t.Commit();

            #region 注册完成后自动登陆调转到个人管理中心

            Alert("恭喜：您的注册信息已成功提交！");
            CommonMethod.AddLoginCookie(NewUser.ID, NewUser.UserName, DateTime.MinValue);
            SendMailFacade.sendEmail(CommonMethodFacade.GetConfigValue("NoticeEmail"), "有人在喜换网注册了", "有人在喜换网注册了:" + txtUserName.Text);
            Response.Redirect("membercenter.aspx?action=" + Server.UrlEncode("membermanageindex.aspx"));

            #endregion
        }
        catch (Exception ex)
        {
            t.RollBack();
            Alert("抱歉：注册信息提交失败," + ex.Message + "请重试！");
            return;
        }

    }
}
