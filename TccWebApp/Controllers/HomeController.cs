using System;
using System.Linq;
using System.Web.Mvc;
using TccWebApp.Models;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace TccWebApp.Controllers
{
    public class HomeController : Controller
    {
        public static string stringResponse = "";
        public static string fileName = "audio.wav";
        public static string directory = @"C:\Users\output\";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormModel formModel)
        {
            int sourceOption = 0;
            if (String.IsNullOrEmpty(formModel.radioId))
            {
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg\output\");

                if (formModel.recordedTrack != null && formModel.recordedTrack.ContentLength > 0)
                {
                    //fileName = Path.GetFileName(formModel.recordedTrack.FileName);

                    //formModel.recordedTrack.SaveAs(Server.MapPath(Path.Combine(directory, fileName)));
                    //formModel.recordedTrack.SaveAs(Path.Combine(directory, fileName));
                    formModel.recordedTrack.SaveAs(Path.Combine(directory, fileName));
                }
            }
            else
            {
                //sourceOption = 1;

                Process process = new Process();
                process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg\bin\ffmpeg.exe");
                //process.StartInfo.Arguments = "-i http://antena1.newradio.it/stream -t 20 -f wav " + @"C:\Users\output\estranho.wav"; //+ Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg\output\") + "bizarro.wav";
                process.StartInfo.Arguments = "-y -i " + formModel.radioId + " -t 20 -f wav " + directory + fileName;
                process.StartInfo.Verb = "runas";
                //process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();

                System.IO.File.Copy(@"C:\Users\output\audio.wav", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg\output\audio.wav"), true);
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg\output\");
            }

            byte[] response;
            using (var client = new WebClient())
            {
                //response = client.UploadFile("http://35.202.190.115/speechmusic/" + formModel.algorithmId, "POST", @"C:\Users\alpha_2017_05_13__20_09_00.wav");
                response = client.UploadFile("http://35.202.190.115/speechmusic/" + formModel.algorithmId, "POST", directory + fileName);
                stringResponse = client.Encoding.GetString(response);
                stringResponse = stringResponse.Insert(1, "\"source\":" + sourceOption + ", ");
                Debug.WriteLine("################################");
                Debug.WriteLine(stringResponse);
                Debug.WriteLine("################################");
                Debug.WriteLine(directory + fileName);
            }

            return RedirectToAction("Result", "Home");
        }

        public ActionResult Result()
        {
            return View();
        }

        
        public ActionResult GetResult()
        {
            //stringResponse = "{ \"audio_file\": \"./files/alpha_2017_05_13__20_09_00.wav\", \"knn\": { \"sec_by_sec_music_prediction\": [1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1], \"likely_to_be_music\": 0.675}}";
            //stringResponse = "{ \"audio_file\": \"audio.wav\", \"knne\": { \"sec_by_sec_music_prediction\": [1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], \"likely_to_be_music\": 1.0}, \"nne\": {\"sec_by_sec_music_prediction\": [1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], \"likely_to_be_music\": 1.0}, \"svme\": {\"sec_by_sec_music_prediction\": [1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], \"likely_to_be_music\": 1.0}, \"voter\": {\"sec_by_sec_music_prediction\": [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1], \"likely_to_be_music\": 1.0}}";
            //JsonResult jsonResult = Json(JObject.Parse(stringResponse), JsonRequestBehavior.AllowGet);
            //return Json(JObject.Parse(stringResponse), JsonRequestBehavior.AllowGet);

            return Content(stringResponse, "application/json", Encoding.UTF8);
        }
    }
}