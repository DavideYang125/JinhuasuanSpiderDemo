using JinhuasuanSpiderTool.DAL.Model;
using JinhuasuanSpiderTool.DAL.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static JinhuasuanSpiderTool.DAL.Utils.GeoCoding;

namespace JinhuasuanSpiderTool
{
    public class DataHandle
    {
        private static string imgHost = "https://images.matyun.com/";

        public static void Test()
        {
            var path = @"D:\jinhuasuan\jinhuasuan_store_data.json";
            var content = File.ReadAllText(path, Encoding.UTF8);
            List<JinhuasuanStoreModel> Models = JsonConvert.DeserializeObject<List<JinhuasuanStoreModel>>(content);//10795
            var telModels = Models.Where(m => string.IsNullOrEmpty(m.announcement)).ToList();//10336   471  10324
            //10795条数据   10336条手机号  10324条格式正确的手机号
            foreach (var item in telModels)
            {
                LogHelper.WriteLogs(item.tel, "tel");
            }
        }
        public static List<JinhuasuanStoreModel> CombineJson()
        {
            var path = @"D:\jinhuasuan\json";
            var files = Directory.GetFiles(path);
            List<JinhuasuanStoreModel> models = new List<JinhuasuanStoreModel>();
            foreach (var filePath in files)
            {
                try
                {
                    Console.WriteLine(filePath);
                    if (filePath.Contains("fail")) continue;
                    var content = File.ReadAllText(filePath, Encoding.UTF8);
                    List<JinhuasuanStoreModel> singlePageModels = JsonConvert.DeserializeObject<List<JinhuasuanStoreModel>>(content);
                    models.AddRange(singlePageModels);
                }
                catch
                {
                    LogHelper.WriteLogs(filePath, "解析失败");
                }
            }
            HashSet<string> hashSet = new HashSet<string>();
            List<JinhuasuanStoreModel> newModels = new List<JinhuasuanStoreModel>();
            foreach (var model in models)
            {
                Console.WriteLine(model.user_id);
                if (!hashSet.Add(model.user_id)) continue;
                newModels.Add(model);
            }

            return newModels;
        }

        public static void ImportData()
        {
            using (JinhuasuanStoreContext context = new JinhuasuanStoreContext())
            {
                var results = CombineJson();
                foreach (var result in results)
                {

                    var userIdStr = result.user_id;
                    Console.WriteLine(userIdStr);
                    var userId = Convert.ToInt32(userIdStr);
                    var jsonStr = JsonConvert.SerializeObject(result);
                    var newStore = new JinhuasuanStore()
                    {
                        StoreId = userId,
                        CreateTime = DateTime.Now,
                        Content = jsonStr,
                        Status = (int)SyncStatus.Wait,
                        ErrorCount = 0
                    };
                    context.JinhuasuanStore.Add(newStore);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 同步数据
        /// </summary>
        public static void SyncData()
        {
            try
            {
                StoreType storeType = new StoreType();
                OwnTypeDic ownStoreType = new OwnTypeDic();
                var typeDic = storeType.typeDic;
                var ownTypeDic = ownStoreType.ownTypeDic;
                var logName = "导入日志";
                var finish = false;
                while (!finish)
                {
                    using (JinhuasuanStoreContext _context = new JinhuasuanStoreContext())
                    {
                        var citys = _context.Citys.ToList();
                        var provinces = _context.Provinces.ToList();
                        var districts = _context.Districts.ToList();
                        var allCount = _context.JinhuasuanStore.Where(l => l.Status == (int)SyncStatus.Wait).ToList().Count();
                        var jinhuasuanStores = _context.JinhuasuanStore.Where(l => l.Status == (int)SyncStatus.Wait).OrderBy(l => l.StoreId).Take(100).ToList();
                        foreach (var outJinhuasuanStore in jinhuasuanStores)
                        {
                            allCount--;
                            Console.WriteLine("数量:" + allCount);
                            using (JinhuasuanStoreContext interContext = new JinhuasuanStoreContext())
                            {
                                var jinhuasuanStore = interContext.JinhuasuanStore.FirstOrDefault(m => m.Id == outJinhuasuanStore.Id);
                                if (jinhuasuanStore.Status != (int)SyncStatus.Wait) continue;
                                Console.WriteLine(jinhuasuanStore.StoreId);
                                var jsonContent = jinhuasuanStore.Content;
                                var storeInfo = JsonConvert.DeserializeObject<JinhuasuanStoreModel>(jsonContent);

                                //坐标
                                var coordinates = storeInfo.coordinates;

                                //详细地址
                                var address = storeInfo.address;

                                //logo
                                var logoImg = storeInfo.logo;
                                //logoImg = imgHost + logoImg;//已改为公司自己的图片。
                                //weixin logo
                                var weixin_logo = storeInfo.weixin_logo;
                                //weixin_logo = imgHost + weixin_logo;
                                //身份证img
                                var sfzimg = storeInfo.sfz_img;
                                //sfzimg = imgHost + sfzimg;
                                //营业执照img
                                var yyzz_img = storeInfo.yyzz_img;
                                //yyzz_img = imgHost + yyzz_img;

                                //tel
                                var tel = storeInfo.tel.Trim();
                                if (!tel.StartsWith("1") || tel.Length != 11)
                                {
                                    Console.WriteLine("号码不对");
                                    jinhuasuanStore.Status = (int)SyncStatus.Igonre;
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString() + "@" + jinhuasuanStore.Status.ToString(), logName);
                                    interContext.SaveChanges();
                                    continue;
                                }
                                var typeId = storeInfo.storetype_id.Trim();
                                if (!typeDic.ContainsKey(typeId))
                                {
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString() + "@" + typeId, "类型不存在");
                                    continue;
                                }
                                var type = typeDic[typeId];
                                var ownTypeIdStr = ownTypeDic[type];
                                var ownTypeId = Convert.ToInt32(ownTypeIdStr);
                                var storeName = storeInfo.store_name;
                                var announCement = storeInfo.announcement;
                                if (string.IsNullOrEmpty(announCement)) announCement = storeName;
                                var detail = storeInfo.details;
                                if (string.IsNullOrEmpty(detail)) detail = storeInfo.announcement;
                                //轮播
                                var ads = storeInfo.ad;
                                List<string> adImgs = new List<string>();
                                var adImgInfos = ads.Split(',');
                                foreach (var adImgInfo in adImgInfos)
                                {
                                    if (!adImgInfo.ToLower().Contains(".jpg") && !adImgInfo.ToLower().Contains(".png")) continue;
                                    adImgs.Add(adImgInfo.Trim());
                                }

                                //外景
                                var imgs = storeInfo.img;
                                List<string> waiImgs = new List<string>();
                                var waiImgInfos = imgs.Split(',');
                                foreach (var waiImgInfo in waiImgInfos)
                                {
                                    if (!waiImgInfo.ToLower().Contains(".jpg") && !waiImgInfo.ToLower().Contains(".png")) continue;
                                    waiImgs.Add(waiImgInfo.Trim());
                                }

                                //轮播\外景
                                List<StoreImagesParam> storeImagesParams = new List<StoreImagesParam>();
                                foreach (var adImg in adImgs)
                                {
                                    storeImagesParams.Add(new StoreImagesParam() { Path = adImg, ShufflingType = StoreImageTypeEnum.Shuffling });
                                }
                                foreach (var waiImg in waiImgs)
                                {
                                    storeImagesParams.Add(new StoreImagesParam() { Path = waiImg, OutDoorType = StoreImageTypeEnum.OutDoor });
                                }

                                //设施
                                List<StoreFacilitiesTypeEnum> storeFacilitiesTypes = new List<StoreFacilitiesTypeEnum>();
                                var skzf = storeInfo.skzf;
                                if (skzf == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.ACreditCard);
                                var wifi = storeInfo.wifi;
                                if (wifi == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.FreeWifi);
                                var mftc = storeInfo.mftc;
                                if (mftc == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.FreeParking);
                                var jzxy = storeInfo.jzxy;
                                if (jzxy == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.NoSmoking);
                                var tgbj = storeInfo.tgbj;
                                if (tgbj == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.ProvideRooms);
                                var sfxx = storeInfo.sfxx;
                                if (sfxx == "1") storeFacilitiesTypes.Add(StoreFacilitiesTypeEnum.Leisure);

                                //行业关键字
                                var keyWordStr = storeInfo.keyword;
                                keyWordStr = keyWordStr.Replace("  ", " ").Replace(" ", "，");
                                var keyWords = new List<string>();
                                var keyWordInfos = keyWordStr.Split("，");
                                foreach (var item in keyWordInfos)
                                {
                                    if (string.IsNullOrEmpty(item)) continue;
                                    keyWords.Add(item.Trim());
                                }

                                StoreEnterpriseCertificationsPublicParam storeEnterpriseCertificationsPublicParam = new StoreEnterpriseCertificationsPublicParam();
                                storeEnterpriseCertificationsPublicParam.EnterpriseName = storeName;
                                storeEnterpriseCertificationsPublicParam.BusinessId = ownTypeId;
                                storeEnterpriseCertificationsPublicParam.LegalPerson = "法人";
                                storeEnterpriseCertificationsPublicParam.PersonFront = sfzimg;
                                storeEnterpriseCertificationsPublicParam.PersonReverseSide = sfzimg;
                                storeEnterpriseCertificationsPublicParam.BusinessLicensePath = yyzz_img;
                                storeEnterpriseCertificationsPublicParam.BusinessLicense = "11111";

                                if (!coordinates.Contains(","))
                                {
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString(), "没有经纬度");
                                    jinhuasuanStore.Status = (int)SyncStatus.Igonre;
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    interContext.SaveChanges();
                                    continue;
                                }
                                //34.77061,113.734955

                                var coorInfos = coordinates.Split(',');

                                var lat = coorInfos[0].ToString().Trim();
                                var lng = coorInfos[1].ToString().Trim();

                                AddressValueModel addressValueMode = new AddressValueModel();
                                var siteInfos = GetSiteId(citys, provinces, districts, coordinates);
                                if (siteInfos.Item1 == -1 || siteInfos.Item2 == -1 || siteInfos.Item3 == -1)
                                {
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    Console.WriteLine("获取地址失败");
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    jinhuasuanStore.Status = (int)SyncStatus.SiteError;
                                    interContext.SaveChanges();
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString(), "获取地址失败");
                                    continue;
                                }
                                LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString() + "@" + siteInfos.Item1 + "@" + siteInfos.Item2 + "@" + siteInfos.Item3, "地址id记录");
                                var pId = siteInfos.Item1;
                                var cId = siteInfos.Item2;
                                var dId = siteInfos.Item3;
                                addressValueMode.Lat = Convert.ToDecimal(lat);
                                addressValueMode.Lng = Convert.ToDecimal(lng);
                                addressValueMode.province_id = pId;
                                addressValueMode.city_id = cId;
                                addressValueMode.area_id = dId;
                                addressValueMode.post_code = siteInfos.Item4;
                                addressValueMode.Address = address;

                                var userId = GetUserId(tel.ToString(), address, pId.ToString(), cId.ToString(), dId.ToString());

                                if (userId == -1)
                                {
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString(), "店铺已存在");
                                    jinhuasuanStore.Status = (int)SyncStatus.Exist;
                                    interContext.SaveChanges();
                                    continue;
                                }
                                else if (userId == -2)
                                {
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString(), "获取userid失败");
                                    continue;
                                }

                                jinhuasuanStore.OwnUserId = userId;

                                EditStoresForgerParam param = new EditStoresForgerParam();
                                param.BusinessId = ownTypeId;
                                param.Name = storeName;
                                param.AddressValue = addressValueMode;
                                param.StoreFacilitiesTypes = storeFacilitiesTypes;
                                param.AnnounCement = announCement;
                                param.Abstract = detail;
                                param.Logo = logoImg;
                                param.StoreImagesParams = storeImagesParams;
                                param.Content = tel;
                                param.BossWechat = weixin_logo;
                                param.IndustryKeyword = keyWords;
                                param.StoreEnterpriseCertificationsPublic = storeEnterpriseCertificationsPublicParam;
                                param.UserId = userId;
                                var ownStoreId = CreateStore(param);
                                if (ownStoreId == -1)
                                {
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    LogHelper.WriteLogs(jinhuasuanStore.StoreId.ToString(), "店铺已存在");
                                    jinhuasuanStore.Status = (int)SyncStatus.Exist;
                                }
                                else if (ownStoreId == -2)
                                {
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    jinhuasuanStore.ErrorCount = jinhuasuanStore.ErrorCount + 1;
                                    if (jinhuasuanStore.ErrorCount >= 3)
                                    {
                                        jinhuasuanStore.Status = (int)SyncStatus.Fail;
                                    }
                                }
                                else
                                {
                                    jinhuasuanStore.UpdateTime = DateTime.Now;
                                    jinhuasuanStore.OwnStoreId = ownStoreId;
                                    jinhuasuanStore.SyncTime = DateTime.Now;
                                    jinhuasuanStore.Status = (int)SyncStatus.Success;
                                }
                                interContext.SaveChanges();
                            }
                        }
                        var existsData = _context.JinhuasuanStore.Where(l => l.Status == (int)SyncStatus.Wait).ToList();
                        if (existsData.Count > 0) finish = false;
                        else finish = true;
                    }
                    LogHelper.WriteLogs("同步完成", "isfinish");
                    Console.WriteLine("同步完成");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.ToString(), "exception");
            }
        }

        public static int CreateStore(EditStoresForgerParam param)
        {
            var url = "";
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    _client.DefaultRequestHeaders.Add("Forgery", "Forgery");
                    StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8,
                                "application/json");
                    HttpResponseMessage response = _client.PostAsync(url, content).Result;
                    string data = response.Content.ReadAsStringAsync().Result;
                    if (data.Contains("{\"msg\":\"此店铺名称已被使用")) return -1;
                    if (data.Contains("\"msg\":\"")) return -2;
                    // if(data.con) {"msg":"此店铺名称已被使用"
                    //  {\"msg\":\"此店铺名称已被使用"
                    UserResponse r = JsonConvert.DeserializeObject<UserResponse>(data);
                    if (r.response.success)
                    {
                        return r.response.id;
                    }
                    //获取userid 失败
                    return -2;
                }
            }
            catch (Exception)
            {
                return -2;
            }


        }

        public static int GetUserId(string mobile, string address, string province_id, string city_id, string area_id)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    _client.DefaultRequestHeaders.Add("Forgery", "Forgery");
                    List<KeyValuePair<string, string>> paramss = new List<KeyValuePair<string, string>>();
                    PublicRegisterParam param = new PublicRegisterParam();
                    param.mobile = mobile;
                    AddressValueModel addresss = new AddressValueModel();
                    addresss.Address = address;
                    addresss.area_id = Convert.ToInt32(area_id);
                    addresss.province_id = Convert.ToInt32(province_id);
                    addresss.city_id = Convert.ToInt32(city_id);
                    param.address = addresss;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8,
                                "application/json");
                    HttpResponseMessage response = _client.PostAsync("", content).Result;
                    string data = response.Content.ReadAsStringAsync().Result;
                    if (data.Contains("\"msg\":\"-1\"")) return -1;

                    UserResponse r = JsonConvert.DeserializeObject<UserResponse>(data);
                    if (r.response.success)
                    {
                        return r.response.id;
                    }
                    //获取userid 失败
                    return -2;
                }
            }
            catch (Exception)
            {
                return -2;
            }

        }

        /// <summary>
        /// 获取当前坐标的省市区三级id.
        /// </summary>
        /// <param name="citys"></param>
        /// <param name="provinces"></param>
        /// <param name="districts"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public static Tuple<int, int, int, string> GetSiteId(List<Citys> citys, List<Provinces> provinces, List<Districts> districts, string coordinates)
        {
            var pId = -1;
            var cId = -1;
            var dId = -1;
            var code = "450000";
            try
            {

                //34.77061,113.734955

                var coorInfos = coordinates.Split(',');

                var lat = coorInfos[1].ToString();
                var lng = coorInfos[0].ToString();
                var location = $"{lat},{lng}";
                var url = $"https://restapi.amap.com/v3/geocode/regeo?key=e3f9b184b21edc9111560f37883648db&location={location}";
                var result = JinhuasuanData.GetHtmlContent(url);
                if (result.Item1 != HttpStatusCode.OK)
                {
                    return new Tuple<int, int, int, string>(pId, cId, dId, code);
                }
                var resultJson = result.Item2;
                var siteInfo = JsonConvert.DeserializeObject<GeoModel>(resultJson);
                var addressComponent = siteInfo.regeocode.addressComponent;
                var cityInfo = addressComponent.city;
                var provinceInfo = addressComponent.province;
                var districtInfo = addressComponent.district;
                var adCodeInfo = addressComponent.adcode;
                if (!string.IsNullOrEmpty(adCodeInfo)) code = adCodeInfo;
                var currentProvince = provinces.FirstOrDefault(l => l.Name == provinceInfo);
                if (currentProvince is null)
                {
                    return new Tuple<int, int, int, string>(pId, cId, dId, code);
                }
                pId = currentProvince.Id;
                var currentCity = citys.FirstOrDefault(l => l.Name == cityInfo && l.ProvinceId == pId);

                if (currentCity is null)
                {
                    return new Tuple<int, int, int, string>(pId, cId, dId, code);
                }
                cId = currentCity.Id;
                var currentDistract = districts.FirstOrDefault(l => l.Name == districtInfo && l.CityId == currentCity.Id);

                if (currentCity is null)
                {
                    return new Tuple<int, int, int, string>(pId, cId, dId, code);
                }
                dId = currentDistract.Id;
                return new Tuple<int, int, int, string>(pId, cId, dId, code);
            }
            catch (Exception)
            {
                return new Tuple<int, int, int, string>(pId, cId, dId, code);
            }

        }


    }
    public class UserResponse
    {
        //  {"code":200,"response":{"id":33582,"success":true}}
        public int code { get; set; }
        public Response response { get; set; }
    }
    public class Response
    {
        public int id { get; set; }
        public bool success { get; set; }
    }
}
