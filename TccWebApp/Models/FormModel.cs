using System.Web;

namespace TccWebApp.Models
{
    public class FormModel
    {
        public string radioId { get; set; }
        public string algorithmId { get; set; }
        public HttpPostedFileBase recordedTrack { get; set; }
    }
}