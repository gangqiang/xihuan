using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using BusinessFacade;
/// <summary>
/// CommonMethod 的摘要说明
/// </summary>
public class CommonMethod
{
    public CommonMethod()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string MD5Encrypt(string str)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
    }

    public static void AddLoginCookie(int uid, string uname, DateTime cookieexpiredate)
    {
        HttpCookie newcookie = new HttpCookie("XIHuan8");
        newcookie.Values.Add("UserId", FinalString(uid));
        newcookie.Values.Add("UserName", HttpContext.Current.Server.UrlEncode(FinalString(uname)));
        if (cookieexpiredate != DateTime.MinValue)
            newcookie.Expires = cookieexpiredate;
        newcookie.HttpOnly = true;
        HttpContext.Current.Response.Cookies.Add(newcookie);
    }

    public static void AddAdminCookie(DateTime expiredate)
    {
        HttpCookie newcookie = new HttpCookie("XIHuan8_Admin");
        newcookie.Values.Add("UserId", "XIHuan8_Admin");
        newcookie.Values.Add("UserName", "lybwgq");
        newcookie.Expires = expiredate;
        newcookie.HttpOnly = true;
        HttpContext.Current.Response.Cookies.Add(newcookie);
    }

    public static bool IsValidatorCodeInputRight(string input)
    {
        bool result = FinalString(input).ToUpper().Equals(HttpContext.Current.Session[CommonMethodFacade.VoucherCode_Name].ToString().ToUpper());
        HttpContext.Current.Session[CommonMethodFacade.VoucherCode_Name] = "";
        return result;
    }

    public static bool IsUploadImageValid(string filter, string extention)
    {
        filter = FinalString(filter);
        if (filter.Length == 0)
            filter = ".jpg,.jpeg,.gif,.png";
        extention = FinalString(extention).ToLower();
        return filter.IndexOf(extention) > -1;
    }

    public static string readAspxAndWriteHtmlSoruce(string aspxPath, string savePath)
    {
        string path = Path.GetDirectoryName(savePath);
        HttpServerUtility su = HttpContext.Current.Server;
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
        }

        System.IO.StringWriter sw = new StringWriter();
        HttpContext.Current.Server.Execute(aspxPath, sw);
        StreamWriter sww = new StreamWriter(HttpContext.Current.Server.MapPath(savePath), false, System.Text.Encoding.GetEncoding("gb2312"));
        if (aspxPath.Equals("default.aspx"))
            sww.Write(sw.ToString());
        else
            sww.Write(Regex.Replace(Regex.Replace(sw.ToString(), "<form.*?action=.*?>", ""), "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\".*?/>", "").Replace("</form>", ""));
        sww.Flush();
        sww.Close();
        return savePath;
    }

    /// <summary>
    /// 转换为int类型
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">返回的默认值</param>
    /// <param name="numStyle">数字格式</param>
    /// <returns></returns>
    public static int ConvertToInt(object obj, int defaultValue, NumberStyles numStyle)
    {
        int result = defaultValue;
        if (obj != null && obj != DBNull.Value)
        {
            if (!int.TryParse(obj.ToString().Trim(), numStyle, null, out result))
            {
                result = defaultValue;
            }
        }
        return result;
    }

    public static bool ContainHtml(string str)
    {
        Regex re = new Regex("<.+?>");
        return re.IsMatch(str);

    }
    /// <summary>
    /// 转换为int类型
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">返回的默认值</param>
    /// <returns></returns>
    public static int ConvertToInt(object obj, int defaultValue)
    {
        return CommonMethod.ConvertToInt(obj, defaultValue, NumberStyles.Number);
    }

    /// <summary>
    /// 转换为decimal类型
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">返回的默认值</param>
    /// <returns></returns>
    public static decimal ConvertToDecimal(object obj, decimal defaultValue)
    {
        decimal result = defaultValue;
        if (obj != null && obj != DBNull.Value)
        {
            if (!decimal.TryParse(obj.ToString().Trim(), out result))
            {
                result = defaultValue;
            }
        }
        return result;
    }

    /// <summary>
    /// 转换为DateTime
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">返回的默认值</param>
    /// <returns></returns>
    public static DateTime ConvertToDateTime(object obj, DateTime defaultValue)
    {
        DateTime result = defaultValue;
        if (obj != null)
        {
            if (!DateTime.TryParse(obj.ToString().Trim(), out result))
            {
                result = defaultValue;
            }
        }
        return result;
    }

    /// <summary>
    /// 转换为HTML
    /// </summary>
    /// <param name="Coding"></param>
    /// <returns>返回br</returns>
    public static string CodingToHtml(string Coding)
    {
        string Html = string.Empty;
        Html = FinalString(Coding).Replace("\r\n", "<br/>");
        Html = Html.Replace("<br/><br/>", "<br/>");
        return Html;
    }


    #region 字符串操作
    /// <summary>
    /// 获取指定字符长度（判断1中文字符为2字符）
    /// </summary>
    /// <param name="stringToSub"></param>
    /// <param name="length"></param>
    /// <param name="endstring">如果截断则显示的字符</param>
    /// <returns></returns>
    public static string GetSubString(string stringToSub, int length, string endstring)
    {
        if (!string.IsNullOrEmpty(stringToSub))
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            Regex regexq = new Regex("[^\x00-\xff]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()) || regexq.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + endstring;
            else
                return sb.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 从左边截取字符串
    /// </summary>
    /// <param name="s">待截取的字符串</param>
    /// <param name="len">要截取的长度</param>
    /// <returns></returns>
    public static string Left(string s, int len)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        else
        {
            if (s.Length <= len)
                return s;
            else
                return s.Substring(0, len) + "…";
        }

    }

    /// <summary>
    /// 字符串简单处理
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <returns></returns>
    public static string FinalString(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        str = str.Trim();
        return str;
    }

    public static string FinalString(object str)
    {
        string strresult = string.Empty;
        if (str != null)
        {
            strresult = str.ToString();
            return FinalString(strresult);
        }
        else
        {
            return string.Empty;
        }
    }

    public static string FinalString(object str, object defaultvalue)
    {
        string strresult = string.Empty;
        if (str != null)
        {
            strresult = str.ToString();
            return FinalString(strresult);
        }
        else
        {
            return CommonMethod.FinalString(defaultvalue);
        }
    }

    /// <summary>
    /// 客户端输入字符串验证, Add by Wangxz at 2008-3-18
    /// </summary>
    /// <param name="text">客户端输入</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns>清理后的字符串</returns>
    public static string ClearInputText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        text = text.Trim();
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        if (text.Length > maxLength)
            text = text.Substring(0, maxLength);
        text = Regex.Replace(text, "[\\s]{2,}", " ");	//移除两个以上的空格
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//移除Br
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//移除&nbsp;
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//移除其他一些标志
        text = text.Replace("'", "''");//防止注入
        return text;
    }
    #endregion

    /// <summary>
    /// 输出Ajax内容
    /// </summary>
    /// <param name="Content"></param>
    public static void ResponseAjaxContent(Page page, string Content)
    {
        page.Response.Clear();
        page.Response.ContentType = "text/html";
        page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        page.Response.Write(Content);
        page.Response.End();
    }


    #region DropDownList

    /// <summary>
    /// 绑定枚举到下拉框
    /// </summary>
    /// <param name="lc"></param>
    /// <param name="enumtype"></param>
    /// <param name="defaultText"></param>
    /// <param name="defaultValue"></param>
    /// <param name="isNeedDefault"></param>
    public static void ListContolDataBindFromEnum(ListControl lc, System.Type enumtype, string defaultText, string defaultValue, bool isNeedDefault)
    {
        lc.Items.Clear();

        //默认值
        if (isNeedDefault)
        {
            lc.Items.Add(new ListItem(defaultText, defaultValue));
        }

        foreach (string enumName in Enum.GetNames(enumtype))
        {
            lc.Items.Add(new ListItem(enumName, Enum.Format(enumtype, Enum.Parse(enumtype, enumName), "d")));
        }
    }

    /// <summary>
    /// 绑定数据到ListControl
    /// </summary>
    /// <param name="lc"></param>
    /// <param name="dt"></param>
    /// <param name="strText"></param>
    /// <param name="strValue"></param>
    public static void BindDrop(ListControl lc, DataTable dt, string strText, string strValue)
    {
        lc.Items.Clear();
        if (dt == null || dt.Rows.Count == 0)
            return;
        lc.DataSource = dt;
        lc.DataTextField = strText.Trim();
        lc.DataValueField = strValue.Trim();
        lc.DataBind();
    }


    /// <summary>
    ///根据指定值选定控件中中的项
    /// </summary>
    /// <param name="rbl">控件名</param>
    /// <param name="text">指定值</param>
    public static bool SelectFlg(ListControl rbl, string flg)
    {
        bool isSelect = false;
        int FlgCount = rbl.Items.Count;
        if (flg == null)
        {
            flg = "";
        }
        if (flg != null)
        {
            for (int i = 0; i <= FlgCount - 1; i++)
            {
                if (rbl.Items[i].Value.Trim() == flg.Trim())
                {
                    isSelect = true;
                    rbl.SelectedIndex = i;
                    break;
                }
            }
        }
        return isSelect;
    }

    #endregion

    #region 获取客户端IP
    public static string GetClientIP(System.Web.UI.Page page)
    {
        string ipAddress = "";
        if (page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
        {
            ipAddress = page.Request.ServerVariables["Remote_Addr"];
        }
        else
        {
            string[] ips = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',');
            if (ips.Length > 0)
            {
                ipAddress = ips[0];
            }
            else
            {
                ipAddress = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

        }
        return ipAddress;
    }
    #endregion

    #region 获取QQ在线状态
    /// <summary>
    /// 获取QQ是否在线
    /// </summary>
    /// <param name="qq">QQ号码</param>
    /// <returns></returns>
    public static bool GetQQOnline(string qq)
    {
        string isOnline = string.Empty;
        string turl = "http://webpresence.qq.com/getonline?Type=1&{0}:";
        try
        {
            WebRequest wReq = WebRequest.Create(string.Format(turl, qq));
            WebResponse wResp = wReq.GetResponse();
            Stream rStream = wResp.GetResponseStream();
            StreamReader reader = new StreamReader(rStream, System.Text.Encoding.GetEncoding("utf-8"));
            isOnline = reader.ReadToEnd();
            if (isOnline == "online[0]=1;")
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region 金额小数转换为中文大写
    /// <summary>
    /// 把金额小数转换为中文大写（注：支持到万亿，也就是13位！）
    /// </summary>
    /// <param name="amount">所要处理的价格int和decimal都可以（注：支持到万亿，也就是13位！）
    /// </param>
    /// <returns>返回中文大写价格</returns>
    public static string FormatAmount(string amount)
    {
        int flag;//标示：0整数、1小数
        int dot = 0;
        string str = "";
        char[] TOBIG = { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' };
        char[] TOUNIT = { '元', '拾', '佰', '仟', '万', '拾', '佰', '仟', '亿', '拾', '佰', '仟', '万' };
        char[] TOUNITDE = { '角', '分', '厘', '毫', '丝' };


        //负数
        if (amount[0] == '-')
        {
            str += "负";
        }

        //除去数字前不规则的字符
        amount = amount.TrimStart('-', '0');

        //是否存在小数点
        if (amount.Contains("."))
        {
            //除去小数点后的0
            amount = amount.TrimEnd('0');
            if (amount.Length - 1 == amount.IndexOf('.'))//.00的情况
            {
                flag = 0;
                amount = amount.TrimEnd('.');
                if (amount.Length == 0)
                    amount = amount.Insert(0, "0");

            }
            else if (amount[0] == '.')//.X的情况
            {
                flag = 1;
                amount.Insert(0, "0");
                dot = amount.IndexOf('.');
            }
            else
            {
                flag = 1;
                dot = amount.IndexOf('.');
            }
        }
        else
        {
            flag = 0;
            if (amount.Length == 0)
                amount = amount.Insert(0, "0");
        }

        //分割整数和小数的部分
        string amount1 = "";
        string amount2 = "";

        if (flag == 0)
            amount1 = amount;
        else
        {
            amount1 = amount.Substring(0, dot);
            amount2 = amount.Substring(++dot, amount.Length - dot);
        }

        //转换整数
        int n = 0;
        for (int i = 0; i < amount1.Length; i++)
        {
            if (amount1[0] == '0')
            {
                str += TOBIG[0];
                break;
            }

            int j = amount1.Length - 1 - i;
            n = i;
            if (amount1[i] == '0')
            {
                for (int k = i; k < amount1.Length - 1; k++)
                {
                    if (amount1[i + 1] == '0')
                    {
                        i++;
                        if ((amount1.Length - i - 1) % 4 == 0)
                            if ((amount1.Length - i - 1) == 4 && amount1.Substring(n, 4) != "0000")
                                str += TOUNIT[4].ToString();
                            else if ((amount1.Length - i - 1) == 8 && amount1.Substring(n, 4) != "0000")
                                str += TOUNIT[8].ToString();
                            else
                                break;
                        //str += (amount1.Length - i - 1) == 4 ? TOUNIT[4].ToString() : TOUNIT[8].ToString();
                    }
                    else
                        break;
                }
                if (i != n || (amount1.Length - n - 1) % 4 == 0)
                {
                    if (j >= 8 && (amount1.Length - n - 1) % 4 == 0)
                    {
                        str += TOUNIT[8].ToString();
                        if (j == 8)
                            str += TOBIG[0].ToString();
                    }
                    else if (j >= 4 && (amount1.Length - n - 1) % 4 == 0)
                    {
                        str += TOUNIT[4].ToString();
                        if (j == 4)
                            str += TOBIG[0].ToString();
                    }
                    else
                        if (i == amount1.Length - 1 || (amount1.Length - n - 1) % 4 == 0 || i - n == 3)
                            str += "";
                        else if (j == 0 || j == 4 || j == 8)
                            str += "";
                        else
                            str += TOBIG[0].ToString();
                }
                else
                    str += TOBIG[0].ToString();
            }
            else
            {
                str += TOBIG[int.Parse(amount1[i].ToString())].ToString();
                str += j == 0 ? "" : TOUNIT[j].ToString();
            }
        }

        if (amount1.Length != 0)
            str += TOUNIT[0].ToString();

        if (amount2.Length == 0)
            str += "整";

        //转化小数位
        if (dot != amount.Length)
        {
            int flag1 = 0;
            for (int i = 0; i < amount2.Length; i++)
            {
                if (amount2[i] == '0')
                {
                    for (int k = i; k < amount2.Length - 1; k++)
                        if (amount2[i + 1] == '0')
                            i++;
                        else
                            break;
                    if (amount1.Length == 0 && flag1 == 0)
                        str += TOBIG[0].ToString() + TOUNIT[0].ToString();
                    else if (amount1.Length == 0 || i == amount2.Length - 1)
                        str += "";
                    else
                        str += TOBIG[0].ToString();
                }
                else
                {
                    flag1 = 1;
                    str += TOBIG[int.Parse(amount2[i].ToString())].ToString();
                    str += i > 4 ? "" : TOUNITDE[i].ToString();
                }
            }
        }

        return str;
    }
    #endregion

    #region 输出星期的通用方法

    public static string FormatWeek(DateTime nDate)
    {
        if (nDate.DayOfWeek == DayOfWeek.Saturday || nDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return "<span class='highlight'>" + nDate.ToString("dddd") + "</span>";
        }
        else
        {
            return nDate.ToString("dddd");
        }
    }
    #endregion

    #region 屏蔽html元素的方法
    /// <summary>
    /// 屏蔽html元素的方法
    /// </summary>
    /// <param name="strHtml">表示要屏蔽html的字符串</param>
    /// <returns>strHtml</returns>
    public static string StripHTML(string strHtml)
    {
        //删除script
        strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除style
        strHtml = Regex.Replace(strHtml, @"<style type='text/css'[^>]*?>[^\$]*?</style>", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"<style[^>]*?>[^\$]*?</style>", "", RegexOptions.IgnoreCase);
        //删除HTML   
        strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);

        strHtml = Regex.Replace(strHtml, @"&ldquo;", "“", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&rdquo;", "”", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        strHtml = Regex.Replace(strHtml, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        strHtml.Replace("<", "");
        strHtml.Replace(">", "");
        strHtml.Replace("\r\n", "");
        strHtml = HttpContext.Current.Server.HtmlEncode(strHtml).Trim();
        return strHtml;

    }
    #endregion

    #region 获取汉字拼音的首字母
    public static string GetFirstPYString(string str)
    {
        string resultstr = string.Empty;
        if (str != null && str.Trim().Length >= 1)
        {
            resultstr = GetPYString(str);
            if (resultstr.Trim().Length >= 1)
            {
                resultstr = resultstr.Substring(0, 1);
            }
        }
        return resultstr;
    }
    #endregion

    #region 汉字转拼音缩写
    ///   <summary>   
    ///   汉字转拼音缩写
    ///   </summary>   
    ///   <param   name="str">要转换的汉字字符串</param>   
    ///   <returns>拼音缩写</returns>   
    public static string GetPYString(string str)
    {
        string tempStr = "";
        foreach (char c in str)
        {
            if ((int)c >= 33 && (int)c <= 126)
            {//字母和符号原样保留   
                tempStr += c.ToString();
            }
            else
            {//累加拼音声母   
                tempStr += GetPYChar(c.ToString());
            }
        }
        return tempStr;
    }

    ///   <summary>   
    ///   取单个字符的拼音声母
    ///   </summary>   
    ///   <param   name="c">要转换的单个汉字</param>   
    ///   <returns>拼音声母</returns>   
    public static string GetPYChar(string c)
    {
        byte[] array = new byte[2];
        array = System.Text.Encoding.Default.GetBytes(c);
        if (array.Length >= 2)
        {
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
        }
        return "*";
    }

    #endregion

}
