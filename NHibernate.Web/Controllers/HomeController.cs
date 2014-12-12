using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Domain.Entities;
using NHibernate.Business;
using System.Web.Helpers;

namespace NHibernate.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AddCustomer()
        {
            var customer = new Customer() { Id = Guid.NewGuid(), NameAddress = new Name() { FirstName = "aaa", CustomerAddress = "bbb" }, LastName = "wp" };
            customer.orders = new HashSet<Orders>();
            customer.orders.Add(new Orders() { OrderId = Guid.NewGuid(), OrderDate = DateTime.Now, customer = customer });
            customer.orders.Add(new Orders() { OrderId = Guid.NewGuid(), OrderDate = DateTime.Now, customer = customer });

            CustomerBusiness customerBusiness = new CustomerBusiness();
            if (customerBusiness.AddCustomer(customer))
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Search()
        {
            CustomerBusiness customerBusiness = new CustomerBusiness();

            customerBusiness.GetCustomerList(x => x.Id != null);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test()
        {
            Guid guidCustomerId = new Guid("90E47048-2753-4297-82F0-43E51CA54155");
            //模拟第一个修改数据
            Customer c1 = new Customer()
            {
                Id = guidCustomerId,
                NameAddress = { FirstName = "aaa", CustomerAddress = "address" },
                LastName = "wp",
                Version = 1
            };
            //模拟第二个修改数据
            Customer c2 = new Customer()
            {
                Id = guidCustomerId,
                NameAddress = { FirstName = "aaa", CustomerAddress = "address" },
                LastName = "wp",
                Version = 1
            };

            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            customerBusiness.SaveOrUpdateByTrans(c1);
            customerBusiness.SaveOrUpdateByTrans(c2);
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrder()
        {
            CustomerBusiness customerBusiness = new CustomerBusiness();
            OrderBusiness orderBusiness = new OrderBusiness();
            Guid cid = new Guid("90E47048-2753-4297-82F0-43E51CA54155");

            var order = new Orders() { customer = customerBusiness.GetCustomerList(x => x.Id == cid).FirstOrDefault(), OrderDate = DateTime.Now, OrderId = Guid.NewGuid() };

            try
            {
                orderBusiness.AddOrder(order);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelCustomer()
        {
            try
            {
                CustomerBusiness customerBusiness = new CustomerBusiness();
                var cid = Guid.Parse("90E47048-2753-4297-82F0-43E51CA54155");
                var customer = customerBusiness.GetCustomerList(x => x.Id == cid).FirstOrDefault();
                customerBusiness.DelCustomer(customer);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
