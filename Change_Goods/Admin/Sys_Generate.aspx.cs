//======================================================================
// Copyright (c) 苏州同程旅游网络科技有限公司. All rights reserved.
// 所属项目：Change_Goods
// 创 建 人：wgq
// 创建日期：2010-12-7 10:09:02
// 用    途： 网站自动执行程序的调用页面
//====================================================================== 

using System;
using System.Data;
using PersistenceLayer;
using BusinessEntity;
using BusinessFacade;
using System.Text;


/// <summary>
/// 在此描述Admin_Sys_Generate的说明
/// </summary>
public partial class Admin_Sys_Generate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userpass = CommonMethod.FinalString(Request.QueryString["userpass"]);
        if (userpass == "lybwgq8486")
        {
            UpdateUserAndViewCount();//更新明星换客和换品浏览次数
            UpdateRoationPic();//更新幻灯片 
            System.Threading.Thread.Sleep(1000 * 2);//中间停顿2秒钟执行
            CheckeNotes();//自动审核留言
            System.Threading.Thread.Sleep(1000 * 2);//中间停顿2秒钟执行
            UpdateTJGoods();
            CommonMethod.readAspxAndWriteHtmlSoruce("../default.aspx", "../index.html");//更新首页
            if (DateTime.Now.Hour == 15) //凌晨5点进行换品详细页面的更新和发邮件的操作
            {
                GenerateDetail();
                System.Threading.Thread.Sleep(1000 * 10);//中间停顿10秒钟执行
                SendMail();
            }

            Response.Clear();
            Response.Write("1");
            Response.End();
            BackUpDataBase();//备份数据库             

        }
    }

    #region 生成换品详细页

    private void GenerateDetail()
    {
        DateTime startdate = new DateTime(2009, 4, 7);
        DateTime enddate = DateTime.Now;
        DataTable dt = null;
        while (startdate <= enddate)
        {
            dt = Query.ProcessSql(string.Format(@"select Id,Name,CreateDate from XiHuan_UserGoods with(nolock) Where IsChecked=1 
 
                                                                                    AND CreateDate>='{0}' AND CreateDate<='{1}' ", startdate, startdate.AddMonths(3)),
                                                                                                                                 GlobalVar.DataBase_Name);

            foreach (DataRow dr in dt.Rows)
            {
                DateTime dt2 = CommonMethod.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                string path = "goods/" + dt2.Year + "/" + dt2.Month + "/" + dt2.Day + "/goods" + dr["Id"] + ".html";
                CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + dr["Id"].ToString(), "../" + path);
                Query.ProcessSqlNonQuery("update XiHuan_UserGoods set DetailUrl='" + path + "' where Id=" + dr["Id"], GlobalVar.DataBase_Name);
                System.Threading.Thread.Sleep(1000 * 3);//中间停顿3秒钟执行
            }

            startdate = startdate.AddMonths(3);
            System.Threading.Thread.Sleep(1000 * 3);//中间停顿3秒钟执行
        }
    }

    #endregion

    #region 发邮件

    public void SendMail()
    {
        string mailcontent = " <table style=\"width:98%;border: #ccc 1px solid;padding-right: 10px;padding-left: 10px;padding-bottom: 10px;padding-top: 10px;background-color: #f7f7f7; font-size:14px; color:black;\">";
        RetrieveCriteria rcGoodsPics = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__NAME);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
        Condition c = rcGoodsPics.GetNewCondition();
        c.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
        c.AddNotEqualTo(XiHuan_UserGoodsEntity.__DEFAULTPHOTO, "images/none.jpg");
        rcGoodsPics.OrderBy(XiHuan_UserGoodsEntity.__CREATEDATE, false);
        rcGoodsPics.Top = 70;
        EntityContainer ecGoods = rcGoodsPics.AsEntityContainer();
        for (int i = 0; i < ecGoods.Count; i++)
        {
            int rem;
            XiHuan_UserGoodsEntity goods = ecGoods[i] as XiHuan_UserGoodsEntity;
            Math.DivRem(i, 7, out rem);
            if (rem == 0)
            {
                mailcontent += "<tr>";
            }
            else
            {
                mailcontent += string.Format("<td style=\"border: solid 1px #CCCCCC;height: 30px;text-align:center;padding: 2px 5px;\"> <a title=\"{0}\" href=\"{1}\" target=\"_blank\"><img style=\"width: 85px;height: 85px;border: solid 1px #CCCCCC;\" title=\"{0}\" src=\"{2}\" /><br/><br/>{3}</a></td>", goods.Name, "http://www.tsc8.com/" + goods.DetailUrl + "?from=email", "http://www.tsc8.com/" + goods.DefaultPhoto, CommonMethod.GetSubString(goods.Name, 6, "..."));
            }

            if (rem == 0)
            {
                mailcontent += "</tr>";
            }

        }

        mailcontent += "</table>";
        DataTable dt = PersistenceLayer.Query.ProcessSql("select Id,UserName,Email,QQ,Msn from XiHuan_UserInfo with(nolock) where  LastLoginTime<='" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd") + "' AND LastLoginTime>='" + DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "' and (Email>'' or QQ >'' or Msn >'')", GlobalVar.DataBase_Name);
        string strMailAdd = string.Empty;
        string email = string.Empty;
        string qq = string.Empty;
        string msn = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {
            strMailAdd = string.Empty;
            email = CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__EMAIL]).ToLower();
            qq = GetFinalQQ(CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__QQ]));
            msn = CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__MSN]);
            if (IsEmail(email))
            {
                strMailAdd += email + ",";
            }
            if (IsInt(qq))
            {
                strMailAdd += qq + "@qq.com,";
            }
            if (IsEmail(msn))
            {
                strMailAdd += msn + ",";
            }
            if (strMailAdd.Length > 0)
            {
                SendMailFacade.sendEmail(strMailAdd.TrimEnd(','), "喜换网-物品交换，节约，时尚，好玩，精彩不容错过！", "<span style=\"color:blue;font-weight:bold;\">尊敬的换友<a href=\"http://www.tsc8.com/xh.aspx?id=" + dr[XiHuan_UserInfoEntity.__ID] + "&from=email\" target=\"_blank\" style=\"color:red;\">" + dr[XiHuan_UserInfoEntity.__USERNAME] + "</a>，我们注意到你有段时间没来<a href=\"http://www.tsc8.com/?from=eamil&id=" + dr[XiHuan_UserInfoEntity.__ID] + "\" target=\"_blank\" style=\"color:red;\">喜换网</a>逛逛了啊，<br/><br/>你不在的这段时间里好多朋友发布了很多好玩的换品：</span><br/><br/>" + mailcontent + "<br/><br/>现在快<a href=\"http://www.tsc8.com/?from=eamil\" target=\"_blank\">去看看</a>吧！");
                System.Threading.Thread.Sleep(1000 * 3);//中间停顿3秒钟执行
            }
        }
    }
    private bool IsEmail(object o)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        return r.IsMatch(o.ToString());
    }
    private bool IsInt(string str)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[0-9]*$");
    }
    private string GetFinalQQ(string qq)
    {
        return qq.Replace('０', '0').Replace('１', '1').Replace('２', '2').Replace('3', '3').Replace('４', '4').Replace('５', '5').Replace('６', '6').Replace('７', '7').Replace('８', '8').Replace('９', '9');
    }
    #endregion

    #region 幻灯片更新
    protected void UpdateRoationPic()
    {
        RetrieveCriteria rcGoodsPics = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__NAME);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
        Condition c = rcGoodsPics.GetNewCondition();
        c.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
        c.AddNotEqualTo(XiHuan_UserGoodsEntity.__DEFAULTPHOTO, "images/none.jpg");
        rcGoodsPics.OrderBy(XiHuan_UserGoodsEntity.__CREATEDATE, false);
        rcGoodsPics.Top = 6;
        EntityContainer ecGoods = rcGoodsPics.AsEntityContainer();
        string pics = "var pics=\"";
        string links = "var links=\"";
        string texts = "var texts=\"";
        foreach (XiHuan_UserGoodsEntity goods in ecGoods)
        {
            pics += goods.DefaultPhoto.Replace(GlobalVar.DefaultPhotoSize, GlobalVar.BigPhotoSize) + "|";
            links += goods.DetailUrl + "|";
            texts += ValidatorHelper.SafeSql(goods.Name) + "|";
        }
        pics = pics.TrimEnd('|') + "\";";
        links = links.TrimEnd('|') + "\";";
        texts = texts.TrimEnd('|') + "\";";
        Query.ProcessSqlNonQuery(string.Format("update xihuan_systemconfig set configvalue='{0}' where configkey='HomeRounPics'", pics + links + texts), GlobalVar.DataBase_Name);
        SystemConfigFacade.Refresh();
    }
    #endregion

    #region 更新明星换客和换品浏览次数

    private void UpdateUserAndViewCount()
    {
        Query.ProcessSqlNonQuery(@"UPDATE XiHuan_UserInfo SET IsStarUser=1 WHERE GoodsNumber>=10;
                                                        UPDATE XiHuan_UserGoods SET ViewCount=ViewCount+3; ", GlobalVar.DataBase_Name);
    }

    #endregion

    #region 审核留言

    private void CheckeNotes()
    {
        Transaction t = new Transaction();
        DataTable dt = Query.ProcessSql(@"SELECT Id,Content,ReplyContent FROM XiHuan_GuestBook WITH(nolock) WHERE IsChecked=0 ", GlobalVar.DataBase_Name);
        string content = string.Empty;
        string replycontent = string.Empty;
        int nid = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                nid = CommonMethod.ConvertToInt(dr["Id"], 0);
                content = CommonMethod.FinalString(dr["Content"]);
                replycontent = CommonMethod.FinalString(dr["ReplyContent"]);
                if (CommonMethodFacade.ValidFFZF(content).Length == 0 && CommonMethodFacade.ValidFFZF(replycontent).Length == 0)
                {
                    XiHuan_GuestBookEntity notesinfo = new XiHuan_GuestBookEntity();
                    notesinfo.Id = nid;
                    notesinfo.Retrieve();
                    if (notesinfo.IsPersistent)
                    {
                        notesinfo.IsChecked = 1;
                        t.AddSaveObject(notesinfo);
                        XiHuan_MessageEntity notechecknoticemessage = new XiHuan_MessageEntity();
                        notechecknoticemessage.FromId = 1;
                        notechecknoticemessage.FromName = "喜换网";
                        notechecknoticemessage.ToId = notesinfo.FromId;
                        notechecknoticemessage.ToName = notesinfo.FromName;
                        notechecknoticemessage.Content = string.Format("尊敬的喜换网会员<strong>{0}</strong>,您好：<br/>您给\"{1}\"的留言\"{2}\",已经通过审核，请注意查看!",
                            notesinfo.FromName, notesinfo.ToName, notesinfo.Content);
                        notechecknoticemessage.Flag = byte.Parse(XiHuan_MessageFacade.MessageState.未读.ToString("d"));
                        notechecknoticemessage.CreateDate = DateTime.Now;
                        t.AddSaveObject(notechecknoticemessage);
                        if (t.Process())
                        {
                            #region   如果是对换品页面的留言，重新生成换品页
                            if (CommonMethod.ConvertToInt(nid, 0) > 0)
                            {
                                RetrieveCriteria rcgoods = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
                                rcgoods.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
                                Condition cgoods = rcgoods.GetNewCondition();
                                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ID, notesinfo.GoodsId);
                                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
                                XiHuan_UserGoodsEntity goods = rcgoods.AsEntity() as XiHuan_UserGoodsEntity;
                                if (goods != null)
                                {
                                    CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + notesinfo.GoodsId, "../" + goods.DetailUrl);
                                }
                            }
                            #endregion
                        }
                    }

                }
            }
        }
    }

    #endregion

    #region 备份数据库

    private void BackUpDataBase()
    {
        Query.ProcessSqlNonQuery(@"BACKUP DATABASE [a0123110836] TO  DISK = N'D:\Program\mssql2005\MSSQL.1\MSSQL\Data\backup\a0123110836_45846078\3.bak' WITH NOFORMAT, NOINIT,  NAME = N'a0123110836-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", GlobalVar.DataBase_Name);
    }

    #endregion

    #region 更新换品推荐信息

    private void UpdateTJGoods()
    {
        Query.ProcessSqlNonQuery(string.Format(
            @"update xihuan_usergoods set istj=1
            where id in
            (
            select top 15 id from xihuan_usergoods 
            where defaultphoto!='images/none.jpg' and istj=0 
            and createdate>='{0}' order by newid()
            )", DateTime.Now.AddDays(-3).ToShortDateString()), GlobalVar.DataBase_Name);
    }

    #endregion

}
