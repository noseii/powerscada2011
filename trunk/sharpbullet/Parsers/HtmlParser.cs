using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace SharpBullet.Parsers
{
    //Sandbox içinde Codedom ile yapılan derlemeler için gerekli.
    public class SiteParser
    {
        public virtual string Parse(HtmlDocument document)
        {
            return "Error";
        }
    }

    public class HtmlParser
    {
        public List<string> Links = new List<string>();
        public List<string> Images = new List<string>();

        public HtmlParser()
        {
        }

        public string Parse(string content)
        {
            bool inTag = false;
            bool ignore = false;
            string tagBuffer = null;
            string textContent = null;

            if (string.IsNullOrEmpty(content)) return "";

            for (int i = 0; i < content.Length; i++)
            {
                switch (content[i])
                {
                    case '<':
                    case '>':
                        if (content[i] == '<')
                        {
                            if (inTag) tagBuffer = null; //mesela js içinde < karakteri kullanılabilir.
                            inTag = true;
                        }
                        if (content[i] == '>') inTag = false;

                        if (!inTag)
                        {
                            if (tagBuffer != null)
                            {
                                if (tagBuffer.StartsWith("style")
                                    || tagBuffer.StartsWith("script")) ignore = true;
                                if (tagBuffer.StartsWith("/style")
                                    || tagBuffer.StartsWith("/script")) ignore = false;
                            }

                            parseTag(tagBuffer);
                            tagBuffer = null;
                            textContent += " ";
                        }
                        break;
                    default:
                        if (inTag)
                            tagBuffer += content[i];
                        else
                            if (!ignore) textContent += content[i];
                        break;
                }
            }

            return textContent;
        }

        private void parseTag(string tagBuffer)
        {
            try
            {
                if (string.IsNullOrEmpty(tagBuffer)) return;

                if (tagBuffer.StartsWith("a ")
                    && tagBuffer.Contains(" href=\""))
                {
                    int i = tagBuffer.IndexOf(" href=\"");
                    string s = tagBuffer.Substring(i + " href=\"".Length);
                    i = s.IndexOf("\"");
                    if (i > 0)
                    {
                        s = s.Substring(0, i).Trim();
                        if (s == ""
                            || s == "#"
                            || s == "/") return;

                        if (s.StartsWith("javascript:")) return;

                        if (!WebHelper.IsWebPage(s)) return;

                        Links.Add(s);
                    }
                }
                else if (tagBuffer.StartsWith("img ")
                    && tagBuffer.Contains(" src=\""))
                {
                    int i = tagBuffer.IndexOf(" src=\"");
                    string s = tagBuffer.Substring(i + " src=\"".Length);
                    i = s.IndexOf("\"");
                    if (i > 0)
                    {
                        Images.Add(s.Substring(0, i));
                    }
                }
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show("Test");
            }
        }
    }

    public class HtmlElement
    {
        public HtmlElement Parent { get; set; }
        public string Tag { get; set; }
        public string InnerText { get; set; }

        private List<HtmlElement> childs;
        public List<HtmlElement> Childs
        {
            get { if (childs == null)childs = new List<HtmlElement>(); return childs; }
            set { childs = value; }
        }

        //Optimizasyon için: bu metodla okuma yapılınca gerek yoksa create edilmiyor childs
        public int ChildCount
        {
            get
            {
                if (childs == null) return 0;
                return childs.Count;
            }
        }

        public string GetTagName()
        {
            if (string.IsNullOrEmpty(Tag)) return null;

            string temp = Tag;
            if (temp.EndsWith("/")) temp = temp.Substring(0, temp.Length - 1);

            int i = temp.IndexOf(' ');
            if (i < 0) return temp.ToLowerInvariant();

            return temp.Substring(0, i).ToLowerInvariant();
        }

        public string GetAttribute(string name)
        {
            if (string.IsNullOrEmpty(Tag)) return null;

            int i = Tag.IndexOf(" " + name);
            if (i < 0) return null;

            string temp = Tag.Substring(i + (" " + name).Length).Trim();
            if (temp.StartsWith("=\""))
            {
                temp = temp.Substring(2);
                i = temp.IndexOf('"');
                if (i < 0) return temp;
                else if (i == 0) return "";
                return temp.Substring(0, i);
            }
            else if (temp.StartsWith("=\""))
            {
                temp = temp.Substring(2);
                i = temp.IndexOf('\'');
                if (i < 1) return temp;
                return temp.Substring(0, i);
            }
            else if (temp.StartsWith("="))
            {
                temp = temp.Substring(1);
                i = temp.IndexOf(' ');
                if (i < 1) return temp;
                return temp.Substring(0, i);
            }

            return null;
        }

        public string GetAllText(bool keepTextTags, bool trim)
        {
            string result = null;

            if (GetTagName() == "script"
               || GetTagName() == "style")
            {
                return result;
            }

            if (keepTextTags)
            {
                if (GetTagName() == "br") result += "<br/>";
                if (GetTagName() == "p") result += "<p>";
                if (GetTagName() == "blockquote") result += "<blockquote>";
                if (GetTagName() == "b") result += "<b>";
                if (GetTagName() == "strong") result += "<strong>";
            }

                if (!trim)
                {
                    result += InnerText;
                }
                else
                {
                    if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(InnerText))
                        result += " " + InnerText.Trim();
                    else if (!string.IsNullOrEmpty(InnerText))
                        result += InnerText.Trim();
                }


                if (ChildCount > 0)
                {
                    foreach (HtmlElement element in Childs)
                    {
                        string childText = element.GetAllText(keepTextTags, trim);
                        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(childText))
                            result += " " + childText.Trim();
                        else if (!string.IsNullOrEmpty(childText))
                            result += childText.Trim();
                    }
                }

            if (keepTextTags)
            {
                if (GetTagName() == "p") result += "</p>";
                if (GetTagName() == "blockquote") result += "</blockquote>";
                if (GetTagName() == "b") result += "</b>";
                if (GetTagName() == "strong") result += "</strong>";
            }

            return result;
        }

        public override string ToString()
        {
            return (Tag ?? "*txt*").Substring(0, 5);
        }
    }

    public class HtmlDocument
    {
        public string OriginalText { get; set; }

        public HtmlElement Root = new HtmlElement();

        private Dictionary<string, bool> inlineTags = new Dictionary<string, bool>() { 
            {"font",true} , {"p",true}, {"br",true}, {"span",true}, {"strong", true},
            {"b",true} ,{"u",true} ,{"i",true} ,{"a",true} ,
        };

        string html;
        int index;
        int state;
        HtmlElement currentTag;

        char getChar()
        {
            int i = index;
            index++;

            return html[i];
        }

        //public void Parse2(string html)
        //{
        //    this.html = html;
        //    state = 0;
        //    index = 0;

        //    while (index < html.Length)
        //    {
        //        char c = getChar();

        //        switch (state)
        //        {
        //            case 0:
        //                while (getChar() != '<') { }
        //                currentTag = new HtmlElement();
        //                Root.Childs.Add(currentTag);
        //                state = 1;
        //                break;
        //            case 1: //tag start
        //                if (c == ' ')
        //                    state= 2;
        //                else if (c == '/')
        //                    state = 3;
        //                else if (c == '>')
        //                    state = 3;
        //                else
        //                    currentTag.Tag += c;
        //                break;

        //            case 2: //attributes
        //            default:
        //                break;
        //        }
        //    }
        //}

        public void Parse(string html)
        {
            OriginalText = html;

            if (string.IsNullOrEmpty(html)) return;

            HtmlElement currentElement = Root;
            bool inTag = false;
            string buffer = null;

            for (int i = 0; i < html.Length; i++)
            {
                switch (html[i])
                {
                    case '<':
                        //script içinde direkt > karakteri kullanılmasını destekliyoruz.
                        if ((i + 1) < html.Length && html[i + 1] != '/' && currentElement.GetTagName() == "script")
                        {
                            buffer += html[i];
                            continue;
                        }
                        if (!inTag)
                        {
                            if (!string.IsNullOrEmpty(buffer))
                            {
                                HtmlElement element = new HtmlElement() { InnerText = buffer };
                                element.Parent = currentElement;
                                currentElement.Childs.Add(element);
                            }
                            inTag = true;
                            buffer = null;
                        }
                        else
                            buffer += html[i];
                        break;
                    case '>':
                        if (!inTag) //html içinde direkt > karakteri kullanılmasını destekliyoruz. Hatalı kullanım olmasına rağmen browserlarda çalışıyor.
                        {
                            buffer += html[i];
                            continue;
                        }
                        HtmlElement element2 = new HtmlElement() { Tag = buffer };

                        if (element2.Tag.StartsWith("/"))
                        {
                            string temp = element2.Tag.Substring(1).ToLowerInvariant();
                            if (currentElement.GetTagName() == temp)
                            {
                                if (currentElement.Parent != null)
                                    currentElement = currentElement.Parent;
                            }
                            else if (currentElement.Parent != null
                                && currentElement.Parent.GetTagName() == temp)
                            {
                                if (currentElement.Parent != null)
                                    currentElement = currentElement.Parent.Parent;
                            }
                            else
                            {
                                while (currentElement.Parent != null && inlineTags.ContainsKey(currentElement.GetTagName()))
                                {
                                    currentElement = currentElement.Parent;
                                }
                            }
                        }
                        else if (element2.GetTagName() != "br"
                            && element2.GetTagName() != "meta"
                            && element2.GetTagName() != "img"
                            && element2.GetTagName() != "hr"
                            && element2.GetTagName() != "link"
                            && !element2.GetTagName().StartsWith("!--")
                            && element2.GetTagName() != "!doctype"
                            && !element2.GetTagName().EndsWith("/"))
                        {
                            element2.Parent = currentElement;
                            currentElement.Childs.Add(element2);
                            currentElement = element2;
                        }
                        else
                        {
                            element2.Parent = currentElement;
                            currentElement.Childs.Add(element2);
                        }
                        inTag = false;
                        buffer = null;
                        break;
                    default:
                        buffer += html[i];
                        break;
                }
            }
            if (!string.IsNullOrEmpty(buffer))
            {
                HtmlElement lastElement = new HtmlElement();
                lastElement.InnerText = buffer;
                Root.Childs.Add(lastElement);
            }
        }

        public void ParseOld(string html)
        {
            if (string.IsNullOrEmpty(html)) return;

            HtmlElement currentElement = Root;
            bool inTag = false;
            string buffer = null;

            for (int i = 0; i < html.Length; i++)
            {
                switch (currentElement.GetTagName())
                {
                    case "script":
                    case "style":
                        switch (html[i])
                        {
                            case '<':
                                if (html.Substring(i + 1, 7) == "/script" || html.Substring(i + 1, 6) == "/style")
                                {
                                    if (!string.IsNullOrEmpty(buffer))
                                        currentElement.InnerText += buffer;
                                    buffer = null;
                                }
                                else
                                    buffer += html[i];
                                break;
                            case '>':
                                if (buffer.StartsWith("/script") || buffer.StartsWith("/style"))
                                {
                                    if (buffer.StartsWith("/"))
                                    {
                                        if (currentElement.Parent != null) currentElement = currentElement.Parent;
                                    }
                                    else
                                    {
                                        HtmlElement newElement = new HtmlElement()
                                        {
                                            Tag = buffer
                                        };
                                        newElement.Parent = currentElement;
                                        currentElement = newElement;
                                    }
                                    buffer = null;
                                }
                                else
                                    buffer += html[i];
                                break;
                            default:
                                buffer += html[i];
                                break;
                        }
                        break;
                    default:
                        switch (html[i])
                        {
                            case '<':
                                if (!string.IsNullOrEmpty(buffer))
                                {
                                    currentElement.Childs.Add(
                                        new HtmlElement() { InnerText = buffer });
                                }
                                buffer = null;
                                break;
                            case '>':
                                if (buffer != null && buffer.StartsWith("!--"))
                                {
                                    buffer = null;
                                    continue;
                                }
                                if (buffer.StartsWith("/") || buffer.EndsWith("/"))
                                {
                                    if (buffer.EndsWith("/") && buffer.Contains(" ")) //self closing tag
                                    {
                                        HtmlElement newElement = new HtmlElement()
                                        {
                                            Tag = buffer
                                        };
                                        newElement.Parent = currentElement;
                                        currentElement.Childs.Add(newElement);
                                    }
                                    else
                                    {
                                        string tempTag = buffer.Replace("/", "").ToLowerInvariant();
                                        if (currentElement.Parent != null)
                                        {
                                            if (currentElement.GetTagName() == tempTag)
                                            {
                                                currentElement = currentElement.Parent;
                                            }
                                            else if (currentElement.Parent.Parent != null
                                                && currentElement.Parent.Parent.GetTagName() == tempTag)
                                            {
                                                currentElement = currentElement.Parent.Parent;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    HtmlElement newElement = new HtmlElement()
                                    {
                                        Tag = buffer
                                    };
                                    newElement.Parent = currentElement;
                                    currentElement.Childs.Add(newElement);

                                    if (newElement.GetTagName() != "br"
                                        && newElement.GetTagName() != "meta"
                                        && newElement.GetTagName() != "img"
                                        && newElement.GetTagName() != "!doctype")
                                    {
                                        currentElement = newElement;
                                    }
                                }
                                buffer = null;
                                break;
                            default:
                                buffer += html[i];
                                break;
                        }
                        break;
                }
            }
        }

        public List<HtmlElement> GetElementsByTag(string tag, string attribute, string value, HtmlElement element)
        {
            List<HtmlElement> allTags = GetElementsByTag(tag, element, null);

            List<HtmlElement> result = new List<HtmlElement>();
            foreach (HtmlElement e in allTags)
            {
                if (e.GetAttribute(attribute) == value) result.Add(e);
            }

            return result;
        }

        public List<HtmlElement> GetElementsByTag(string tag, HtmlElement element, List<HtmlElement> list)
        {
            if (list == null) list = new List<HtmlElement>();

            if (element.GetTagName() == tag) list.Add(element);

            if (element.ChildCount > 0)
                foreach (HtmlElement child in element.Childs)
                    GetElementsByTag(tag, child, list);

            return list;
        }

        public HtmlElement GetFirst(string tag, HtmlElement element)
        {
            if (element.GetTagName() == tag) return element;

            if (element.ChildCount > 0)
                foreach (HtmlElement child in element.Childs)
                {
                    HtmlElement e = GetFirst(tag, child);
                    if (e != null) return e;
                }

            return null; ;
        }

        public static string HtmlTrimLeft(string html)
        {
            html = html.Trim();
            while (html.StartsWith("<br>", StringComparison.InvariantCultureIgnoreCase)
                || html.StartsWith("<br/>", StringComparison.InvariantCultureIgnoreCase)
                || html.StartsWith("&nbsp;", StringComparison.InvariantCultureIgnoreCase))
            {
                if (html.StartsWith("<br>", StringComparison.InvariantCultureIgnoreCase))
                    html = html.Remove(0, 4).Trim();
                if (html.StartsWith("<br/>", StringComparison.InvariantCultureIgnoreCase))
                    html = html.Remove(0, 5).Trim();
                if (html.StartsWith("&nbsp;", StringComparison.InvariantCultureIgnoreCase))
                    html = html.Remove(0, 6).Trim();
            }
            return html;
        }
    }
}
