
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    public class EditStoresForgerParam
    {
        /// <summary>
        /// 主营类目id
        /// </summary>
        [Required(ErrorMessage = "请选择店铺类目")]
        [JsonProperty(PropertyName = "business_id")]
        public int BusinessId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required(ErrorMessage = "请填写店铺名称")]
        [StringLength(20, ErrorMessage = "店铺名称不得超过20个字符")]
        [RegularExpression("^(?!_)(?!.*?_$)[a-zA-Z0-9_\u2E80-\u9FFF]+$", ErrorMessage = "店铺名称之支持中英文以及数字和下划线")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 店铺位置
        /// </summary>
        [Required(ErrorMessage = "请填写店铺位置")]
        [JsonProperty(PropertyName = "address_value")]
        public AddressValueModel AddressValue { get; set; }

        /// <summary>
        /// 店内设施
        /// </summary>
        [JsonProperty(PropertyName = "store_facilities_types")]
        public List<StoreFacilitiesTypeEnum> StoreFacilitiesTypes { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [JsonProperty(PropertyName = "announ_cement")]
        [Required(ErrorMessage = "请输入商家公告")]
        public string AnnounCement { get; set; }

        /// <summary>
        /// 店铺简介
        /// </summary>
        [JsonProperty(PropertyName = "abstract")]
        [Required(ErrorMessage = "请输入商家简介")]
        public string Abstract { get; set; }

        /// <summary>
        /// 店铺logo
        /// </summary>
        [JsonProperty(PropertyName = "logo")]
        [Required(ErrorMessage = "请上传店铺Logo")]
        public string Logo { get; set; }

        /// <summary>
        /// 轮播图/外景图
        /// </summary>
        [JsonProperty(PropertyName = "store_images_params")]
        public List<StoreImagesParam> StoreImagesParams { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        [Required(ErrorMessage = "请输入商家联系方式")]
        public string Content { get; set; }

        /// <summary>
        /// 老板微信logo
        /// </summary>
        [JsonProperty(PropertyName = "boss_wechat")]
        [Required(ErrorMessage = "请上传老板微信")]
        public string BossWechat { get; set; }

        /// <summary>
        /// 行业关键字
        /// </summary>
        [JsonProperty(PropertyName = "industry_keyword")]
        public List<string> IndustryKeyword { get; set; }

        /// <summary>
        /// 企业认证
        /// </summary>
        [JsonProperty(PropertyName = "store_enterprise_certi_fications_public")]
        public StoreEnterpriseCertificationsPublicParam StoreEnterpriseCertificationsPublic { get; set; }

        /// <summary>
        /// 用户id  需注册用户
        /// </summary>
        [JsonProperty(PropertyName ="user_id")]
        public int UserId { get; set; }
        
    }
}
