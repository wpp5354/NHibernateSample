﻿using NHibernate.Domain.Entities;
using NHibernateSample;

namespace NHibernate.Business
{
    public class OrderBusiness
    {
        private OrderData _orderData;
        public OrderBusiness()
        {
            _orderData = new OrderData();
        }
        public bool AddOrder(Orders order)
        {
            return _orderData.AddOrder(order);
        }
    }
}
