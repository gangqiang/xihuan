using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessFacade;
public partial class UC_Top : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType();
            CommonMethod.BindDrop(ddlGoodType, dt, "TypeName", "Id");
            ddlGoodType.Items.Insert(0, new ListItem("不限", ""));
        }
    }

}
