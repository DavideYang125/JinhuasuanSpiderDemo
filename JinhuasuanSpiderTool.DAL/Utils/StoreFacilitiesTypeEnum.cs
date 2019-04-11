using System.ComponentModel;

namespace JinhuasuanSpiderTool.DAL.Utils
{
    /// <summary>
    /// 店铺设施类型
    /// </summary>
    public enum StoreFacilitiesTypeEnum
    {
        /// <summary>
        /// 刷卡支付
        /// </summary>
        [Description("刷卡支付")] ACreditCard = 1,

        /// <summary>
        /// 免费Wifi
        /// </summary>
        [Description("免费Wifi")] FreeWifi = 2,

        /// <summary>
        /// 免费停车
        /// </summary>
        [Description("免费停车")] FreeParking = 3,

        /// <summary>
        /// 禁止吸烟
        /// </summary>
        [Description("禁止吸烟")] NoSmoking = 4,

        /// <summary>
        /// 提供包间
        /// </summary>
        [Description("提供包间")] ProvideRooms = 5,

        /// <summary>
        /// 沙发休闲
        /// </summary>
        [Description("沙发休闲")] Leisure = 6
    }
}