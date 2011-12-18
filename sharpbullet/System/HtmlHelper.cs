using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class HtmlHelper
    {
        public static string WriteImg(string src, string style)
        {
            return "<img style=\"" + style + "\" src=\"" + src + "\" /\">";
        }
        public static string WriteTag(string tag, string innertext)
        {
            return "<" + tag + ">" + innertext + "</" + tag + ">";
        }

        public static string GetInnerTextOfTag(string html, string tag)
        {
            int i, j, k;

            i = html.IndexOf("<" + tag);
            if (i > -1)
            {
                j = html.IndexOf("</" + tag + ">", i);
                k = html.IndexOf(">", i);
                if (j > i && k < j)
                {
                    string s = html.Substring(k + 1, j - k-1);
                    return s;
                }
            }

            return "";
        }

        public static string GetInnerTextOfTag(string html, string tag, string id)
        {
            int i, j, k;

            i = html.IndexOf("<" + tag + " " + "id=\""+id+"\"");
            if (i > -1)
            {
                j = html.IndexOf("</" + tag + ">", i);
                k = html.IndexOf(">", i);
                if (j > i && k < j)
                {
                    string s = html.Substring(k + 1, j - k - 1);
                    return s;
                }
            }

            return "";
        }

        public static string WriteCheckBox(string name, string value)
        {
            return "<input type=\"checkbox\" name=\"" + name + "\" value=\"" + value + "\" />";
        }
    }
}
