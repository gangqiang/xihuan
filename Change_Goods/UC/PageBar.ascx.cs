using System;
using System.Collections.Generic;
public partial class UC_PageBar : System.Web.UI.UserControl
{
    private int m_PageSize = 10;
    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize
    {
        get { return m_PageSize; }
        set { m_PageSize = value; }
    }


    private int m_RecordCount = 0;
    /// <summary>
    /// 总记录数
    /// </summary>
    public int RecordCount
    {
        get { return m_RecordCount; }
        set { m_RecordCount = value; }
    }


    /// <summary>
    /// 获取页面总数
    /// </summary>
    public int PageCount
    {
        get { return RecordCount % PageSize == 0 ? RecordCount / PageSize : (int)Math.Ceiling(RecordCount * 1.0M / PageSize); }
    }

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex
    {
        get
        {
            return CommonMethod.ConvertToInt(Request[ParamName_PageNo], 0);
        }
    }


    private string m_CmdPreviousText = "上一页";
    /// <summary>
    /// 上一页按钮文本
    /// </summary>
    public string CmdPreviousText
    {
        get { return m_CmdPreviousText; }
        set { m_CmdPreviousText = value; }
    }


    private string m_CmdNextText = "下一页";
    /// <summary>
    /// 下一页按钮文本
    /// </summary>
    public string CmdNextText
    {
        get { return m_CmdNextText; }
        set { m_CmdNextText = value; }
    }

    private string paramname_pageno = "page";

    public string ParamName_PageNo
    {
        get { return paramname_pageno; }
        set { paramname_pageno = value; }
    }

    /// <summary>
    /// 创建客户端分页控件
    /// </summary>
    public void Draw()
    {
        int pageCount = PageCount;

        string pageStr = "";

        int pageStart = 0;
        if (PageIndex > 5)
            pageStart = PageIndex - 5;
        int pageEnd = pageStart + 11;

        if (PageIndex > 0)
            pageStr += "<a href=\"" + MakeLink(0) + "\">首页</a> ";

        if (PageIndex > 0)
            pageStr += "<a href=\"" + MakeLink(PageIndex - 1) + "\">" + CmdPreviousText + "</a> ";

        if (pageStart > 0)
            pageStr += "<a href=\"" + MakeLink(pageStart - 1) + "\">...</a> ";

        for (int i = 0; i < pageCount; i++)
        {
            if (i >= pageStart && i < pageEnd)
            {
                if (PageIndex != i)
                    pageStr += "<a class=\"pagelistnormal\" href=\"" + MakeLink(i) + "\" title=\"转到第" + (i + 1) + "页\">" + (i + 1).ToString() + "</a> ";
                else
                    pageStr += "<strong class=\"pagelistselected\">" + (i + 1) + "</strong> ";
            }
        }

        if (pageEnd < pageCount)
            pageStr += "<a href=\"" + MakeLink(pageEnd) + "\">...</a> ";

        if (PageIndex < pageCount - 1)
            pageStr += "<a href=\"" + MakeLink(PageIndex + 1) + "\">" + CmdNextText + "</a> ";

        if (PageIndex < pageCount - 1)
            pageStr += "<a href=\"" + MakeLink(pageCount - 1) + "\">末页</a> ";

        pageCtrl.InnerHtml = pageStr + "共" + PageCount.ToString() + "页" + RecordCount.ToString() + "条记录";
    }

    private string MakeLink(int pageindex)
    {
        List<string> queryString = new List<string>();
        foreach (string key in Request.QueryString.Keys)
        {
            if (key != null && !key.Equals(ParamName_PageNo))
            {
                queryString.Add(key + "=" + Microsoft.JScript.GlobalObject.escape(Request.QueryString[key]));
            }
        }

        queryString.Add(ParamName_PageNo + "=" + pageindex);
        string filePath = Request.CurrentExecutionFilePath;
        return String.Format(filePath + "?{0}", String.Join("&", queryString.ToArray()));
    }
}
