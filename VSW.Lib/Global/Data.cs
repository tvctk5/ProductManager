using System;
using System.Text.RegularExpressions;

namespace VSW.Lib.Global
{
    public class Data
    {
        public static string RemoveAllCRLF(string strinput)
        {
            return Regex.Replace(strinput, "\r|\t|\n", string.Empty);
        }

        public static string RemoveAllTag(string strinput)
        {
            return Regex.Replace(strinput, "<.*?>", string.Empty);
        }

        public static string GetTooltip(string s, int length)
        {
            try
            {
                s = RemoveContent(s, "[if", "[endif]");
                s = RemoveContent(s, "<!--", "-->");

                s = RemoveAllTag(s);
                s = RemoveAllCRLF(s);

                s = CutString(s, length);

                s = s.Replace("'", "\\'");
                s = s.Replace("\"", "");

                return s;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string CutString(string s, int length)
        {
            if (s.Length > length)
            {
                if (length > 0)
                    s = s.Substring(0, length);

                int index = s.LastIndexOf(" ");

                if (index > 0)
                    s = s.Substring(0, index);

                return s + " ...";
            }

            return s;
        }

        public static string Base64Encode(string s)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
        }

        public static string Base64Decode(string s)
        {
            s = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(s));
            return s.Replace("\0", string.Empty);
        }

        public static string GetContent(string sHtml, string sBegin, string sEnd)
        {
            int i = sHtml.IndexOf(sBegin);
            if (i > -1)
            {
                int j = sHtml.IndexOf(sEnd, i + sBegin.Length);
                if (j > -1)
                {
                    return sHtml.Substring(i, j - i + sEnd.Length);
                }
            }

            return string.Empty;
        }

        public static string RemoveContentOne(string sHtml, string sBegin, string sEnd)
        {
            while (true)
            {
                int i = sHtml.IndexOf(sBegin);
                if (i > -1)
                {
                    int j = sHtml.IndexOf(sEnd, i + sBegin.Length);
                    if (j > -1)
                    {
                        string sTemp = sHtml.Substring(i, j - i + sEnd.Length);

                        sHtml = sHtml.Replace(sTemp, string.Empty);
                    }

                    break;
                }

                break;
            }

            return sHtml;
        }

        public static string RemoveContent(string sHtml, string sBegin, string sEnd)
        {
            return RemoveContent(sHtml, sBegin, sEnd, string.Empty);
        }

        public static string RemoveContent(string sHtml, string sBegin, string sEnd, string sNotIn)
        {
            while (true)
            {
                int i = sHtml.IndexOf(sBegin);
                if (i > -1)
                {
                    int j = sHtml.IndexOf(sEnd, i + sBegin.Length);
                    if (j > -1)
                    {
                        string sTemp = sHtml.Substring(i, j - i + sEnd.Length);

                        if (sNotIn != string.Empty && sTemp.IndexOf(sNotIn) > -1)
                        {
                            return sHtml;

                        }

                        sHtml = sHtml.Replace(sTemp, string.Empty);
                        return RemoveContent(sHtml, sBegin, sEnd, sNotIn);
                    }

                    break;
                }

                break;
            }

            return sHtml;
        }

        public static string EscapeQuote(string s)
        {
            return EscapeQuoteButNotTrim(s).Trim();
        }

        public static string EscapeQuoteButNotTrim(string s)
        {
            return string.Format("'{0}'", EscapeQuoteReg(s));
        }

        public static string Enquote(string s)
        {
            return s.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("\b", "\\b").Replace("\f", "\\f");
        }

        public static string EscapeQuoteReg(string s)
        {
            return Regex.Replace(Regex.Replace(s, "'", "''"), "(?:delete|select|drop|create|--)", string.Empty);
        }

        public static string FormatRemoveSQL(string s)
        {
            if (s != null)
            {
                string[] BadCommands = ";,--,create,drop,select,insert,delete,update,union,sp_,xp_".Split(new Char[] { ',' });

                int intCommand;
                for (intCommand = 0; intCommand <= BadCommands.Length - 1; intCommand++)
                {
                    s = Regex.Replace(s, BadCommands[intCommand], " ", RegexOptions.IgnoreCase);
                }

                s = s.Replace("'", "''").Replace("[", string.Empty).Replace("]", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
            }

            return s;
        }

        public static string RemoveVietNamese(string s)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";

            int index = -1;
            while ((index = s.IndexOfAny(FindText.ToCharArray())) != -1)
            {
                int index2 = FindText.IndexOf(s[index]);
                s = s.Replace(s[index], ReplText[index2]);
            }
            return s;
        }

        private static string RemoveNotABCChar(string s)
        {
            string _s = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if ((int)c == 160)
                    c = ' ';

                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c))
                    _s += c.ToString();
            }
            return _s;
        }

        public static string GetCode(string s)
        {
            s = RemoveNotABCChar(RemoveVietNamese(s));

            return s.Trim().Replace(" ", "-")
                .Replace("'", "")
                .Replace("/", "-")
                .Replace("*", "-")
                .Replace("\\", "-")
                .Replace("--", "-")
                .Replace("--", "-");
        }
    }
}
