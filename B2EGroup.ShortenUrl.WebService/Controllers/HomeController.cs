using B2EGroup.ShortenUrl.Models;
using System.Linq;
using System.Web.Mvc;

namespace B2EGroup.ShortenUrl.WebService.Controllers
{
    public class HomeController : Controller
    {
        private ShortenUrlContext db = new ShortenUrlContext();

        public ActionResult Index(string id="")
        {
            if (!string.IsNullOrEmpty(id))
            {
                Url url = db.Urls.FirstOrDefault(u => u.ShortUrl == id);

                if (url != null)
                    return Redirect(url.LongUrl);
            }

            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ClearAllTables()
        {
            TruncateAllTables();

            return RedirectToAction("Index","Infos");
        }

        public void TruncateAllTables()
        {
            using (var _context = new ShortenUrlContext())
            {
                _context.Database.Connection.Open();
                var sqlCommand = _context.Database.Connection.CreateCommand();

                string[] table = new string[]{ "Urls","Users"};

                for (int t = 0; t < table.Count(); t++)
                {
                    sqlCommand.CommandText = $"TRUNCATE TABLE {table[t]}; ";
                    sqlCommand.ExecuteNonQuery();
                }

                _context.Database.Connection.Close();
            }
        }
    }
}
