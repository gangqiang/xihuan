using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public delegate void PageChangedDelegate(object sender, EventArgs e);


public partial class UC_PageControl : BaseUserControl
{
    public event PageChangedDelegate PageChanged;


    protected PagedDataSource m_DataSource;

    /// <summary>
    /// 数据源
    /// </summary>
    public PagedDataSource DataSource
    {
        get 
        {
            return m_DataSource;
        }
        set
        {
            m_DataSource = value;
            if (PageIndex > m_DataSource.PageCount - 1)
                PageIndex = m_DataSource.PageCount - 1;
            if (PageIndex < 0)
                PageIndex = 0;
            m_DataSource.CurrentPageIndex = PageIndex;
		    MarkPageStr();
        }
    }

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex
    {
        get
        {
            int i;
            if (!int.TryParse(txtPageIndex.Text, out i))
                i = 0;
            return i;
        }
        set
        {
            txtPageIndex.Text = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtPageIndex.Attributes["style"] = "display:none;";
        }
    }

    private void MarkPageStr()
{
		string pageStr = "<script type=\"text/javascript\">" +
			"function " + this.ClientID + "_ChangePage(i){" +
			"    document.getElementById('" + txtPageIndex.ClientID + "').value = i;" +
			"    __doPostBack('" + this.ClientID + "$" + btnChangePage.ID + "', '');" +
			"}\n" +
			"function " + ClientID + "_MoveTo(){" +
			"   var p = parseInt(document.getElementById('" + ClientID + "_txtMoveTo').value) - 1;" +
			"   " + ClientID + "_ChangePage(p);" +
			"}" +
			"</script>";


		int pageStart = 0;
		if (PageIndex > 5)
			pageStart = PageIndex - 5;
		int pageEnd = pageStart + 11;

		if (PageIndex > 0)
			pageStr += "<a href=\"javascript:" + this.ClientID + "_ChangePage(0);\">首页</a> ";

		if (PageIndex > 1)
			pageStr += "<a href=\"javascript:" + this.ClientID + "_ChangePage(" + (PageIndex - 1).ToString() + ");\">上页</a> ";

		for (int i = 0; i < DataSource.PageCount; i++)
		{
			if (i >= pageStart && i < pageEnd)
			{
				if (PageIndex != i)
                    pageStr += "<a class=\"pagelistnormal\" href=\"javascript:" + this.ClientID + "_ChangePage(" + i.ToString() + ");\" title=\"转到第" + (i + 1).ToString() + "页\">" + (i + 1).ToString() + "</a> ";
				else
                    pageStr += "<strong class=\"pagelistselected\">" + (i + 1).ToString() + "</strong> ";
			}
		}

		if (PageIndex < DataSource.PageCount - 2)
			pageStr += "<a href=\"javascript:" + this.ClientID + "_ChangePage(" + (PageIndex + 1).ToString() + ");\">下页</a> ";

		if (PageIndex < DataSource.PageCount - 1)
			pageStr += "<a href=\"javascript:" + this.ClientID + "_ChangePage(" + (DataSource.PageCount - 1).ToString() + ");\">末页</a> ";

		pageStr += "&nbsp;&nbsp;<input id=\"" + this.ClientID + "_txtMoveTo\" type=\"text\" class=\"textbox\" style=\"width:20px;height: 12px;font-size:12px\" value=\"" + (PageIndex + 1) + "\" />/" + DataSource.PageCount +
			" <input type=\"button\" class=\"button\"  value=\"转到\" onclick=\"" + this.ClientID + "_MoveTo();\" /> ";
		pageStr += "<span>共" + DataSource.DataSourceCount.ToString() + "条记录</span>";
		pageCtrl.Visible = true;
		pageCtrl.InnerHtml = pageStr;
		
    }

    protected void btnChangePage_Click(object sender, EventArgs e)
    {
        if (PageChanged != null)
            PageChanged(sender, e);
    }
}
