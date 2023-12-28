using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ISupportWeb.Models.BLL.checkoutBLL;

namespace ISupportWeb.Models.Service
{
    public class checkoutService
    {
        checkoutBLL bll;
        public RspOrderPunch OrderPunch(OrderMasterBLL obj)
        {
            var dsc = 0;
            RspOrderPunch rsp = new RspOrderPunch();
            try
            {
                var currDate = DateTime.UtcNow;
                Random random = new Random();
                obj.TransactionNo = "IT-" + random.Next(10, 10000) + random.Next(1, 1000);
                obj.OrderNo = obj.OrderNo = "IT-" + currDate.Date.ToString("ddMMyy") + "-" + random.Next(1, 100) + random.Next(1, 100);
                obj.OrderDate = DateTime.UtcNow.Date;
                obj.CreationDate = DateTime.UtcNow;
                obj.StatusID = 101;
                dsc = bll.Insert(obj);

                if (dsc > 0)
                {
                    rsp.status = 1;
                    rsp.description = "Order has been punched successfully";
                }
                else
                {
                    rsp.status = 0;
                    rsp.description = "Order is failed to be punched";
                }
            }
            catch (Exception ex)
            {
                rsp.status = 0;
                rsp.description = ex.Message;
            }
            return rsp;
        }
    }
}