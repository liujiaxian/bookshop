using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace BookShop.Common
{
    public class WebCommon
    {
        /// <summary>
        /// 跳转到登录之前的页面
        /// </summary>
        //前台跳转
        public static void GoPage()
        {
            HttpContext.Current.Response.Redirect("/Login/Index?returnUrl=" + HttpContext.Current.Request.Url.ToString());
        }
        //后台跳转
        public static void GoAdminPage()
        {
            HttpContext.Current.Response.Redirect("/Admin/AdminLogin/Index");
        }
        //弹出框显示信息
        public static void Show(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "')</script>");
        }
        //弹出并跳转
        public static void ShowUrl(string msg, string url)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.location='" + url + "'</script>");
        }
        //直接跳转
        public static void Url(string url)
        {
            HttpContext.Current.Response.Write("<script>window.location='" + url + "'</script>");
        }
        //成功跳转
        public static void GoNext(string msg, string txt, string url)
        {
            HttpContext.Current.Response.Redirect("/Home/ShowMsg?Msg=" + HttpContext.Current.Server.UrlEncode(msg) + "&Txt=" + HttpContext.Current.Server.UrlEncode(txt) + "&Url=" + url);
        }
        //弹出信息并返回上个页面
        public static void GoBack(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.history.go(-1)</script>");
        }
        //字符串输出限制
        public static string CurrentString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + "......";
            }
            else
            {
                return str;
            }
        }

        public static string GetDir(DateTime time, int id)
        {
            return "/StaticPage/" + time.Year + "/" + time.Month + "/" + time.Day + "/" + id + ".html";
        }

        #region 评论时间处理
        public static string GetDateTime(TimeSpan time)
        {
            if (time.TotalDays >= 365)
            {
                return Math.Floor(time.TotalDays / 356) + "年前";
            }
            else if (time.TotalDays >= 30)
            {
                return Math.Floor(time.TotalDays / 30) + "月前";
            }
            else if (time.TotalHours >= 24)
            {
                return Math.Floor(time.TotalDays) + "天前";
            }
            else if (time.TotalHours >= 1)
            {
                return Math.Floor(time.TotalHours) + "小时前";
            }
            else if (time.TotalMinutes >= 1)
            {
                return Math.Floor(time.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }
        #endregion

        #region 将UBB编码替换成标准的HTML编码
        /// <summary>
        /// 将UBB编码替换成标准的HTML编码
        /// </summary>
        /// <param name="argString"></param>
        /// <returns></returns>
        public static string Decode(string argString)
        {
            string tString = argString;
            if (tString != "")
            {
                Regex tRegex;
                bool tState = true;
                tString = tString.Replace("&", "&amp;");
                tString = tString.Replace(">", "&gt;");
                tString = tString.Replace("<", "&lt;");
                tString = tString.Replace("\"", "&quot;");
                tString = Regex.Replace(tString, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
                string[,] tRegexAry = {
          {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
          {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
          {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
          {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
          {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
          {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
          {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
          {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
          {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
          {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
          {@"\[size=([^\]]*)\]([^\[]*?)\[\/size\]", "<font style=\"size: $1\">$2</font>"},
          {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
          {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
          {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
          {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
        };
                while (tState)
                {
                    tState = false;
                    for (int ti = 0; ti < tRegexAry.GetLength(0); ti++)
                    {
                        tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                        if (tRegex.Match(tString).Success)
                        {
                            tState = true;
                            tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            return tString;
        }
        #endregion

        #region 盘古分词
        public static string[] PanGuSplit(string key)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(key));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                //Console.WriteLine(token.TermText());
                list.Add(token.TermText());
            }
            return list.ToArray();
        }
        #endregion
        //创建HTMLFormatter,参数为高亮单词的前后缀
        public static string CreateHightLight(string keywords, string Content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
             new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, Content);
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }
        /// <summary>
        /// 解决HttpContext.Current.Server.MapPath空的问题
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string MapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用            
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');                  
                    strPath = strPath.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        public static string GetIPAddress()
        {
            string ip = "";
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData("http://www.net.cn/static/customercare/yourip.asp"); //从指定网站下载数据
                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            
                //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                Regex reg = new Regex(@"<h2>(.*?)</h2>", RegexOptions.IgnoreCase);//[^(<td>))] 
                string str = reg.Match(pageHtml).Value;
                string[] str1 = str.Replace("<h2>", "").Split(',');
                ip = str1[0];
            }
            catch (WebException webEx)
            {
                //webEx.Message.ToString()
            }
            return ip;
        }
    }
}
