using System;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class changestate : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(Request["id"], 0);
        if (id > 0)
        {
            XiHuan_UserGoodsChangeRequireEntity require = new XiHuan_UserGoodsChangeRequireEntity();
            require.Id = id;
            require.Retrieve();
            if (require.IsPersistent)
            {
                require.Flag = byte.Parse(rbtFlag.SelectedValue);
                require.Save();
                string ids =CommonMethod.FinalString(require.SelectToChangeGoodsId).Length > 0 ? require.SelectToChangeGoodsId + require.GoodsId.ToString() : require.GoodsId.ToString();
                Query.ProcessSqlNonQuery(string.Format("update XiHuan_UserGoods set GoodState="+rbtFlag.SelectedValue.Trim()+" where Id in ({0}) ",ids),GlobalVar.DataBase_Name);
                XiHuan_UserGoodsEntity goods = new XiHuan_UserGoodsEntity();
                goods.Id = require.GoodsId;
                goods.Retrieve();
                if (goods.IsPersistent)
                {
                    CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + goods.Id, goods.DetailUrl);
                }
                Alert("恭喜：状态更改成功！");
                ExecScript("parent.location='userequst.aspx?type=" + Request["type"] + "'");
            }
        }
    }
}
