using System.Collections.Generic;

namespace JinhuasuanSpiderTool.DAL.Model
{
    public class JinhuasuanStoreModel
    {
        public string id { get; set; }

        /// <summary>
        /// 商店id
        /// </summary>

        public string user_id { get; set; }

        /// <summary>
        /// 商店名称
        /// </summary>

        public string store_name { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string announcement { get; set; }

        /// <summary>
        /// 店铺类型
        /// </summary>

        public string storetype_id { get; set; }

        public string storetype2_id { get; set; }

        public string area_id { get; set; }

        public string yy_time { get; set; }

        /// <summary>
        /// 行业关键字
        /// </summary>

        public string keyword { get; set; }

        /// <summary>
        /// 刷卡支付
        /// </summary>
        public string skzf { get; set; }

        /// <summary>
        /// wifi
        /// </summary>
        public string wifi { get; set; }

        /// <summary>
        /// 免费停车
        /// </summary>
        public string mftc { get; set; }

        /// <summary>
        /// 禁止吸烟
        /// </summary>
        public string jzxy { get; set; }

        /// <summary>
        /// 提供包间
        /// </summary>
        public string tgbj { get; set; }


        /// <summary>
        /// 沙发休闲
        /// </summary>
        public string sfxx { get; set; }


        /// <summary>
        /// 电话
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// logo img url
        /// </summary>
        public string logo { get; set; }

        public string weixin_logo { get; set; }

        /// <summary>
        /// 轮播图 图片
        /// </summary>
        public string ad { get; set; }

        public string state { get; set; }

        public string money { get; set; }

        public string password { get; set; }

        /// <summary>
        /// 商家介绍
        /// </summary>
        public string details { get; set; }

        public string uniacid { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public string coordinates { get; set; }

        /// <summary>
        /// 人气 浏览量
        /// </summary>
        public string views { get; set; }

        public string score { get; set; }

        public string type { get; set; }

        public string sh_time { get; set; }

        public string time_over { get; set; }

        /// <summary>
        /// 详情页展示图，即外景图
        /// </summary>
        public string img { get; set; }
        public string vr_link { get; set; }
        public string num { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string wallet { get; set; }
        public string user_name { get; set; }
        public string pwd { get; set; }
        public string dq_time { get; set; }
        public string cityname { get; set; }
        public string time { get; set; }

        public string fx_num { get; set; }
        public string ewm_logo { get; set; }
        public string is_top { get; set; }
        public string yyzz_img { get; set; }
        public string sfz_img { get; set; }
        public string is_rm { get; set; }
        public string video { get; set; }
        public string juli { get; set; }

    }

    public class StoreType
    {
        public Dictionary<string, string> typeDic = new Dictionary<string, string>();
        public StoreType()
        {
            typeDic.Add("1", "房产中介");
            typeDic.Add("2", "教育培训");
            typeDic.Add("3", "休闲娱乐");
            typeDic.Add("4", "酒店旅游");
            typeDic.Add("5", "餐饮美食");
            typeDic.Add("7", "生活服务");
            typeDic.Add("8", "购物广场");
            typeDic.Add("10", "美容美发");
            typeDic.Add("11", "汽车服务");
            typeDic.Add("12", "五金建材");
            typeDic.Add("13", "珠宝饰品");
            typeDic.Add("14", "家具家居");
            typeDic.Add("15", "烟酒茶饮");
            typeDic.Add("16", "服装鞋包");
            typeDic.Add("17", "水果鲜花");
            typeDic.Add("18", "文化用品");
            typeDic.Add("19", "化妆品");
            typeDic.Add("20", "二手车交易");
            typeDic.Add("21", "厨具用品");
            typeDic.Add("9", "其他行业");
        }
    }
}
