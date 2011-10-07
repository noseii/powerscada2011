using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace System
{
    public class WebHelper
    {
        public static string GetContent(string url)
        {
            string s = "";
            return GetContent(url, ref s);
        }

        public static string GetContent(string url, ref string content_type)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            string content = "";
            try
            {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                content = client.DownloadString(url);
                string s = client.ResponseHeaders["Content-Type"];
                content_type = s;
                //bu if bir durum böyle başka durumlar da olabilir, onları da || ile eklemek lazım
                if (s.ToLower().Contains("utf-8") && client.Encoding != Encoding.UTF8)
                {
                    content = Encoding.UTF8.GetString(client.Encoding.GetBytes(content));
                }
                else if (content.Contains("Ã¼") || content.Contains("ÅŸ") || content.Contains("â€"))
                {
                    content = Encoding.UTF8.GetString(client.Encoding.GetBytes(content));
                }
                /*if (client.Encoding!=Encoding.ASCII)
                {
                    Encoding srcEncoding = client.Encoding;
                    if (s.ToLower().Contains("utf-8") && client.Encoding != Encoding.UTF8)
                        srcEncoding = Encoding.UTF8;

                    content = Encoding.Default.GetString(
                        Encoding.Convert(srcEncoding, Encoding.Default, client.Encoding.GetBytes(content)));
                }*/
            }
            catch
            {
                //TODO: Error handling
                client.Dispose();
                throw;
            }
            client.Dispose();

            return content;
        }

        public static long GetFileSize(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            req.Method = "HEAD";
            req.Timeout = 2000;
            try
            {
                WebResponse response = req.GetResponse();
                response.Close();
                return response.ContentLength;
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertRelativeUrlToAbsolute(string relativeUrl, string baseUrl)
        {
            return (new Uri(new Uri(baseUrl), relativeUrl)).ToString();
        }

        public static void GetFile(string url, string filename)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            try
            {
                client.DownloadFile(url, filename);
            }
            catch { }
            client.Dispose();
        }

        public static bool IsWebPage(string url)
        {
            return !IsBinary(url);
        }

        public static bool IsBinary(string url)
        {
            if (url.Length < 4) return false;

            string ext = url.Substring(url.Length - 4, 4).ToLower();

            if (ext.EndsWith(".bmp")) return true;
            if (ext.EndsWith(".jpg")) return true;
            if (ext.EndsWith(".png")) return true;
            if (ext.EndsWith(".ppt")) return true;
            if (ext.EndsWith(".pdf")) return true;
            if (ext.EndsWith(".zip")) return true;
            if (ext.EndsWith(".gif")) return true;
            if (ext.EndsWith(".doc")) return true;
            if (ext.EndsWith(".xls")) return true;
            if (ext.EndsWith(".wmv")) return true;
            if (ext.EndsWith(".avi")) return true;
            if (ext.EndsWith(".mp3")) return true;
            if (ext.EndsWith(".mp4")) return true;

            return false;
        }

        public static CookieContainer mySession = new CookieContainer();

        public static string PostData(string url, Dictionary<string, string> data)
        {
            // Create a request using a URL that can receive a post. 
            //WebRequest request = WebRequest.Create(url);            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            request.CookieContainer = mySession;

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = null;
            foreach (string key in data.Keys)
            {
                if (!string.IsNullOrEmpty(postData)) postData += "&";
                postData += key + "=" + HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(data[key] ?? ""));
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
