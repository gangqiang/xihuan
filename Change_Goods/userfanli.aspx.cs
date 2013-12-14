//======================================================================
// Copyright (c) 苏州同程旅游网络科技有限公司. All rights reserved.
// 所属项目：Change_Goods
// 创 建 人：wgq
// 创建日期：2010-12-8 11:08:30
// 用    途：淘宝返利申请提现
//====================================================================== 

using System;
using BusinessEntity;
using BusinessFacade;
/// <summary>
/// 在此描述userfanli的说明
/// </summary>
public partial class userfanli : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membercenter.aspx");
        }
    }
    #endregion

    #region  申请提现
    protected void btnChange_Click(object sender, EventArgs e)
    {
        #region 服务端验证
        string account = txtTaoBaoAccount.Text.Trim();
        decimal amount = CommonMethod.ConvertToDecimal(txtAmount.Text, 0);
        if (account.Length == 0)
        {
            Alert("请输入您要申请提现的淘宝账号！");
            Select(txtTaoBaoAccount);
            return;
        }
        if (amount == 0)
        {
            Alert("申请提现的金额应为大于0的数字！");
            Select(txtTaoBaoAccount);
            return;
        }
        #endregion

        #region 提交申请

        XiHuan_UserRequireEntity require = new XiHuan_UserRequireEntity();
        require.UserId = CurrentUserId;
        require.UserName = CurrentUserName;
        require.RequireType = (byte)XiHuan_UserRequireFacade.RequireType.AlipayCash;
        require.RequireContent = "申请淘宝购物返利提现,申请人淘宝账号" + account + "，申请提现金额：" + amount;
        require.ReuireDate = DateTime.Now;
        require.IsChecked = 0;
        require.Save();
        Alert("恭喜：您的提现申请已经成功提交，我们会尽快审核，审核后会以站内信通知您，请注意查收^_^！");

        #endregion
    }
    #endregion
}
