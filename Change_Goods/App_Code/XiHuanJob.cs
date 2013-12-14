//======================================================================
// Copyright (c) 苏州同程旅游网络科技有限公司. All rights reserved.
// 所属项目：Change_Goods
// 创 建 人：wgq
// 创建日期：2010-12-6 17:30:33
// 用    途：系统定时执行程序
//====================================================================== 

using System;
using System.Data;
using Quartz;
using PersistenceLayer;
using BusinessFacade;
using BusinessEntity;
using System.Net;

/// <summary>
///在此描述XiHuanJob的说明
/// </summary>
public class XiHuanJob : IJob
{
    public XiHuanJob()
    {
    }

    #region IJob 成员

    public void Execute(JobExecutionContext context)
    {
        SendMailFacade.sendEmail(CommonMethodFacade.GetConfigValue("NoticeEmail"), " 进行了自动触发程序执行操作！", "进行了自动触发程序执行操作");
        HttpWebRequest wr = WebRequest.Create(CommonMethodFacade.GetConfigValue("GenerateUrl")) as HttpWebRequest;
        wr.Timeout = 30 * 1000 * 60;//超时时间30分钟
        wr.ReadWriteTimeout = 30 * 1000 * 60;//超时时间30分钟
        WebResponse ws = wr.GetResponse();
        string response = string.Empty;
        using (System.IO.StreamReader reader = new System.IO.StreamReader(
                                               ws.GetResponseStream()
                                             , System.Text.Encoding.GetEncoding("GBK")))
        {
            response = reader.ReadToEnd();
            if (response.Equals("1") && CommonMethodFacade.GetConfigValue("SendSysTriggerEmail").Equals("true"))
            {
                SendMailFacade.sendEmail(CommonMethodFacade.GetConfigValue("NoticeEmail"), "网站半小时自动执行程序触发",
                " 执行成功，进行了更新明星换客和换品浏览次数，更新幻灯操作，留言自动审核，更新首页操作！");
            }
        }
    }

    #endregion
}
