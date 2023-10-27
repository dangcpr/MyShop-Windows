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
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Net.Http.Json;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;

// https://stackoverflow.com/questions/42756950/unable-to-use-json-data-with-newtonsoft-json-in-wpf
// https://stackoverflow.com/questions/37894177/deserializing-json-object-throws-a-newtonsoft-json-jsonserializationexception
// https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

namespace MyShop.API
{
    public class RootObject
    {
        public List<Account> accountList { get; set; }

        public List<Customer> customerList { get; set; }

        public List<CustomerQuery> customers { get; set; }

    }

    public class ResponseType
    {
        public CustomerType Customer { get; set; }
    }

    public class CustomerType
    {
        public string Customer_id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
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

    public class Customer
    {
        [JsonProperty(PropertyName = "customerList")]
        public int customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public DateTime create_at { get; set; }
        public DateTime modify_at { get; set; }
    }

    public class CustomerQuery
    {
        [JsonProperty(PropertyName = "customers")]
        public int customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public DateTime create_at { get; set; }
        public DateTime modify_at { get; set; }

    }

    public class MyShopApi: INotifyPropertyChanged
    {
        public static string baseUrl = "http://localhost:5000/";
        public static string graphqlEndpoint = "http://localhost:4000/graphql";

        public static List<Account> apiAccountList { get; set; }

        public static List<Customer> apiCustomerList { get; set; }

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

        public static async Task<String> GetCustomerData()
        {
            HttpClient client = new HttpClient();

            // Http setting
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string jsonStrRes = await client.GetStringAsync($"{baseUrl}customer");

            if (jsonStrRes != "") return jsonStrRes;

            return "";
        }

        public static async Task<String> GetCustomerQueryData()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(graphqlEndpoint)
            };

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var queryObject = new
            {
                query = @"query CustomersQuery {
                          customers {
                            customer_id
                            name
                            address
                            phone
                            create_at
                            modify_at
                          }
                        }",
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json")
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                string resSort = "";

                for(var i = 8; i < responseString.Length - 2; ++i)
                {
                    resSort += responseString[i];
                }

                return resSort;
            }

            return "";
        }
    }
}
