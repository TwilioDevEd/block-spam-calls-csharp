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
        private const int WhitepagesBadReputation = 4;
        private const int NomoroboSpamScore = 1;
        private const string SuccessfulStatus = "successful";

        [HttpPost]
        public TwiMLResult Index(VoiceRequest request, string addOns)
        {
            var response = new VoiceResponse();
            var isCallBlocked = false;

            if (!string.IsNullOrWhiteSpace(addOns))
            {
                Trace.WriteLine(addOns);

                var addOnData = JObject.Parse(addOns);
                if (addOnData["status"]?.ToString() == "successful")
                {
                    isCallBlocked = IsBlockedByNomorobo(addOnData["results"]?["nomorobo_spamscore"])
                                 || IsBlockedByWhitepages(addOnData["results"]?["whitepages_pro_phone_rep"])
                                 || IsBlockedByMarchex(addOnData["results"]?["marchex_cleancall"]);
                }
            }

            if (isCallBlocked)
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

        private static bool IsBlockedByNomorobo(JToken nomorobo)
        {
            if (nomorobo?["status"]?.Value<string>() != SuccessfulStatus) return false;

            var score = nomorobo["result"]?["score"]?.Value<int>();
            return score == NomoroboSpamScore;
        }

        private static bool IsBlockedByWhitepages(JToken whitepages)
        {
            if (whitepages?["status"]?.Value<string>() != SuccessfulStatus) return false;

            var reputationLevel = whitepages["result"]?["reputation_level"].Value<int>();
            return reputationLevel == WhitepagesBadReputation;
        }

        private static bool IsBlockedByMarchex(JToken marchex)
        {
            if (marchex?["status"]?.Value<string>() != SuccessfulStatus) return false;

            var recommendation = marchex["result"]?["result"]?["recommendation"]?.Value<string>();
            return recommendation == "BLOCK";
        }
    }
}