using ETicaret.Entity;
using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        SearchModel viewModel = null;
        // GET: Home
        public ActionResult Index()
        {
            var urunler = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    CategoryId = i.CategoryId
                }).ToList();
            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            var kat = _context.Products.Where(i => i.Id == id).Select(l => l.CategoryId).FirstOrDefault();
       
            var taksit = _context.Taksits.Where(p => p.CategoryId == kat).Select(m => new CreditCategory()
            {  CartName = m.Credit.CartName, Taksit = m.TaksitSayisi }).ToList();

            ViewBag.data = taksit;
         

            return View(_context.Products.Where(i => i.Id == id).FirstOrDefault()); }
     

        public ActionResult List(int ? id)
        {
            var urunler = _context.Products
                .Where(i => i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId
                }).AsQueryable();
            
            if (id != null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }

            return View(urunler.ToList());
        }
        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }

        [HttpPost]
        public PartialViewResult Search(string searchKey)
        {

            IEnumerable<ProductModel> i = new List<ProductModel>();
            viewModel = new SearchModel();

            i = GetSeachResult(searchKey);
           

            return this.PartialView(i);



        }

        private IEnumerable<ProductModel> GetSeachResult(string search)
        {
            DataContext db = new DataContext();
            SearchModel sm = new SearchModel();

            var urun = db.Products.Where(cus => cus.Name.Contains(search)).Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                Image = i.Image ?? "1.jpg",
                CategoryId = i.CategoryId
            }).AsQueryable();


            return urun;
        }
    }
}