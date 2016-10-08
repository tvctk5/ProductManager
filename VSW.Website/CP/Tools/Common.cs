using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSW.Website.CP.Tools
{
    public class Common
    {
        public int ConvertToInt32(object obj)
        {
            int retVal = 0;

            try
            {
                if (obj == null || obj == DBNull.Value || string.IsNullOrEmpty(ConvertToString(obj)))
                    return 0;

                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public Double ConvertToDouble(object obj)
        {
            Double retVal = 0;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public bool ConvertToBoolean(object obj)
        {
            bool retVal = false;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public DateTime ConvertToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
                ////return new DateTime();
                //return null;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

            return retVal;
        }

        public bool ConvertToDateTime(object obj, ref object objOut)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
                objOut = retVal;
                return true;
            }
            catch
            {
                retVal = new DateTime();
                return false;
            }
        }

        public static string ConvertToMoney(object objValue, int DecimalNumber = 0)
        {
            try
            {
                string sValue = objValue.ToString();
                sValue = sValue.Replace(" ", "");
                return String.Format("{0:C" + DecimalNumber.ToString() + "}", Convert.ToDouble(sValue)).Replace("$", "").Replace("£", "");
            }
            catch (Exception)
            { return ""; }
        }

        public string ConvertToString(object obj)
        {
            string retVal;

            try
            {
                if (obj == null)
                    return string.Empty;

                retVal = Convert.ToString(obj).Trim();
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }
    }
}