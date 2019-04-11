
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    public class PublicRegisterParam
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "mobile")]
        [Required(ErrorMessage = "手机号信息有误")]
        [Phone]
        public string mobile { get; set; }

        /// <summary>
        /// 地址信息
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public AddressValueModel address { get; set; }

    }
}
