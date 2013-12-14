

using System;

public class PageChangeEventArgs : EventArgs
{
    public PageChangeEventArgs()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        m_PageIndex = 0;
    }
    private int m_PageIndex;
    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex
    {
        get { return m_PageIndex; }
        set { m_PageIndex = value; }
    }
}
