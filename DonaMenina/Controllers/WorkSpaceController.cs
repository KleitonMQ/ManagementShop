using DonaMenina.Context;
using DonaMenina.Entities;
using DonaMenina.Filters;
using DonaMenina.Helper;
using DonaMenina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using System.Globalization;

namespace DonaMenina.Controllers
{
    [PageForLoginUser]
    public class WorkSpaceController : Controller
    {
        private readonly DataBaseContext _dbContext;
        private readonly IUserSession _session;
        private readonly ISaleSession _saleSession;

        public WorkSpaceController(DataBaseContext context, IUserSession session, ISaleSession saleSession)
        {
            _dbContext = context;
            _session = session;
            _saleSession = saleSession;
        }

        [HttpPost]
        public IActionResult InsertWorker(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Workers.Add(worker);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Sucess), new {sucess = "Inserir Funcionario"});
            }
            return RedirectToAction(nameof(ManageWorker));
        }

        [HttpPost]
        public IActionResult EditWorker(Worker worker)
        {
            var workerDB = _dbContext.Workers.Find(worker.WorkerId);

            workerDB.Name = worker.Name;
            workerDB.Password = worker.Password;
            workerDB.IsActive = worker.IsActive;
            workerDB.IsAdm = worker.IsAdm;

            _dbContext.Workers.Update(workerDB);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Sucess), new {sucess = "Editar Funcionário" });
        }

        public IActionResult EditWorker(int Id)
        {
            var worker = _dbContext.Workers.Find(Id);
            if (worker == null) return NotFound();
            return View(worker);
        }

        public IActionResult ManageWorker()
        {
            var workers = _dbContext.Workers.ToList();
            return View(workers);
        }

        public ActionResult ManageProducts()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }
        public IActionResult Reports()
        {
            var mergeClass = _dbContext.MergeClasses
                .Include(mc => mc.Product)
                .Include(mc => mc.Sale)
                .Include(mc => mc.Worker)
                .ToList();

            return View(mergeClass);
        }

        [HttpPost]
        public IActionResult InsertProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Sucess), new {sucess = "Inserir Produto" });
        }

        public ActionResult EditProducts(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            var productDB = _dbContext.Products.Find(product.ProductId);

            productDB.ProductName = product.ProductName;
            productDB.Brand = product.Brand;
            productDB.Quantity = product.Quantity;
            productDB.Price = product.Price;
            

            _dbContext.Products.Update(productDB);
            _dbContext.SaveChanges();

            
            return RedirectToAction(nameof(Sucess), new {sucess = "Editar Produto" });
            
        }
        public ActionResult InsertProduct()
        {
            return View();
        }

        public IActionResult WorkSpace(string password)
        {
            var worker = _dbContext.Workers.FirstOrDefault(w => w.Password == password);
            WorkSpaceModel workSpaceModel = new WorkSpaceModel();
            workSpaceModel.Worker = worker;
            workSpaceModel.Products = _dbContext.Products.ToList();
            _session.BuildUserSession(workSpaceModel.Worker);
            workSpaceModel.ShoppingCart = new List<Product>();

            return View(workSpaceModel);
        }

        [HttpPost]
        public IActionResult ConfirmPurchase(ProductList products)
        {

            WorkSpaceModel workSpaceModel = new WorkSpaceModel();

            string[] arrayId = products.IdList.Split(" ");
            int[] intId = new int[arrayId.Length];

            for (int i = 0; i < arrayId.Length; i++)
            {
                intId[i] = int.Parse(arrayId[i]);
                workSpaceModel.IdProducts.Add(intId[i]);
                workSpaceModel.ShoppingCart.Add(_dbContext.Products.Find(intId[i]));
            }

            string[] arrayQuantity = products.QuantityList.Split(" ");
            int[] intQuantity = new int[arrayQuantity.Length];


            for (int i = 0; i < arrayQuantity.Length; i++)
            {
                intQuantity[i] = int.Parse(arrayQuantity[i]);
                workSpaceModel.QuantitySold.Add(intQuantity[i]);
                workSpaceModel.ShoppingCart[i].Quantity = intQuantity[i];
            }

            workSpaceModel.Worker = _session.GetWorkerSession();

            var lastIDSale = _dbContext.Sales.OrderByDescending(x => x.SaleId).Select(x => x.SaleId).FirstOrDefault();

            var nextIDSale = (lastIDSale != 0) ? lastIDSale + 1 : 1;

            _saleSession.BuildSaleSession(workSpaceModel);

            return View(workSpaceModel);
        }

        [HttpPost]
        public IActionResult SavePurchase(string discount, string paymentMethod, string totalSale)
        {
            var workSpace = _saleSession.GetSaleSession();

            if (string.IsNullOrEmpty(discount))
            {
                discount = "0";
            }
            workSpace.Sale.PriceDiscount = int.Parse(discount);
            workSpace.Sale.PaymentMethod = paymentMethod;
            workSpace.Sale.TotalSale = decimal.Parse(totalSale);
            workSpace.Sale.DataSale = DateTime.Now;

            _dbContext.Add(workSpace.Sale);
            _dbContext.SaveChanges();

            var lastIDSale = _dbContext.Sales.OrderByDescending(x => x.SaleId).Select(x => x.SaleId).FirstOrDefault();

            foreach (Product product in workSpace.ShoppingCart)
            {
                MergeClass mergeClass = new MergeClass();
                mergeClass.SaleId = lastIDSale;
                mergeClass.WorkerID = workSpace.Worker.WorkerId;
                mergeClass.ProductId = product.ProductId;
                mergeClass.QuantitySold = product.Quantity;

                Product productToUpdate = _dbContext.Products.Find(product.ProductId);
                productToUpdate.Quantity -= product.Quantity;

                _dbContext.Update(productToUpdate);
                _dbContext.Add(mergeClass);
            }

            _dbContext.SaveChanges();

            _saleSession.EndSaleSession();

            return RedirectToAction(nameof(Sucess), new {sucess = "Venda Realizada" });
        }

        public IActionResult Sucess(string sucess)
        {
            SucessModel sucessModel = new SucessModel(sucess);
            return View(sucessModel);
        }
    }
}