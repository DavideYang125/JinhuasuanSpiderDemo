
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    public class GeoCoding
    {


        /// <summary>
        /// 根据地址返回火星坐标
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <param name="city">城市</param>
        //public static GeoModel GetMars(double lat, double lng)
        //{
        //    var resultJson = HttpClient.GetResponse(
        //            "/v3/geocode/regeo", MethodEnum.Get, new
        //            {
        //                key = "e3f9b184b21edc9111560f37883648db",
        //                location = $"{lat},{lng}"
        //            }, apiHost: "https://restapi.amap.com");
        //    var r = JsonConvert.DeserializeObject<GeoModel>(resultJson);
        //    return Utils.JsonCommon.Deserialize<GeoModel>(resultJson);
        //}
        public class StreetNumber
        {
            /// <summary>
            /// 6-2号楼
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string location { get; set; }
            /// <summary>
            /// 东北
            /// </summary>
            public string direction { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string distance { get; set; }
            /// <summary>
            /// 阜通东大街
            /// </summary>
            public string street { get; set; }
        }

        public class BusinessAreasItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string location { get; set; }
            /// <summary>
            /// 望京
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
        }

        public class Building
        {
            /// <summary>
            /// 方恒国际中心A座
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 商务住宅;楼宇;商务写字楼
            /// </summary>
            public string type { get; set; }
        }

        public class Neighborhood
        {
            /// <summary>
            /// 方恒国际中心
            /// </summary>
            public List<string> name { get; set; }
            /// <summary>
            /// 商务住宅;楼宇;商住两用楼宇
            /// </summary>
            public List<string> type { get; set; }
        }

        public class AddressComponent
        {
            /// <summary>
            /// 
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 北京市
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string adcode { get; set; }
            /// <summary>
            /// 朝阳区
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string towncode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            //public StreetNumber streetNumber { get; set; }
            /// <summary>
            /// 中国
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 望京街道
            /// </summary>
            public string township { get; set; }
            /// <summary>
            /// 
            /// </summary>
            //public List<BusinessAreasItem> businessAreas { get; set; }
            /// <summary>
            /// 
            /// </summary>
            //public Building building { get; set; }
            /// <summary>
            /// 
            /// </summary>
            //public Neighborhood neighborhood { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string citycode { get; set; }
        }
        /// <summary>
        /// 地址信息
        /// </summary>
        public class Regeocode
        {
            /// <summary>
            /// 详细地址信息
            /// </summary>
            public AddressComponent addressComponent { get; set; }

            /// <summary>
            /// 北京市朝阳区望京街道方恒国际中心A座北京方恒假日酒店
            /// </summary>
            public string formatted_address { get; set; }
        }
        /// <summary>
        /// 位置模型
        /// </summary>
        public class GeoModel
        {
            /// <summary>
            /// 状态
            /// </summary>
            public string status { get; set; }

            /// <summary>
            /// 位置信息
            /// </summary>
            public Regeocode regeocode { get; set; }

            /// <summary>
            /// 信息
            /// </summary>
            public string info { get; set; }

            /// <summary>
            /// 信息状态
            /// </summary>
            public string infocode { get; set; }
        }
    }
}
