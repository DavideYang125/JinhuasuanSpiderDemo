using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    public class NetHandle
    {

        /// <summary>
        /// get content
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public static Tuple<HttpStatusCode, string> GetHtmlContent(string url, string parameter = "", string referer = "")
        {
            Tuple<HttpStatusCode, string> htmlResult = new Tuple<HttpStatusCode, string>(HttpStatusCode.Gone, string.Empty);
            string content = string.Empty;
            try
            {
                var clientHandler = new HttpClientHandler();
                if (clientHandler.SupportsAutomaticDecompression)
                {
                    clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                using (var httpClient = new HttpClient(clientHandler))
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    httpClient.Timeout = TimeSpan.FromSeconds(25);
                    requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36");
                    if (!string.IsNullOrEmpty(referer)) requestMessage.Headers.Add("Referer", referer);
                    var response = httpClient.SendAsync(requestMessage).Result;
                    content = response.Content.ReadAsStringAsync().Result;
                    htmlResult = new Tuple<HttpStatusCode, string>(response.StatusCode, content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return htmlResult;
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DownFileMethod(string url, string path)
        {
            var myWebClient = new WebClient();
            bool isSucess = false;
            if (!string.IsNullOrEmpty(url) && url.Trim().StartsWith("//"))
            {
                url = "http:" + url;
            }
            try
            {
                myWebClient.Headers.Set("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                myWebClient.DownloadFile(url.Trim(), path);
                File.SetAttributes(path, FileAttributes.Normal);
                isSucess = true;
            }
            catch (Exception) { }
            return isSucess;
        }
    }
}
