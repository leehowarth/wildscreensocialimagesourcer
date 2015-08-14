using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildscreenFlickProcessor
{

  public class Photo
  {
    public string id { get; set; }
    public string owner { get; set; }
    public string secret { get; set; }
    public string server { get; set; }
    public int farm { get; set; }
    public string title { get; set; }
    public int ispublic { get; set; }
    public int isfriend { get; set; }
    public int isfamily { get; set; }
  }

  public class Photos
  {
    public int page { get; set; }
    public int pages { get; set; }
    public string perpage { get; set; }
    public string total { get; set; }
    public List<Photo> photo { get; set; }
  }

  public class FlickrSearchResponse
  {
    public Photos photos { get; set; }
    public string stat { get; set; }
  }
}
