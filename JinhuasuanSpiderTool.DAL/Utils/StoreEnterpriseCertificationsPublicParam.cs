
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    /// <summary>
    /// 企业认证
    /// </summary>
    public class StoreEnterpriseCertificationsPublicParam
    {
        /// <summary>
        /// 法人名字
        /// </summary>
        [Required(ErrorMessage = "请填写法人姓名")]
        [JsonProperty(PropertyName = "legal_person")]
        public string LegalPerson { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Required(ErrorMessage = "请填写企业名称")]
        [JsonProperty(PropertyName = "enterprise_name")]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 证件正面照
        /// </summary>
        [Required(ErrorMessage = "请上传法人的证件正面照")]
        [JsonProperty(PropertyName = "person_front")]
        public string PersonFront { get; set; }

        /// <summary>
        /// 证件反面照
        /// </summary>
        [Required(ErrorMessage = "请上传法人的证件反面照")]
        [JsonProperty(PropertyName = "person_reverse_side")]
        public string PersonReverseSide { get; set; }

        /// <summary>
        /// 营业执照照片
        /// </summary>
        [Required(ErrorMessage = "请上传公司的营业执照照片！")]
        [JsonProperty(PropertyName = "business_license_path")]
        public string BusinessLicensePath { get; set; }

        /// <summary>
        /// 营业执照号
        /// </summary>
        [Required(ErrorMessage = "请输入公司的营业执照号！")]
        [JsonProperty(PropertyName = "business_license")]
        public string BusinessLicense { get; set; }

        /// <summary>
        /// 类目id
        /// </summary>
        [JsonProperty(PropertyName = "business_id")]
        public int BusinessId { get; set; } = 0;

    }
}
