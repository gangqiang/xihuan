using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
public partial class citychange : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptProvince.DataSource = ProvinceCityFacade.GetInstance().GetProvince();
            rptProvince.DataBind();
        }

    }

    protected void rptProvince_ItemDataBound1(object sender, RepeaterItemEventArgs e)
    {
        DataRowView row = (DataRowView)e.Item.DataItem;
        Repeater rptCity = (Repeater)e.Item.FindControl("rptCity");
        rptCity.DataSource = ProvinceCityFacade.GetInstance().GetCityInfo(row["provinceId"].ToString());
        rptCity.DataBind();
    }
}
