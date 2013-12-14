using System.Text.RegularExpressions;
using System.IO;

namespace BusinessFacade
{
    public class ValidatorHelper
    {
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumeric(object o)
        {
            Regex r = new Regex(@"^\d+$");
            return r.IsMatch(o.ToString());
        }
        /// <summary>
        /// 是否浮点数
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsDecimal(object o)
        {
            Regex r = new Regex(@"^\d+$|^\d+(\.\d+)?$");
            return r.IsMatch(o.ToString());
        }
        /// <summary>
        /// 是否负数
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNegative(object o)
        {
            Regex r = new Regex(@"^[-]?(\d+\.?\d*|\.\d+)$");
            return r.IsMatch(o.ToString());
        }
		/// <summary>
		/// 验证是否为正整数
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsInt(string str)
		{

			return Regex.IsMatch(str, @"^[0-9]*$");
		}
        /// <summary>
        /// 是否有效手机号码
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsMobile(object o) 
        {
            Regex r = new Regex(@"^1[0-9]{10}$");
            return r.IsMatch(o.ToString());
        }

        /// <summary>
        /// 是否是有效的电子邮件
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsEmail(object o) 
        {
            Regex r = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return r.IsMatch(o.ToString());
        }

        /// <summary>
        /// 是否是数字或浮点数
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsIntAndFloat(string str) 
        {
            Regex r = new Regex(@"^\+?\d+(\.\d+)?$");
            return r.IsMatch(str);
        }
		/// <summary>
		/// 检测是否是正确的Url
		/// </summary>
		/// <param name="strUrl">要验证的Url</param>
		/// <returns>判断结果</returns>
		public static bool IsURL(string strUrl)
		{
			return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
		}
		/// <summary>
		/// 判断是否为base64字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsBase64String(string str)
		{
			//A-Z, a-z, 0-9, +, /, =
			return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
		}
		/// <summary>
		/// 检测是否有Sql危险字符
		/// </summary>
		/// <param name="str">要判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsSafeSqlString(string str)
		{

			return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
		}

		/// <summary>
		/// 判断字符串是否是yy-mm-dd字符串
		/// </summary>
		/// <param name="str">待判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsDateString(string str)
		{
			return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
		}

		/// <summary>
		/// 判断文件流是否为UTF8字符集
		/// </summary>
		/// <param name="sbInputStream">文件流</param>
		/// <returns>判断结果</returns>
		private static bool IsUTF8(FileStream sbInputStream)
		{
			int i;
			byte cOctets;  // octets to go in this UTF-8 encoded character 
			byte chr;
			bool bAllAscii = true;
			long iLen = sbInputStream.Length;

			cOctets = 0;
			for (i = 0; i < iLen; i++)
			{
				chr = (byte)sbInputStream.ReadByte();

				if ((chr & 0x80) != 0) bAllAscii = false;

				if (cOctets == 0)
				{
					if (chr >= 0x80)
					{
						do
						{
							chr <<= 1;
							cOctets++;
						}
						while ((chr & 0x80) != 0);

						cOctets--;
						if (cOctets == 0) return false;
					}
				}
				else
				{
					if ((chr & 0xC0) != 0x80)
					{
						return false;
					}
					cOctets--;
				}
			}

			if (cOctets > 0)
			{
				return false;
			}

			if (bAllAscii)
			{
				return false;
			}

			return true;

		}



        /// <summary>
        /// 替换SQL危险字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SafeSql(string s)
        {
            string str = string.Empty;
            if (s != null)
            {
                str=s.Replace("'", "''");
            }
            return str;
        }
    }
}
