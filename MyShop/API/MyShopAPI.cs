using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Documents;

using static MyShop.Classes.Product;
using System.Collections;
using System.ComponentModel;

// https://stackoverflow.com/questions/42756950/unable-to-use-json-data-with-newtonsoft-json-in-wpf
// https://stackoverflow.com/questions/37894177/deserializing-json-object-throws-a-newtonsoft-json-jsonserializationexception
// https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

namespace MyShop.API
{
    public class RootObject
    {
        public List<Account> accountList { get; set; }
    }

    public class Account
    {
        [JsonProperty(PropertyName = "accountList")]
        public string uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string created { get; set; }
    }


    public class MyShopApi: INotifyPropertyChanged
    {
        public static string baseUrl = "http://localhost:5000/";

        public static List<Account> apiAccountList { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static async Task<String> GetAccountData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string jsonStrRes = await client.GetStringAsync($"{baseUrl}account");

            if (jsonStrRes != "")
            {
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};

                //var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

                //foreach (var acccount in res.accountList)
                //{
                //    Debug.WriteLine(acccount.username);
                //}

                return jsonStrRes;
            }

            return "";
        }
    }
}
