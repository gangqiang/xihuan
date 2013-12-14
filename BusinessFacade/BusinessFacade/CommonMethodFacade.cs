using System;
using System.Data;
using System.Globalization;
namespace BusinessFacade
{
    public class CommonMethodFacade
    {

        public const string VoucherCode_Name = "CheckCode";

        /// <summary>
        /// ת��Ϊint����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">���ص�Ĭ��ֵ</param>
        /// <param name="numStyle">���ָ�ʽ</param>
        /// <returns></returns>
        public static int ConvertToInt(object obj, int defaultValue)
        {
            int result = defaultValue;
            if (obj != null && obj != DBNull.Value)
            {
                if (!int.TryParse(obj.ToString().Trim(), NumberStyles.Number, null, out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        /// <summary>
        /// �ַ����򵥴���
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <returns></returns>
        public static string FinalString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            str = str.Trim();
            return str;
        }

        public static string FormatGender(byte gender, string rootpath)
        {
            if (gender == 1)
                return string.Format("<img title=\"˧��\" src=\"{0}images/man.gif\" />", rootpath);
            else
                return string.Format("<img title=\"��Ů\" src=\"{0}images/woman.gif\" />", rootpath);
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

        public static string GetProvinceNameById(string pid)
        {
            DataTable dt = ProvinceCityFacade.GetInstance().GetProvince();
            DataRow[] dr = dt.Select("provinceID=" + pid);
            if (dr.Length > 0)
                return CommonMethodFacade.FinalString(dr[0]["province"]);
            else
                return string.Empty;
        }

        public static string GetCityNameById(string cid)
        {
            string result = string.Empty;
            DataTable dt = ProvinceCityFacade.GetInstance().GetCityInfo("");
            DataRow[] dr = dt.Select("cityID=" + cid);
            if (dr.Length > 0)
                result = CommonMethodFacade.FinalString(dr[0]["city"]);
            else
            {
                dr = dt.Select("father=" + cid);
                if (dr.Length > 0)
                    result = CommonMethodFacade.FinalString(dr[0]["city"]);
            }

            return result;
        }

        public static string GetAreaNameById(string aid)
        {
            DataTable dt = ProvinceCityFacade.GetInstance().GetAreaInfo("");
            DataRow[] dr = dt.Select("areaId=" + aid);
            if (dr.Length > 0)
                return CommonMethodFacade.FinalString(dr[0]["area"]);
            else
                return string.Empty;
        }

        public static string GetSchoolNameById(string pid, string cid, string sid)
        {
            DataTable dt = ProvinceCityFacade.GetInstance().GetSchoolInfo(pid, cid);
            DataRow[] dr = dt.Select("Id=" + sid);
            if (dr.Length > 0)
                return CommonMethodFacade.FinalString(dr[0]["SchoolName"]);
            else
                return string.Empty;
        }

        #region �Ƿ��ַ��Ĺ���

        public static string ValidFFZF(string strContent)
        {
            if (FinalString(strContent).Length > 0)
            {
                string[] strFFZF = SystemConfigFacade.Instance().FFZF.Split(',');
                for (int i = 0; i < strFFZF.Length; i++)
                {
                    if (strContent.IndexOf(strFFZF[i].Trim()) >= 0)
                    {
                        return strFFZF[i];
                    }
                }
            }

            return string.Empty;

        }

        #endregion

        #region ��ȡConfig������Ϣ

        public static string GetConfigValue(string configkey)
        {
            return FinalString(System.Configuration.ConfigurationManager.AppSettings[configkey]);
        }

        #endregion
    }


}