using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolutionName.DataLayer;
using SolutionName.Model;
using SolutionName.Web.ViewModels;

namespace SolutionName.Web.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;

        public SalesController()
        {
            _salesContext = new SalesContext();
        }

        public ActionResult Index()
        {
            return View(_salesContext.SalesOrders.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel _salesOrderViewModel = new SalesOrderViewModel();
            _salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            _salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            _salesOrderViewModel.PONumber = salesOrder.PONumber;
            _salesOrderViewModel.MessageToClient = "Hello View Model";

            return View(_salesOrderViewModel);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            SalesOrderViewModel _salesOrderViewModel = new SalesOrderViewModel();
            _salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(_salesOrderViewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel _salesOrderViewModel = new SalesOrderViewModel();
            _salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            _salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            _salesOrderViewModel.PONumber = salesOrder.PONumber;
            _salesOrderViewModel.MessageToClient = "Hello View Model";
            _salesOrderViewModel.ObjectState = ObjectState.Unchanged;


            return View(_salesOrderViewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel _salesOrderViewModel = new SalesOrderViewModel();
            _salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            _salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            _salesOrderViewModel.PONumber = salesOrder.PONumber;
            _salesOrderViewModel.MessageToClient = "Hello View Model";
            _salesOrderViewModel.ObjectState = ObjectState.Deleted;

            return View(_salesOrderViewModel);

        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult Save(SalesOrderViewModel _salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.SalesOrderId = _salesOrderViewModel.SalesOrderId;
            salesOrder.CustomerName = _salesOrderViewModel.CustomerName;
            salesOrder.PONumber = _salesOrderViewModel.PONumber;
            salesOrder.ObjectState = _salesOrderViewModel.ObjectState;
                        
            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = Helper.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Sales/Index/" });

            _salesOrderViewModel.MessageToClient = string.Format("{0}'s sales order has been added", salesOrder.CustomerName);

            _salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            _salesOrderViewModel.ObjectState = ObjectState.Unchanged;
            return Json(new { _salesOrderViewModel });
        }
    }
}
