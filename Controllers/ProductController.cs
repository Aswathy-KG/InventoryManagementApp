using InventoryManagementApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementApp.Controllers
{
    public class ProductController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        public ActionResult ProductList()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult AddOrEdit(int? id)
        {
            Product product = id == null ? new Product() : db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(imageFile.InputStream))
                        {
                            product.ProductImage = reader.ReadBytes(imageFile.ContentLength);
                        }
                    }

                    db.Products.Add(product); 
                }
                else
                {

                    var existingProduct = db.Products.Find(product.ProductId);

                    if (existingProduct != null)
                    {

                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            using (var reader = new System.IO.BinaryReader(imageFile.InputStream))
                            {
                                existingProduct.ProductImage = reader.ReadBytes(imageFile.ContentLength);
                            }
                        }

                        existingProduct.ProductName = product.ProductName;
                        existingProduct.Price = product.Price;

                        db.Entry(existingProduct).State = EntityState.Modified; 
                    }
                }

                db.SaveChanges(); 
                return RedirectToAction("ProductList");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductId)
        {
            var product = db.Products.Find(ProductId);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("ProductList");
        }


    }
}