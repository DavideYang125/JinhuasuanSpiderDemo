using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    /// <summary>
    /// 轮播图/外景图
    /// </summary>
    public class StoreImagesParam
    {

        /// <summary>
        /// 轮播图
        /// </summary>
        [JsonProperty(PropertyName = "shuffling_type")]
        public StoreImageTypeEnum? ShufflingType { get; set; }

        /// <summary>
        /// 外景图
        /// </summary>
        [JsonProperty(PropertyName = "outdoor_type")]
        public StoreImageTypeEnum? OutDoorType { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

    }
}
