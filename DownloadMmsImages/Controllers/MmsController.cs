using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Win32;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Task = System.Threading.Tasks.Task;

namespace DownloadMmsImages.Controllers
{
    public class MmsController : TwilioController
    {
        private const string SavePath = "~/App_Data/";

        [HttpPost]
        public async Task<TwiMLResult> Index(SmsRequest request, int numMedia)
        {
            for (var i = 0; i < numMedia; i++)
            {
                var mediaUrl = Request.Form[$"MediaUrl{i}"];
                Trace.WriteLine(mediaUrl);
                var contentType = Request.Form[$"MediaContentType{i}"];

                var filePath = GetMediaFileName(mediaUrl, contentType);
                await DownloadUrlToFileAsync(mediaUrl, filePath);
            }

            var response = new MessagingResponse();
            var body = numMedia == 0 ? "Send us an image!" : 
                $"Thanks for sending us {numMedia} file(s)!";
            response.Message(body);
            return TwiML(response);
        }

        private string GetMediaFileName(string mediaUrl, 
            string contentType)
        {
            return Server.MapPath(
                // e.g. ~/App_Data/MExxxx.jpg
                SavePath +
                Path.GetFileName(mediaUrl) +
                GetDefaultExtension(contentType)
            );
        }

        private static async Task DownloadUrlToFileAsync(string mediaUrl, 
            string filePath)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(mediaUrl);
                var httpStream = await response.Content.ReadAsStreamAsync();
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    await httpStream.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
            }
        }

        public static string GetDefaultExtension(string mimeType)
        {
            // NOTE: This implementation is Windows specific (uses Registry)
            // Platform independent way might be to download a known list of
            // mime type mappings like: http://bit.ly/2gJYKO0
            var key = Registry.ClassesRoot.OpenSubKey(
                @"MIME\Database\Content Type\" + mimeType, false);
            var ext = key?.GetValue("Extension", null)?.ToString();
            return ext ?? "application/octet-stream";
        }
    }
}