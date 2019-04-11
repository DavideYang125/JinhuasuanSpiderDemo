using JinhuasuanSpiderTool.DAL.Model;
using JinhuasuanSpiderTool.DAL.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace JinhuasuanSpiderTool
{
    public class ImgHandle
    {
        private static string imgHost = "https://images.matyun.com/";
        private static string baseImgPath = @"D:\jinhuasuan\down_img";
        /// <summary>
        /// 替换店铺信息里的图片地址
        /// </summary>
        public static void ReplaceImgUrl()
        {
            using (JinhuasuanStoreContext _context = new JinhuasuanStoreContext())
            {
                var stores = _context.JinhuasuanStore.Where(l => l.ImgReplaced == false);
                foreach (var store in stores)
                {
                    var storeId = store.StoreId;
                    Console.WriteLine(store.Id.ToString());
                    ReplaceSingleStore(storeId);
                }
            }
        }
        /// <summary>
        /// 替换单个店铺的图片地址
        /// </summary>
        /// <param name="storeId"></param>
        public static void ReplaceSingleStore(int storeId)
        {
            using (JinhuasuanStoreContext _context = new JinhuasuanStoreContext())
            {
                var storeInfo = _context.JinhuasuanStore.FirstOrDefault(l => l.StoreId == storeId);
                var content = storeInfo.Content;
                var storeDetailInfo = JsonConvert.DeserializeObject<JinhuasuanStoreModel>(content);
                var adStr = storeDetailInfo.ad;
                var imgStr = storeDetailInfo.img;
                var adList = adStr.Split(',').ToList();

                var imgList = imgStr.Split(',').ToList();

                adList = FilterImgList(adList);
                imgList = FilterImgList(imgList);

                var upLoadAddResult = GetNewImgStr(storeId, adList, (int)ImgTyepe.Lunbo);
                if (!upLoadAddResult.Item1)
                {
                    return;
                }
                storeDetailInfo.ad = upLoadAddResult.Item2;

                var upLoadImgResult = GetNewImgStr(storeId, imgList, (int)ImgTyepe.Lunbo);
                if (!upLoadImgResult.Item1) return;
                storeDetailInfo.img = upLoadAddResult.Item2;
                //轮播外景
                Console.WriteLine($"storeId--{storeId.ToString()}--轮播外景处理完成");

                //logo img
                var logoImg = storeDetailInfo.logo;
                logoImg = imgHost + logoImg;
                var logoImgList = new List<string>() { logoImg };
                var upLoadLogoResult = GetNewImgStr(storeId, logoImgList, (int)ImgTyepe.logo);
                if (!upLoadLogoResult.Item1)
                {
                    return;
                }
                storeDetailInfo.logo = upLoadLogoResult.Item2;
                Console.WriteLine($"storeId--{storeId.ToString()}--logo处理完成");
                //weixin logo
                var weixin_logo = storeDetailInfo.weixin_logo;
                weixin_logo = imgHost + weixin_logo;

                var weiXinImgList = new List<string>() { weixin_logo };
                var upLoadWeixinResult = GetNewImgStr(storeId, weiXinImgList, (int)ImgTyepe.logo);
                if (!upLoadWeixinResult.Item1)
                {
                    return;
                }
                storeDetailInfo.weixin_logo = upLoadWeixinResult.Item2;
                Console.WriteLine($"storeId--{storeId.ToString()}--weixin处理完成");



                //身份证img
                var sfzimg = storeDetailInfo.sfz_img;
                sfzimg = imgHost + sfzimg;

                var sfImgList = new List<string>() { sfzimg };
                var upLoadsfResult = GetNewImgStr(storeId, sfImgList, (int)ImgTyepe.Identify);
                if (!upLoadsfResult.Item1)
                {
                    return;
                }
                storeDetailInfo.sfz_img = upLoadsfResult.Item2;
                Console.WriteLine($"storeId--{storeId.ToString()}--身份证img处理完成");

                //营业执照img
                var yyzz_img = storeDetailInfo.yyzz_img;
                yyzz_img = imgHost + yyzz_img;
                var yyImgList = new List<string>() { yyzz_img };
                var upLoadyyResult = GetNewImgStr(storeId, yyImgList, (int)ImgTyepe.Identify);
                if (!upLoadyyResult.Item1)
                {
                    return;
                }
                storeDetailInfo.yyzz_img = upLoadsfResult.Item2;
                Console.WriteLine($"storeId--{storeId.ToString()}--yyzz img处理完成");


                var newJson = JsonConvert.SerializeObject(storeDetailInfo);
                storeInfo.UpdateTime = DateTime.Now;
                storeInfo.ImgReplaced = true;
                storeInfo.Content = newJson;

                _context.SaveChanges();
                Console.WriteLine($"店铺 storeId--{storeId.ToString()}--处理完成");
            }
        }

        public static List<string> FilterImgList(List<string> imgList)
        {
            for (int i = imgList.Count - 1; i >= 0; i--)
            {
                var l = imgList[i].Trim();
                if (!l.ToLower().Contains(".jpg") && !l.ToLower().Contains(".png"))
                    imgList.Remove(l);
                else imgList[i] = imgHost + l;
            }
            return imgList;
        }

        /// <summary>
        /// 下载多个图片
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="imgList"></param>
        /// <returns></returns>
        public static Tuple<bool, List<string>> DownLoadImgs(int storeId, List<string> imgList)
        {
            List<string> paths = new List<string>();
            foreach (var singleImg in imgList)
            {
                Console.WriteLine("处理图片--" + singleImg);
                var newId = Guid.NewGuid();
                var exten = singleImg.Substring(singleImg.LastIndexOf("."));
                var newImgPath = Path.Combine(baseImgPath, newId + exten);
                if (NetHandle.DownFileMethod(singleImg, newImgPath))
                {
                    paths.Add(newImgPath);
                }
                else
                {
                    LogHelper.WriteLogs(storeId.ToString() + "@" + singleImg, "下载图片失败");
                    Console.WriteLine("下载图片失败---" + singleImg);
                    return new Tuple<bool, List<string>>(false, paths);
                }
            }
            return new Tuple<bool, List<string>>(true, paths);
        }
        /// <summary>
        /// get new img urls
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="imgList"></param>
        /// <returns></returns>
        public static Tuple<bool, string> GetNewImgStr(int storeId, List<string> imgList, int type)
        {
            if (imgList.Count == 0) return new Tuple<bool, string>(true, "");
            var downResult = DownLoadImgs(storeId, imgList);
            if (!downResult.Item1) return new Tuple<bool, string>(false, "");
            var imgPaths = downResult.Item2;
            var newUrls = UploadImgs(imgPaths, type);
            if (string.IsNullOrEmpty(newUrls))
            {
                Thread.Sleep(1000 * 1);
                newUrls = UploadImgs(imgPaths, type);
            }
            if (string.IsNullOrEmpty(newUrls))
            {
                Console.WriteLine("上传失败");
                return new Tuple<bool, string>(false, newUrls);
            }

            return new Tuple<bool, string>(true, newUrls);
        }


        /// <summary>
        /// 请求上传图片接口
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static string UploadImgs(List<string> imgPaths, int type = 1)
        {
            try
            {
                var url = $"http:///api/storage/publicsmallprogram/v1/Upload/Post?resource_source={type.ToString()}";
                using (HttpClient _client = new HttpClient())
                {
                    _client.DefaultRequestHeaders.Add("Platform", "wuqdIy%2fjx8TM3hFo1OnNrQ%3d%3d");
                    using (var multiContent = new MultipartFormDataContent())
                    {
                        for (int i = 0; i < imgPaths.Count; i++)
                        {
                            var imgPath = imgPaths[i];
                            var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(imgPath));
                            multiContent.Add(fileContent, "file_" + i.ToString(), Path.GetFileName(imgPath));
                        }

                        HttpResponseMessage response = _client.PostAsync(url, multiContent).Result;
                        string data = response.Content.ReadAsStringAsync().Result;
                        if (!data.Contains("response")) return string.Empty;
                        ImgResponse imgResponse = JsonConvert.DeserializeObject<ImgResponse>(data);
                        var imgs = imgResponse.response.data;
                        var imgUrl = String.Join(",", imgs.ToArray());
                        return imgUrl;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
    public enum ImgTyepe
    {
        Lunbo = 1,
        Identify = 6,
        yy = 7,
        weixin = 4,
        logo = 3
    }
    public class ImgResponse
    {
        public int code { get; set; }

        public ImgInnerResponse response { get; set; }

    }
    public class ImgInnerResponse
    {
        public bool statue { get; set; }
        public List<string> data { get; set; }
        public string msg { get; set; }
    }
}
