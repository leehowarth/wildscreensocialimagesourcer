using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WildscreenLib;

namespace WildscreenAnimalsAPI.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewBag.Title = "Best Images";

      var animals = new List<Animal>();
      var animalImages = new List<AnimalImage>();

      using (var context = new WildscreenAnimalsAPI_dbEntities())
      {
        animals = context.Animals.ToList();
        animalImages = context.AnimalImages.Where(x => x.Upvotes > 0 && x.Upvotes > x.Downvotes).OrderByDescending(x => x.Upvotes).Take(10).ToList();
      }

      //take data and take random selection 
      var animalData = animalImages.Select(x => new { Image = x, Animal = animals.Where(a => a.Id == x.Animal_Id).FirstOrDefault() }).OrderByDescending(x => x.Image.Upvotes).Take(10);

      return View(animalData);
    }
  }
}
