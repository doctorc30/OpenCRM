using System;
using System.Security.Cryptography;
using System.Text;

namespace bbom.Admin.Core.Services.PaySystemService
{
    public class YandexMoneyService : IPaySystemService
    {
        public bool ProcessingRespons(IPaySystemOptions options, string key)
        {
            var moneyOptions = options as YandexMoneyOptions;
            if (moneyOptions != null)
            {
                string paramString =
                    $"{moneyOptions.notification_type}" +
                    $"&{moneyOptions.operation_id}" +
                    $"&{moneyOptions.amount}" +
                    $"&{moneyOptions.currency}" +
                    $"&{moneyOptions.datetime}" +
                    $"&{moneyOptions.sender}&{moneyOptions.codepro.ToLower()}" +
                    $"&{key}" +
                    $"&{moneyOptions.label}";
                string paramStringHash1 = GetHash(paramString);
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                return 0 == comparer.Compare(paramStringHash1, moneyOptions.sha1_hash);
            }
            return false;
        }

        public string GetHash(string val)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}