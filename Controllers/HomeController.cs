using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SilverlineSolutionsInterviewApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseAccess DatabaseAccess = new DatabaseAccess();
        private readonly List<string> AllowedExtensions = new List<string> { "image/jpeg", "image/png", "image/tiff", "image/webp", "image/bmp" };

        [HttpGet]
        public ActionResult Index()
        {
            return View(DatabaseAccess.GetAllRecipes());
        }

        [HttpPost]
        public ActionResult AddNewRecipe(string Name, HttpPostedFileBase Image, string Ingredients)
        {
            if (AllowedExtensions.Contains(Image.ContentType))
            {
                //save the image to the folder
                Image.SaveAs(Path.Combine(Server.MapPath("/Content/Images/"), Image.FileName));
                //add the recipe in the database
                if (DatabaseAccess.AddRecipe(Name, Image.FileName, Ingredients))
                    return View("Index", DatabaseAccess.GetAllRecipes());
            }
            return Json("Failed to add the new recipe. Please try again.");
        }

        [HttpPost]
        public ActionResult EditRecipe(int ID, string Name, string Ingredients, HttpPostedFileBase Image = null)
        {
            if (Image != null)//save the image to the folder
                Image.SaveAs(Path.Combine(Server.MapPath("/Content/Images/"), Image.FileName));
            if (DatabaseAccess.EditRecipe(ID, Name, Ingredients, Image != null ? Image.FileName : string.Empty))
                return View("Index", DatabaseAccess.GetAllRecipes());
            return Json("Failed to edit the recipe. Please try again.");
        }

        [HttpPost]
        public ActionResult DeleteRecipe(int ID)
        {
            if (DatabaseAccess.DeleteRecipe(ID))
                return View("Index", DatabaseAccess.GetAllRecipes());
            return Json("Failed to delete the recipe. Please try again.");
        }
    }
}