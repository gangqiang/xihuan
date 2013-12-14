using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BusinessFacade;
public partial class ValidateCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string checkCode = CreateRandomCode(4);
        Session[CommonMethodFacade.VoucherCode_Name] =checkCode;
        CreateImage(checkCode);

    }
    private string CreateRandomCode(int codeCount)
    {
        string allChar = "1,2,3,4,5,6,7,8,9,A,B,c,D,E,F,G,h,I,J,K,L,M,N,p,Q,R,S,t,U,W,x,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(33);
            if (temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            while (temp == t)
            {
                t =rand.Next(35);
            } 

            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }

    private void CreateImage(string checkCode)
    {
        int iwidth = (int)(checkCode.Length *13);
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth,20);
        Graphics g = Graphics.FromImage(image);
        Font f = new System.Drawing.Font("Arial",12, System.Drawing.FontStyle.Bold);
        Brush b = new System.Drawing.SolidBrush(Color.LimeGreen);
        HatchBrush hb = new HatchBrush(HatchStyle.SmallConfetti,Color.DarkSeaGreen,Color.White);
        g.FillRectangle(hb, 0, 0, image.Width, image.Height);         
        g.DrawString(checkCode, f, b, 3, 3);
        Pen blackPen = new Pen(Color.Black, 0);
        Random rand = new Random();        

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ClearContent();
        Response.ContentType = "image/gif";
        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        image.Dispose();
    }
}
