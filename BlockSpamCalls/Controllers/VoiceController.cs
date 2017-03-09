using System.Diagnostics;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace BlockSpamCalls.Controllers
{
    public class VoiceController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index(VoiceRequest request, string addOns)
        {
            var response = new VoiceResponse();
            var blockCall = false;

            if (!string.IsNullOrWhiteSpace(addOns))
            {
                Trace.WriteLine(addOns);

                var addOnData = JObject.Parse(addOns);
                if (addOnData["status"]?.ToString() == "successful")
                {
                    blockCall = ShouldBeBlockedByNomoRobo(addOnData)
                             || ShouldBeBlockedByWhitePages(addOnData)
                             || ShouldBeBlockedByMarchex(addOnData);
                }
            }

            if (blockCall)
            {
                response.Reject();
            }
            else
            {
                response.Say("Welcome to the jungle.");
                response.Hangup();
            }
            return TwiML(response);
        }

        private static bool ShouldBeBlockedByNomoRobo(JObject addOnData)
        {
            var nomorobo = addOnData["results"]?["nomorobo_spamscore"];
            if (nomorobo?["status"]?.ToString() != "successful") return false;

            var score = nomorobo["result"]?["score"]?.Value<int>();
            return score == 1;
        }

        private static bool ShouldBeBlockedByWhitePages(JObject addOnData)
        {
            var whitePages = addOnData["results"]?["whitepages_pro_phone_rep"];
            if (whitePages?["status"]?.ToString() != "successful") return false;

            var results = whitePages["result"]?["results"];
            if (results == null) return false;

            foreach (var result in results)
            {
                var level = result["reputation"]?["level"]?.Value<int>();
                if (level == 4) return true;
            }

            return false;
        }

        private static bool ShouldBeBlockedByMarchex(JObject addOnData)
        {
            var cleanCall = addOnData["results"]?["marchex_cleancall"];
            if (cleanCall?["status"]?.ToString() != "successful") return false;

            var recommendation =
                cleanCall["result"]?["result"]?["recommendation"]?.ToString();
            return recommendation == "BLOCK";
        }
    }
}