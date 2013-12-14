using System;
using BusinessEntity;
using BusinessFacade;
public partial class sendnotes : BaseWebPage
{

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membernotes.aspx?type=" + Request["type"]);
        }
        else
        {
            if (!IsPostBack)
            {
                txtSender.Text = CurrentUserName;
            }
        }

    }
    #endregion

    #region 发送按钮
    protected void btnSend_Click(object sender, EventArgs e)
    {
        #region 服务器端验证

        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membernotes.aspx?type=" + Request["type"]);
        }
        else
        {
            if (txtReceiver.Text.Trim().Length == 0)
            {
                Alert("请填写接收人！");
                Select(txtReceiver);
                return;
            }
            if (txtContent.Text.Trim().Length == 0)
            {
                Alert("请填写留言内容！");
                Select(txtContent);
                return;
            }
            if (txtContent.Text.Trim().Length > 200)
            {
                Alert("留言内容不能超过200字！");
                Select(txtContent);
                return;
            }
            if (!XiHuan_UserFacade.IsUserNameAlreayUse(txtReceiver.Text))
            {
                Alert("收件人不存在，请查证后再发！");
                Select(txtReceiver);
                return;
            }
        }
        #endregion

        #region 发送留言
        XiHuan_GuestBookEntity newguestbook = new XiHuan_GuestBookEntity();
        newguestbook.IsScerect = chkSecret.Checked ? (byte)1 : (byte)0;
        newguestbook.ToId = XiHuan_UserFacade.GetIdByName(txtReceiver.Text.Trim());
        newguestbook.ToName = txtReceiver.Text.Trim();
        newguestbook.Content = txtContent.Text.Trim();
        newguestbook.CreateDate = DateTime.Now;
        newguestbook.Flag = (byte)XiHuan_UserNotesFacade.NotesState.未读;
        newguestbook.FromId = CurrentUserId;
        newguestbook.FromName = CurrentUserName;
        newguestbook.IsChecked = 0;
        newguestbook.Save();
        Alert("恭喜：留言发送成功！");
        ExecScript(string.Format("parent.location='membernotes.aspx?type={0}&s='+Math.random();", Request["type"]));
        #endregion
    }
    #endregion
}
