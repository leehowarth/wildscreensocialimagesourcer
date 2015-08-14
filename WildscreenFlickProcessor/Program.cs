using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WildscreenLib;

namespace WildscreenFlickProcessor
{
  class Program
  {
    private const string flickrKey = "3495a3735fabb8711ccca506627cdafb";
    private const string flickSecret = "1bfeca36e9048e41";
    private const string flickrSearchUrl = " https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&privacy_filter=1&content_type=1&page=1&per_page=500&format=json&nojsoncallback=1&sort=interestingness-desc";
    private const string flickrImageUrl = "https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg";
    static void Main(string[] args)
    {
      //open DB
      using (var context = new WildscreenAnimalsAPI_dbEntities())
      {
        var animals = context.Animals.ToList();//Get all animals
        var allAnimalImages = context.AnimalImages.ToList(); 

        foreach (var animal in animals)
        {
          //Do a flickr search on the animal 
          var json = SendRequest(string.Format(flickrSearchUrl, flickrKey, WebUtility.UrlEncode(animal.Name)));
          var searchResult = JsonConvert.DeserializeObject<FlickrSearchResponse>(json);
          //Loop through flikr images returned in the resuts and get image urls for them 
          if(searchResult.photos != null && searchResult.photos.photo.Count > 0)
          {
            foreach(var fPhoto in searchResult.photos.photo)
            {
              var photo = new AnimalImage()
              {
                Animal_Id = animal.Id,
                CreatedOn = DateTime.Now,
                Downvotes = 0,
                Upvotes = 0,
                SourceSystem = "Flickr",
                ImageUrl = string.Format(flickrImageUrl,fPhoto.farm,fPhoto.server,fPhoto.id,fPhoto.secret),
                Name = fPhoto.title
              };
              if(!allAnimalImages.Any(x => x.ImageUrl == photo.ImageUrl))
                context.AnimalImages.Add(photo); 
            }//foreach 
          }//if 
          //Create an image record
        }
        //Save
        context.SaveChanges(); 
      } //close db
    }

    static string SendRequest(string url)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      try
      {
        WebResponse response = request.GetResponse();
        using (Stream responseStream = response.GetResponseStream())
        {
          StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
          return reader.ReadToEnd();
        }
      }
      catch (WebException ex)
      {
        WebResponse errorResponse = ex.Response;
        using (Stream responseStream = errorResponse.GetResponseStream())
        {
          StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
          String errorText = reader.ReadToEnd();
          // log errorText
        }
        throw;
      }
    }
  }
}
