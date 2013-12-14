using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
public partial class schoolchange : BaseWebPage
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
        Repeater rptSchool = (Repeater)e.Item.FindControl("rptSchool");
        rptSchool.DataSource = ProvinceCityFacade.GetInstance().GetSchoolInfo(row["provinceId"].ToString(),"");
        rptSchool.DataBind();
    }
}
