namespace bbom.Admin.Core.Services.PaySystemService
{
    public interface IPaySystemService
    {
        bool ProcessingRespons(IPaySystemOptions options, string key);
    }
}