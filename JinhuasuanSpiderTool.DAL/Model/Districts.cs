
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JinhuasuanSpiderTool.DAL.Model
{
    /// <summary>
    /// 市/区
    /// </summary>
    [Table("deyouyun_address_districts")]
    public partial class Districts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 平台识别码
        /// </summary>
        [StringLength(10)]
        public string IdentificationCode { get; set; }

        /// <summary>
        /// 城市id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 市区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public char Initial { get; set; }

        /// <summary>
        /// 简拼
        /// 例如：北京 bj
        /// </summary>
        [MaxLength(50)]
        public string SimpleSpell { get; set; }

        /// <summary>
        /// 全拼
        /// </summary>
        [MaxLength(200)]
        public string FullSpell { get; set; }

        /// <summary>
        /// 唯一标示
        /// </summary>
        [MaxLength(6)]
        public string Unique { get; set; }

        /// <summary>
        /// 区关键字
        /// </summary>
        //[MaxLength(200)]
        //public List<string> KeyWord
        //{
        //    get =>Utils.JsonCommon.Deserialize<List<string>>(KeyWordJson);
        //    set => this.KeyWordJson = Utils.JsonCommon.Serializer(value);
        //}

        public string KeyWordJson { get; private set; } = "[]";
    }
}