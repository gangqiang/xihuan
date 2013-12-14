using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
public partial class userlist : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonMethod.BindDrop(ddlProvince, ProvinceCityFacade.GetInstance().GetProvince(), "province", "provinceId");
            ddlProvince.Items.Insert(0, new ListItem("不限省份", ""));
            if (CommonMethod.FinalString(Request["province"]).Length > 0)
                CommonMethod.SelectFlg(ddlProvince, Request["province"]);
            if (CommonMethod.FinalString(Request["type"]).Equals("star"))
            {
                lblLinkName.Text = ">>明星换客";
                chkStarUser.Checked = true;
            }

            if (CommonMethod.FinalString(Request["keyword"]).Length > 0)
            {
                txtGoodName.Text = Microsoft.JScript.GlobalObject.unescape(Request["keyword"]);
                lblLinkName.Text = ">>" + txtGoodName.Text;
            }
            CommonMethod.SelectFlg(ddlProvince, Request["province"]);
            if (CommonMethod.FinalString(Request["city"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlCity, ProvinceCityFacade.GetInstance().GetCityInfo(Request["province"]), "city", "cityID");
                ddlCity.Items.Insert(0, new ListItem("不限城市", ""));
            }
            if (CommonMethod.FinalString(Request["area"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlArea, ProvinceCityFacade.GetInstance().GetAreaInfo(Request["city"]), "area", "areaId");
                ddlArea.Items.Insert(0, new ListItem("不限地区", ""));
            }
            CommonMethod.SelectFlg(ddlCity, Request["city"]);
            if (CommonMethod.FinalString(Request["school"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlSchool, ProvinceCityFacade.GetInstance().GetSchoolInfo(Request["province"], Request["city"]), "SchoolName", "Id");
                ddlSchool.Items.Insert(0, new ListItem("不限学校", ""));
            }
            CommonMethod.SelectFlg(ddlArea, Request["area"]);
            CommonMethod.SelectFlg(ddlSchool, Request["school"]);
            CommonMethod.SelectFlg(ddlTime, Request["time"]);
            CommonMethod.SelectFlg(ddlOrderBy, Request["sort"]);
            CommonMethod.SelectFlg(ddlGender, Request["gender"]);
            chkHavePhoto.Checked = (CommonMethod.FinalString(Request["ph"]).Equals("true"));
            BindData();
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


    private void BindData()
    {
        XiHuan_UserSearchFilter f = new XiHuan_UserSearchFilter();
        f.SelectFileds = " u.Id,u.UserName,u.HeadImage,u.ProvinceName,u.CityName,u.AreaName,u.SchoolName,u.RegisterDate,u.LastLoginTime,u.GoodsNumber,u.QQ,u.WangWang  ";
        f.UserName = txtGoodName.Text;
        f.ProvinceId = CommonMethod.ConvertToInt(ddlProvince.SelectedValue, int.MaxValue);
        f.CityId = CommonMethod.ConvertToInt(Request["city"], int.MaxValue);
        f.AreaId = CommonMethod.ConvertToInt(Request["ddlArea"], int.MaxValue);
        f.SchooId = CommonMethod.ConvertToInt(Request["school"], int.MaxValue);
        f.IsHavePhoto = chkHavePhoto.Checked ? 1 : int.MaxValue;
        f.IsStartUser = chkStarUser.Checked ? 1 : int.MaxValue;
        f.Gender = CommonMethod.ConvertToInt(ddlGender.SelectedValue, int.MaxValue);
        string value = ddlTime.SelectedValue.Trim();
        if (value.Trim().Length > 0)
        {
            DateTime dtbegin = DateTime.MinValue;
            if (value.Equals("0"))
                dtbegin = DateTime.Now.AddDays(-7);
            if (value.Equals("1"))
                dtbegin = DateTime.Now.AddMonths(-1);
            if (value.Equals("2"))
                dtbegin = DateTime.Now.AddMonths(-3);
            f.CreateDateBegin = dtbegin;
            f.CreateDateEnd = DateTime.Now;
        }

        f.OrderByParam = ddlOrderBy.SelectedValue + " desc ";
        f.PageIndex = PageBar1.PageIndex;
        int rowcount = 0;
        DataTable dt = XiHuan_UserFacade.SearchUser(f, out rowcount);
        rptGoodsList.DataSource = dt;
        rptGoodsList.DataBind();
        PageBar1.RecordCount = rowcount;
        PageBar1.Draw();
    }


}
