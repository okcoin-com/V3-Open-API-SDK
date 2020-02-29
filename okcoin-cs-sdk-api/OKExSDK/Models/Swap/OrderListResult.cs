using System;
using System.Collections.Generic;
using System.Text;

namespace OKCoinSDK.Models.Swap
{
    public class OrderListResult
    {
        public bool result { get; set; }
        public List<Order> order_info { get; set; }
    }
}
