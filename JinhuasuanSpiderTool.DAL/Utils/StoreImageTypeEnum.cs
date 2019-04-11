using System.ComponentModel;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    /// <summary>
    /// 店铺图片分类
    /// </summary>
    public enum StoreImageTypeEnum
    {
        /// <summary>
        /// 外景图
        /// </summary>
        [Description("外景图")] OutDoor = 1,

        /// <summary>
        /// 轮播图
        /// </summary>
        [Description("轮播图")] Shuffling = 2
    }
}