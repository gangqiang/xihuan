using System;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class notesreply : BaseWebPage
{
    #region 页面初始

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membernotes.aspx?type=" + Request["type"]);
        }
        else if (!IsPostBack)
        {
            if (CommonMethod.ConvertToInt(Request["id"], 0) > 0)
            {
                XiHuan_GuestBookEntity note = new XiHuan_GuestBookEntity();
                note.Id = CommonMethod.ConvertToInt(Request["id"], 0);
                note.Retrieve();
                if (note.IsPersistent)
                    txtContent.Text = CommonMethod.FinalString(note.ReplyContent);
            }
        }
    }

    #endregion

    #region 保存回复

    protected void btnSend_Click(object sender, EventArgs e)
    {
        #region 验证

        if (txtContent.Text.Trim().Length == 0)
        {
            Alert("请输入回复内容！");
            return;
        }

        #endregion

        XiHuan_GuestBookEntity note = new XiHuan_GuestBookEntity();
        note.Id = CommonMethod.ConvertToInt(Request["id"], 0);
        if (note.Id > 0)
        {
            note.Retrieve();
        }
        if (note.IsPersistent)
        {
            note.Flag = (byte)XiHuan_UserNotesFacade.NotesState.已回复;
            note.ReplyContent = CommonMethod.ClearInputText(txtContent.Text, 200);
            note.IsChecked = 0;
            note.Save();
            if (note.GoodsId > 0)
            {
                RetrieveCriteria rcgoods = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
                rcgoods.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
                Condition cgoods = rcgoods.GetNewCondition();
                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ID, note.GoodsId);
                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
                XiHuan_UserGoodsEntity goods = rcgoods.AsEntity() as XiHuan_UserGoodsEntity;
                if (goods != null)
                {
                    CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + note.GoodsId, goods.DetailUrl);
                }
            }
            Alert("恭喜：留言回复成功！");
            ExecScript(string.Format("parent.location='membernotes.aspx?type={0}';", Request["type"]));
        }
    }

    #endregion
}
