using System.Net.Http;
using System.Threading.Tasks;
using Whoops.Services.ViberCommunication;

namespace Whoops.Services
{
    public class ViberMessanger
    {
        public HttpClient Client { private set; get; }

        public ViberMessanger()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "48ea44919867d7c3-f8be6436807a7265-e6d506cca079548c");
            SetWebHook().Wait();
        }

        public async Task PostMessage(string message, string phone)
        {

            var postModel = new ViberMessaging.SendMessageRequestModel
            {
                receiver = phone,
                min_api_version = 1,
                sender = new ViberMessaging.Sender
                {
                    name = "Some Fun website"
                },
                tracking_data = "tracking data",
                type = "text",
                text = message
            };
            var response = await Client.PostAsJsonAsync("https://chatapi.viber.com/pa/send_message", postModel);
        }

        public async Task  SetWebHook()
        {
            var wh = new WebHookRequest
            {
                url = "google.com",
                event_types = {"delivered",
                                  "seen",
                                  "failed",
                                  "subscribed",
                                  "unsubscribed",
                                  "conversation_started"},
                send_name = true,
                send_photo = true
            };

            var response= await Client.PostAsJsonAsync("https://chatapi.viber.com/pa/set_webhook", wh);
        }
    }
    
}
