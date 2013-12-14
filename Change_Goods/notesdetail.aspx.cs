using System;
using BusinessFacade;
using BusinessEntity;
public partial class notesdetail : BaseWebPage
{
    private XiHuan_GuestBookEntity note = null;

    protected XiHuan_GuestBookEntity Note
    {
        get
        {
            if (note == null)
                note = XiHuan_GuestBookEntityAction.RetrieveAXiHuan_GuestBookEntity(Convert.ToInt32(Request["id"]));
            return note;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermessage.aspx?type=" + Request["type"]);
        }
        else
        {
            if (Note.FromId != CurrentUser.ID && Note.ToId != CurrentUserId)
            {
                Alert("您无权查看此留言！");
                Response.End();
            }
            else
            {
                if (Note.Flag == (byte)XiHuan_UserNotesFacade.NotesState.未读 && Request["type"] == "receive")
                {
                    Note.Flag = (byte)XiHuan_UserNotesFacade.NotesState.已读;
                    Note.Save();
                }

            }
        }
    }
}
