namespace bbom.Admin.Core.Domain
{
    public class RouteRedirectRule
    {
        public string TargetAction { get; set; } 
        public string TargetController{ get; set; }
        public string RedirectAction { get; set; }
        public string RedirectController { get; set; }
        public string Role{ get; set; } 

    }
}