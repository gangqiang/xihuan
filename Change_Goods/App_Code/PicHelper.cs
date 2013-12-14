using System.Drawing;

/// <summary>
/// PicHelper 的摘要说明
/// </summary>
public class PicHelper
{
    public PicHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 生成缩略图

    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
    {
        if (System.IO.File.Exists(originalImagePath))
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
            {
                oh = originalImage.Height;
                ow = originalImage.Height * towidth / toheight;
                y = 0;
                x = (originalImage.Width - ow) / 2;
            }
            else
            {
                ow = originalImage.Width;
                oh = originalImage.Width * height / towidth;
                x = 0;
                y = (originalImage.Height - oh) / 2;
            }


            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }

    #endregion


    /// <summary>
    /// 在图片上增加文字水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_sy">生成的带文字水印的图片路径</param>
    public static void AddShuiYinWord(string Path, string Path_sy)
    {
        string addText = "www.tsc8.com";
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(image, 0, 0, image.Width, image.Height);
        System.Drawing.Font f = new System.Drawing.Font("Verdana", 10);
        System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        g.DrawString(addText, f, b, 0, image.Height - 20);
        g.Dispose();
        image.Save(Path_sy);
        image.Dispose();
    }


    /// <summary>
    /// 在图片上生成图片水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_syp">生成的带图片水印的图片路径</param>
    /// <param name="Path_sypf">水印图片路径</param>
    public static void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
        g.Dispose();
        image.Save(Path_syp);
        image.Dispose();
    }

}
