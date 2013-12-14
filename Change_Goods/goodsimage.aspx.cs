using System;
using System.Data;
using BusinessEntity;
using PersistenceLayer;
using System.Text;
public partial class goodsimage : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = CommonMethod.ConvertToInt(Request["id"], 0);
            if (id > 0)
            {
                RetrieveCriteria rcgoodimage = new RetrieveCriteria(typeof(XiHuan_GoodsImageEntity));
                Condition c = rcgoodimage.GetNewCondition();
                c.AddEqualTo(XiHuan_GoodsImageEntity.__GOODSID, id);
                rcgoodimage.AddSelect(XiHuan_GoodsImageEntity.__IMGSRC);
                rcgoodimage.AddSelect(XiHuan_GoodsImageEntity.__IMGDESC);
                rcgoodimage.OrderBy(XiHuan_GoodsImageEntity.__CREATEDATE, false);
                DataTable dt = rcgoodimage.AsDataTable();
                StringBuilder sbImage = new StringBuilder();
                StringBuilder sbDesc = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbImage.AppendFormat("\"{0}\",", dt.Rows[i][XiHuan_GoodsImageEntity.__IMGSRC]);
                    sbDesc.AppendFormat("\"{0}\",", dt.Rows[i][XiHuan_GoodsImageEntity.__IMGDESC]);
                }

                ExecStartupScript(string.Format("imageArray=new Array({0});", sbImage.ToString().TrimEnd(',')));
                ExecStartupScript(string.Format("imageDesc=new Array({0});", sbDesc.ToString().TrimEnd(',')));
                ExecStartupScript("getnext();$('#loadingimage').hide(); $('#saint').show();");

            }
        }
    }
}
