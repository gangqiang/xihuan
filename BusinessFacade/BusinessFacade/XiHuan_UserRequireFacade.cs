//======================================================================
// Copyright (c) 苏州同程旅游网络科技有限公司. All rights reserved.
// 所属项目：BusinessFacade
// 创 建 人：wgq
// 创建日期：2010-12-8 11:37:20
// 用    途：网站会员的请求业务逻辑处理
//====================================================================== 

namespace BusinessFacade
{
    using System.ComponentModel;

    /// <summary>
    /// 在此描述XiHuan_UserRequireFacade的说明
    /// </summary>
    public class XiHuan_UserRequireFacade
    {

        public enum RequireType
        {
            [Description("淘宝返利提现")]
            AlipayCash = 0
        }
    }
}
