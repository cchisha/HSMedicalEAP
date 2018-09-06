using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthSourceMedicalApp.Common
{

    public class RestService
    {

        HttpClient client;
        String BaseURL = "http://eapmobile.healthsource.co.za";
        public RestService()
        {
            client = new HttpClient();
            //client.BaseAddress = new Uri("http://eapmobile.healthsource.co.za");
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<String> RegisterUser(JObject user)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("name", (String)user.GetValue("firstname")),
                    new KeyValuePair<string, string>("surname", (String)user.GetValue("firstname")),
                    new KeyValuePair<string, string>("idno", (String)user.GetValue("idnumber")),
                    new KeyValuePair<string, string>("dob", (String)user.GetValue("dob")),
                    new KeyValuePair<string, string>("phone", (String)user.GetValue("phonenumber")),
                    new KeyValuePair<string, string>("email", (String)user.GetValue("email")),
                    new KeyValuePair<string, string>("sex", (String)user.GetValue("gender")),
                    new KeyValuePair<string, string>("player_id", (String)user.GetValue("player_id")),
                    new KeyValuePair<string, string>("push_token", (String)user.GetValue("push_token")),
                    new KeyValuePair<string, string>("action", (String)user.GetValue("action"))

                });
                Console.WriteLine("API Request Params:........................" + user.ToString());
                var uri = new Uri(string.Format(BaseURL + "/admin/api/register.php", string.Empty));
                Console.WriteLine("Sending Request:........................" + uri.ToString());
                var response = await client.PostAsync(uri, formContent);
                Console.WriteLine("Receiving Response:........................" + response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "";
            }
        }
        public async Task<String> PostJSONRequest(String paramz)
        {
        // RestUrl = http://developer.xamarin.com:8081/api/todoitems/
        //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        //string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}";
        //http://196.46.196.39/rest_v1/api/memberapi/check_member_exists?ssn=100880183
            var content = new StringContent(paramz, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/foo/login", content);
            //HttpResponseMessage response = await client.GetAsync("/foo/login", content);
            if (response.IsSuccessStatusCode)
            {
                var tokenJson = await response.Content.ReadAsStringAsync();
                //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                return tokenJson;
            }
            else
            {
                return "";
            }
        }
        async Task<string> PostRequest(string URL)
        {
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("somekey", "1"),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            var json = await response.Content.ReadAsStringAsync();

            return json;
            //Events result = JsonConvert.DeserializeObject<Events>(json);
        }
        /*
         * 
        public async Task<String> RegisterUser(JObject user)
        {
            //RestUrl = http://developer.xamarin.com:8081/api/todoitems/
            //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            //string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}";
            //http://196.46.196.39/rest_v1/api/memberapi/check_member_exists?ssn=100880183
            //var content = new StringContent(paramz, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync("/foo/login", content);
            //HttpResponseMessage response = await client.GetAsync("/foo/login", content);
            //http://196.46.196.39/rest_v1/api/employerapi/employer_session?msisdn=260966157164

            try
            {
                Console.WriteLine("API Request Params:........................" + user.ToString());
                var uri = new Uri(string.Format(BaseURL + "/admin/api/register.php", string.Empty));
                Console.WriteLine("Sending Request:........................" + uri.ToString());
                var response = await client.GetAsync(uri);
                //{ "success":1,"message":"Registration succesfull"}
                Console.WriteLine("Receiving Response:........................" + response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ///Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                    return content;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "";
            }
        }
         * 
         * 
         * 
// E.g. a JSON string.
HttpStringContent stringContent = new HttpStringContent(
    "{ \"firstName\": \"John\" }",
    UnicodeEncoding.Utf8,
    "application/json");

HttpClient client = new HttpClient();
HttpResponseMessage response = await client.PostAsync(
    uri,
    stringContent);
         * 
         * 
         * 
async void PostRequest(string URL)
    {
        var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("somekey", "1"), 
            });

        var myHttpClient = new HttpClient();
        var response = await myHttpClient.PostAsync(URL, formContent);

        var json = await response.Content.ReadAsStringAsync();
        Events result = JsonConvert.DeserializeObject<Events>(json);
    }
         * 
         * 
         * 
         * 
async void PostRequest(string URL)
    {
        var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("somekey", "1"), 
            });

        var myHttpClient = new HttpClient();
        var response = await myHttpClient.PostAsync(URL, formContent);

        var json = await response.Content.ReadAsStringAsync();
        Events result = JsonConvert.DeserializeObject<Events>(json);
    }
         * 
         * 
         * 
         * 
IInputStream inputStream = await file.OpenAsync(FileAccessMode.Read);
HttpMultipartFormDataContent multipartContent =
    new HttpMultipartFormDataContent();
multipartContent.Add(
    new HttpStreamContent(inputStream),
    "myFile",
    file.Name);
multipartContent.Add(
    new HttpStringContent("Hello World"),
    "myText");
 
HttpClient client = new HttpClient();
HttpResponseMessage response = await client.PostAsync(
    uri,
    multipartContent);
         * 
         * 
         * 
         * 
Dictionary<string, string> pairs = new Dictionary<string,string>();
pairs.Add("Nameob");
pairs.Add("Age8");
pairs.Add("Genderale");
HttpFormUrlEncodedContent formContent =
    new HttpFormUrlEncodedContent(pairs);
 
HttpClient client = new HttpClient();
HttpResponseMessage response = await client.PostAsync(uri, formContent);
         * 
         * 
         * 
         * public async Task<string> GettRequest()
        {
            var client = new HttpClient();

            //JArray array = new JArray();
            //array.Add("Manual text");
            //array.Add(new DateTime(2000, 5, 23));
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems/

            //string json = JsonConvert.SerializeObject(product);
           var content = new StringContent(JsonConvert.SerializeObject(new { username = "myusername", password = "mypass" }));
            var result = await client.PostAsync("localhost:8080", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var tokenJson = await result.Content.ReadAsStringAsync();
                return tokenJson;
            }
            else {
               return "";
            }
        }
         public async Task<Boolean>  RegiaterUsers()
         {
             client.BaseAddress = new Uri("localhost:8080");
             string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}";
             var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
             HttpResponseMessage response = await client.PostAsync("/foo/login", content);
             // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
             var result = await response.Content.ReadAsStringAsync();


             var content = new StringContent(JsonConvert.SerializeObject(new { username = "myusername", password = "mypass" }));
             var result = await client.PostAsync("localhost:8080", content).ConfigureAwait(false);
             if (result.IsSuccessStatusCode)
             {
                 var tokenJson = await result.Content.ReadAsStringAsync();
             }


             var URL = "http://developer.xamarin.com:8081/api/todoitems/";
             var post = new Post { Title = "Title " + DateTime.Now.Ticks };
             var content = JsonConvert.SerializeObject(post);
             await client.PostAsync(URL, new StringContent(content));
             return true;
         }

        public async Task<int> AddTodoItemAsync(TodoItem itemToAdd)
        {
            var data = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5000/api/todo/item", content);
            var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            return result;
        }



        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems/
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"                TodoItem successfully saved.");

            }

        }*/
    }
}
