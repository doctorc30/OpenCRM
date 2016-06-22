namespace bbom.Admin.Core.Services.PaySystemService
{
    public class YandexMoneyOptions : IPaySystemOptions
    {
        public string notification_type { get; set; }
        public string operation_id { get; set; }
        public string amount { get; set; }
        public string withdraw_amount { get; set; }
        public string currency { get; set; }
        public string datetime { get; set; }
        public string sender { get; set; }
        public string codepro { get; set; }
        public string label { get; set; }
        public string sha1_hash { get; set; }
        public string test_notification { get; set; }
        public string unaccepted { get; set; }
    }
}