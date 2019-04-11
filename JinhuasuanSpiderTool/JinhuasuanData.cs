using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace JinhuasuanSpiderTool
{
    public class JinhuasuanData
    {
        public static string urlFormat = "https://mp.mvpshop.cn/app/index.php?i=2339&t=0&v=10.9.9&from=wxapp&c=entry&a=wxapp&do=StoreList&m=zh_tcwq&sign=8d06599d7bf4f4784b9fa584d9762dff&type=1&lat=34.767669&lng=113.726246&page={0}&cityname=%E9%83%91%E5%B7%9E%E5%B8%82";

        /// <summary>
        /// 获取jinhuasuan小程序的商家数据
        /// </summary>
        public static void Run()
        {
            var basePath = @"D:\jinhuasuan\json";
            var jsonName = @"jinhuasuan_data_{0}.json";
            for (int i = 1; i < 1500; i++)
            {
                Console.WriteLine($"第{i.ToString()}页");
                var jsonPath = Path.Combine(basePath, string.Format(jsonName, i.ToString()));
                if (File.Exists(jsonPath)) continue;
                var url = string.Format(urlFormat, i.ToString());
                using (HttpClient client = new HttpClient())
                {
                    var result = GetHtmlContent(url);
                    if (result.Item1 != HttpStatusCode.OK)
                    {
                        LogHelper.WriteLogs(i.ToString(), "jinhuasuan_download_fail");
                        Console.WriteLine($"第{i.ToString()}页请求失败");
                    }
                    else
                    {
                        if (!result.Item2.Contains("user_id"))
                        {
                            File.WriteAllText(jsonPath+"_fail.json", result.Item2, Encoding.UTF8);
                            Console.WriteLine($"第{i.ToString()}页下载失败");
                            continue;
                        }
                        var decodeContent = System.Text.RegularExpressions.Regex.Unescape(result.Item2);
                       // dynamic jsonObj = JsonConvert.DeserializeObject(decodeContent);
                        File.WriteAllText(jsonPath, decodeContent, Encoding.UTF8);
                        Console.WriteLine($"第{i.ToString()}页下载成功");
                        Thread.Sleep(1 * 1000);
                    }
                }
            }
        }
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
    }
}
