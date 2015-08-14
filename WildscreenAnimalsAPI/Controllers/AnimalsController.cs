using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WildscreenLib;

namespace WildscreenAnimalsAPI.Controllers
{

    public class AnimalsController : ApiController
    {
        // GET api/Animals
        public IEnumerable<dynamic> GetRandom(int count = 10)
        {
          var animals = new List<Animal>();
          var animalImages = new List<AnimalImage>();
          using(var context = new WildscreenAnimalsAPI_dbEntities())
          {
            animals = context.Animals.ToList();
            animalImages = context.AnimalImages.ToList();
          }
          //take data and take random selection 
          var animalData = animalImages.Select(x => new { Image = x, Animal = animals.Where(a => a.Id == x.Animal_Id).FirstOrDefault() }).OrderBy(elem => Guid.NewGuid()).Take(count);

          return animalData; 
        }

        //api/Upvote
        [ActionName("Upvote")]
       [HttpGet, HttpPost]
        public bool Upvote(int id)
        {
          //Upvote an animal 
          using (var context = new WildscreenAnimalsAPI_dbEntities())
          {
            var image = context.AnimalImages.Where(x => x.Id == id).FirstOrDefault();
            image.Upvotes++;
            context.SaveChanges();
          }
          return true; 
        }

       [ActionName("Downvote")]
       [HttpGet, HttpPost]
        public bool Downvote(int id)
        {
          //Downvote an animal 
          //Upvote an animal 
          using (var context = new WildscreenAnimalsAPI_dbEntities())
          {
            var image = context.AnimalImages.Where(x => x.Id == id).FirstOrDefault();
            image.Downvotes++;
            context.SaveChanges();
          }
          return true; 
        }
    }
}
