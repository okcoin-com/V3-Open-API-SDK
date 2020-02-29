using System;
using System.Collections.Generic;
using System.Text;

namespace OKCoinSDK.Models.Margin
{
    public class MarginOrderMarket : MarginOrder
    {
        /// <summary>
        /// 买入金额，市价买入是必填notional
        /// </summary>
        public string notional { get; set; }
    }
}
