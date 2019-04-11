using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinhuasuanSpiderTool.DAL.Utils
{

    /// <summary>
    /// 地址信息
    /// </summary>
    public class AddressValueModel
    {
        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty(PropertyName = "lng")]
        public decimal Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty(PropertyName = "lat")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 省份id
        /// </summary>
        [JsonProperty(PropertyName = "province_id")]
        public int province_id { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [JsonProperty(PropertyName = "province_name")]
        public string ProviceName { get; set; }

        /// <summary>
        /// 城市id
        /// </summary>
        [JsonProperty(PropertyName = "city_id")]
        public int city_id { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [JsonProperty(PropertyName = "city_name")]
        public string CityName { get; set; }

        /// <summary>
        /// 区域id
        /// </summary>
        [JsonProperty(PropertyName = "area_id")]
        public int area_id { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [JsonProperty(PropertyName = "area_name")]
        public string AreaName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [JsonProperty(PropertyName = "post_code")]
        public string post_code { get; set; }

    }
}
