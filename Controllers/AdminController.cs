using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using InventoryManagementApp.Models;
namespace InventoryManagementApp.Controllers
{
    public class AdminController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var loggedInAdmin = db.Admins
            .FirstOrDefault(a => a.Username.Trim().ToLower() == admin.Username.Trim().ToLower() && a.Password.Trim() == admin.Password.Trim());
            if (loggedInAdmin != null)
            {
                return RedirectToAction("ProductList", "Product");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }
    }
}