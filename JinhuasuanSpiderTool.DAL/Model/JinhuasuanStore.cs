using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JinhuasuanSpiderTool.DAL.Model
{
    [Table("jinhuasuan_store")]
    public class JinhuasuanStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 公司内部店铺id
        /// </summary>
        [Column("own_store_id")]
        public int? OwnStoreId { get; set; }

        /// <summary>
        /// 公司内部用户id
        /// </summary>
        [Column("own_user_id")]

        public int? OwnUserId { get; set; }



        [Column("store_id")]

        public int StoreId { get; set; }



        [Column("create_time")]

        public DateTime CreateTime { get; set; }


        [Column("update_time")]

        public DateTime? UpdateTime { get; set; }


        [Column("content")]
        public string Content { get; set; }


        [Column("status")]
        public int Status { get; set; }


        [Column("sync_time")]

        public DateTime? SyncTime { get; set; }

        /// <summary>
        /// 错误次数
        /// </summary>

        [Column("error_count")]

        public int ErrorCount { get; set; }

        /// <summary>
        /// 图片是否已替换
        /// </summary>
        [Column("img_replaced")]
        public bool ImgReplaced { get; set; }
    }
    /// <summary>
    /// 同步状态
    /// </summary>
    public enum SyncStatus
    {
        /// <summary>
        /// 等待同步
        /// </summary>
        Wait = 0,

        /// <summary>
        /// 同步成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 同步失败
        /// </summary>
        Fail = 2,

        /// <summary>
        /// 店铺已存在
        /// </summary>
        Exist = 3,

        /// <summary>
        /// 其他原因，不需要导入的，比如，号码是固话，或者手机号位数不对,或者没有经纬度。
        /// </summary>
        Igonre = 4,

        /// <summary>
        /// 获取地址失败
        /// </summary>
        SiteError = 5,
    };
}
