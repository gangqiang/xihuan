using System.Web.UI;

/// <summary>
/// BaseUserControl 的摘要说明
/// </summary>
public class BaseUserControl : UserControl
{
    protected BasePage m_ParentPage;

    public BaseUserControl()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //ss
    }

    /// <summary>
    /// 容器页
    /// </summary>
    public BasePage ParentPage
    {
        get { return Page as BasePage; }
    }

}
