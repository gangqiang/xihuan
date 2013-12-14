using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
using Microsoft.JScript;
public partial class searchllist : BaseWebPage
{
    protected string title = string.Empty;

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonMethod.BindDrop(ddlProvince, ProvinceCityFacade.GetInstance().GetProvince(), "province", "provinceId");
            ddlProvince.Items.Insert(0, new ListItem("不限省份", ""));
            CommonMethod.BindDrop(ddlGoodType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType(), "TypeName", "Id");
            ddlGoodType.Items.Insert(0, new ListItem("不限类别", ""));
            if (CommonMethod.FinalString(Request["typeid"]).Length > 0)
                CommonMethod.SelectFlg(ddlGoodType, Request["typeid"]);
            if (CommonMethod.FinalString(Request["childid"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlGoodChildType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(Request["typeid"]), "Name", "Id");
                CommonMethod.SelectFlg(ddlGoodChildType, Request["childid"]);
                ddlGoodChildType.Items.Insert(0, new ListItem("不限子类别", ""));
            }

            if (CommonMethod.FinalString(Request["province"]).Length > 0)
                CommonMethod.SelectFlg(ddlProvince, Request["province"]);
            if (CommonMethod.FinalString(Request["city"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlCity, ProvinceCityFacade.GetInstance().GetCityInfo(Request["province"]), "city", "cityID");
                CommonMethod.SelectFlg(ddlCity, Request["city"]);
                ddlCity.Items.Insert(0, new ListItem("不限城市", ""));
            }
            if (CommonMethod.FinalString(Request["area"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlArea, ProvinceCityFacade.GetInstance().GetAreaInfo(Request["city"]), "area", "areaId");
                CommonMethod.SelectFlg(ddlArea, Request["area"]);
                ddlArea.Items.Insert(0, new ListItem("不限地区", ""));
            }
            if (CommonMethod.FinalString(Request["school"]).Length > 0)
            {
                CommonMethod.BindDrop(ddlSchool, ProvinceCityFacade.GetInstance().GetSchoolInfo(Request["province"], Request["city"]), "SchoolName", "Id");
                CommonMethod.SelectFlg(ddlSchool, Request["school"]);
                ddlSchool.Items.Insert(0, new ListItem("不限学校", ""));
            }

            if (CommonMethod.FinalString(Request["keyword"]).Length > 0)
                txtGoodName.Text = GlobalObject.unescape(Request["keyword"]);
            if (CommonMethod.FinalString(Request["ownername"]).Length > 0)
                txtOwnerName.Text = GlobalObject.unescape(Request["ownername"]);
            CommonMethod.SelectFlg(ddlTime, Request["time"]);
            CommonMethod.SelectFlg(ddlNewOldDeep, Request["newdeep"]);
            CommonMethod.SelectFlg(ddlOrderBy, Request["sort"]);
            chkHavePhoto.Checked = (CommonMethod.FinalString(Request["ph"]).Equals("true"));
            chkShow.Checked = (CommonMethod.FinalString(Request["sc"]).Equals("true"));
            BindData();
        }

        lblLinkName.Text = "";
        string area = CommonMethod.FinalString(Request["changearea"]);

        if (area.Length > 0)
        {
            string type = "同城换区";
            string url = "citychange.html";
            if (area.Equals("school"))
            {
                type = "校园换区";
                url = "schoolchange.html";
            }
            if (area.Equals("idea"))
            {
                type = "创意换区";
                url = "searchlist.aspx?type=idea";
            }

            lblLinkName.Text += string.Format(">><a href=\"{1}\">{0}</a>", type, url);
            title += "-" + type;

        }

        if (ddlProvince.SelectedValue.Trim().Length > 0 )
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?province={0}\">{1}</a>", ddlProvince.SelectedValue, ddlProvince.SelectedItem.Text);
            title += "-" + ddlProvince.SelectedItem.Text;
        }
        if (ddlCity.SelectedValue.Trim().Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?city={0}\">{1}</a>", ddlCity.SelectedValue, ddlCity.SelectedItem.Text);
            title += "  " + ddlCity.SelectedItem.Text;
        }
        if (ddlSchool.SelectedValue.Trim().Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?province={2}&city={3}&school={0}\">{1}</a>", ddlSchool.SelectedValue, ddlSchool.SelectedItem.Text, ddlProvince.SelectedValue, ddlCity.SelectedValue);
            title += "  " + ddlSchool.SelectedItem.Text;
        }
        if (ddlGoodType.SelectedValue.Trim().Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?typeid={0}\">{1}</a>", ddlGoodType.SelectedValue, ddlGoodType.SelectedItem.Text);
            title += "-" + ddlGoodType.SelectedItem.Text;
        }

        if (ddlGoodChildType.SelectedValue.Trim().Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?typeid={0}&childid={1}\">{2}</a>", ddlGoodType.SelectedValue, ddlGoodChildType.SelectedValue, ddlGoodChildType.SelectedItem.Text);
            title += "-" + ddlGoodChildType.SelectedItem.Text;
        }

        if (txtGoodName.Text.Trim().Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"searchlist.aspx?keyword={0}\">{1}</a>", Microsoft.JScript.GlobalObject.escape(txtGoodName.Text.Trim()), txtGoodName.Text.Trim());
            title += "-" + txtGoodName.Text;
        }
        if (CommonMethod.FinalString(Request["ownername"]).Length > 0 && CommonMethod.FinalString(Request["ownerid"]).Length > 0)
        {
            lblLinkName.Text += string.Format(">><a href=\"xh.aspx?id={0}\">{1}</a>的所有换品</a>", Request["ownerid"], Server.UrlDecode(Request["ownername"]));
            title += "-" + Server.UrlDecode(Request["ownername"]) + " 的所有换品";
        }
    }
    #endregion

    #region 下拉框

    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGoodChildType.Items.Clear();
        if (ddlGoodType.SelectedValue.Trim().Length > 0)
            CommonMethod.BindDrop(ddlGoodChildType, XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(ddlGoodType.SelectedValue), "Name", "Id");
        ddlGoodChildType.Items.Insert(0, new ListItem("不限子类别", ""));
        ddlGoodChildType.SelectedIndex = 0;
    }

    #endregion

    #region 数据查询

    private void BindData()
    {
        XiHuan_UserGoodsSearchFilter f = new XiHuan_UserGoodsSearchFilter();
        f.SelectFileds = @" g.Id,g.Name,g.DefaultPhoto,g.ViewCount,g.CreateDate,g.OwnerId,g.OwnerName,
                            g.NewDeep,g.OnlyCityChange,g.OnlySchoolChange,g.HopeToChangeTypeId,g.HopeToChangeChildTypeId,
                            g.HopeToChangeDesc,g.ProvinceName,g.CityName,g.AreaName,g.SchoolName,g.GoodState,g.DetailUrl,g.IsChecked";
        f.GoodsName = txtGoodName.Text;
        f.GoodsTypeId = CommonMethod.ConvertToInt(ddlGoodType.SelectedValue, int.MaxValue);
        f.GoodsSceondTypeId = CommonMethod.ConvertToInt(ddlGoodChildType.SelectedValue, int.MaxValue);
        f.ProvinceId = CommonMethod.ConvertToInt(ddlProvince.SelectedValue, int.MaxValue);
        f.CityId = CommonMethod.ConvertToInt(ddlCity.SelectedValue, int.MaxValue);
        f.AreaId = CommonMethod.ConvertToInt(ddlArea.SelectedValue, int.MaxValue);
        f.SchooId = CommonMethod.ConvertToInt(ddlSchool.SelectedValue, int.MaxValue);
        f.NewDeep = CommonMethod.ConvertToInt(ddlNewOldDeep.SelectedValue, int.MaxValue);
        f.IsHavePhoto = chkHavePhoto.Checked ? 1 : int.MaxValue;
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

        f.IsChecked = 1;
        if (chkShow.Checked)
            f.GoodsStateNotIn = XiHuan_UserGoodsFacade.GoodsState.交换中.ToString("d") + "," + XiHuan_UserGoodsFacade.GoodsState.交换成功.ToString("d");
        f.OrderByParam = ddlOrderBy.SelectedValue + "desc ";
        f.OwnerId = CommonMethod.ConvertToInt(Request["ownerid"], int.MaxValue);
        f.OwnerName = txtOwnerName.Text;
        f.PageIndex = PageBar1.PageIndex;
        int rowcount;
        DataTable dt = XiHuan_UserGoodsFacade.SearchGoods(f, out rowcount);
        rptGoodsList.DataSource = dt;
        rptGoodsList.DataBind();
        PageBar1.RecordCount = rowcount;
        PageBar1.Draw();
    }

    #endregion

    #region 页面呈现

    protected string ChangeRequire(string pid, string cid, string onlycitychange, string onlyschoolchagne)
    {
        string typenaem = XiHuan_UserGoodsFacade.GetTypeNameById(pid);
        string childname = XiHuan_UserGoodsFacade.GetSecondTypeNameById(pid, cid);
        string city = onlycitychange.Equals("1") ? "限同城交换" : "";
        string school = onlyschoolchagne.Equals("1") ? "限同校交换" : "";
        return string.Format("类别要求：<span class=\"inittext\">{0}</span><br/>交换条件：<span class=\"inittext\">{1}</span>",
            (typenaem.Length == 0 && childname.Length == 0 ? "不限" : typenaem + "&nbsp;" + childname),
            (city.Length == 0 && school.Length == 0 ? "不限" : city + "&nbsp;" + school)
            );
    }

    #endregion 

}
