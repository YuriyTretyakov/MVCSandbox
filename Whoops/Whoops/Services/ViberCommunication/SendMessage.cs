namespace Whoops.Services.ViberMessaging
{
    public class Sender
    {
        public string name { get; set; }
        public string avatar { get; set; }
    }

    public class SendMessageRequestModel
    {
        public string receiver { get; set; }
        public int min_api_version { get; set; }
        public Sender sender { get; set; }
        public string tracking_data { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }
}

