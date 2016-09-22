using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestDemo
{
public static class HttpTestClass
{

// Basic authentication example.
// TODO : If you don't need List for authentication you might disable returning result or return string for example.
// TODO : If you don't need platform specific HttpClient, remove HttpClient from parameter.
public static async Task<List<TestModel>> basicAuthTask(HttpClient httpClient, string userName, string userPassword, string apiUrl)
{

	// Creating a new List object to prevent null object reference error.
	var testModelList = new List<TestModel>();
	
	using (httpClient)
	{
		// Setting default request authorization
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(convertStringtoByteArray(userName, userPassword)));

		// Setting http method and creating a new http request message
		using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl))
		 {
			// TODO :  Enter your own api key and key value 
			httpRequestMessage.Headers.Add("YourApiKey", "YourApiKeyValue");

			// Sending request message and setting configure await as false to run code in background
			using (var httpResponse = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false))
			 {
				// Reading result and setting configure await as false to run code in background
				string returnValue = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

				// Converting string result to json object and setting configure await  as false to run code in background
				var jObject = await Task.Run(() => JObject.Parse(returnValue)).ConfigureAwait(false);

				// Deserializing json object and setting configure await  as false to run code in background by running code in Task
				var jsonObject = await Task.Run(() => JsonConvert.DeserializeObject<TestJsonClass.Example>(jObject.ToString())).ConfigureAwait(false);

				// Filling list with each object within result
				foreach (var user in jsonObject.SessionResponse.User)
				{
					testModelList.Add(new TestModel
					{
						userName = user.first_name,
						userLastName = user.last_name,
						httpStatus = jsonObject.SessionResponse.Return.Status,
						httpMessage = jsonObject.SessionResponse.Return.Message
					});
				}
			}
		}
	}

	// Returning final list
	return testModelList;
}


//Token based authentication example. 
// TODO : Token must be saved after basic authentication.
public static async Task NoAuthTask(string apiToken,string apiUrl)
{
	using (var httpClient = new HttpClient())
	{
		// Post method can be changed to GET and PUT
		using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl))
		{
			// TODO : Enter your own api key and key value
			httpRequestMessage.Headers.Add("ApiKey", "KeyValue");
			httpRequestMessage.Headers.Add("ApiToken", apiToken);

			// TODO : Optional content for PUT and POST methods, should be removed for GET method.
			var content = new FormUrlEncodedContent(new[]
				{
				new KeyValuePair<string, string>("key1", "value1"),
				new KeyValuePair<string, string>("key2", "value2")
			});

			// TODO : Optional setup for PUT and POST methods, should be removed for GET method.
			httpRequestMessage.Content = content;

			// Sending Request
			using (var httpResponse = await httpClient.SendAsync(httpRequestMessage))
			{
				// Reading result
				string readHttpResponse = await httpResponse.Content.ReadAsStringAsync();

				// TODO : Use same steps within basic authentication to be able to work with Json data
			}
		}
	}
}


// Helper method for basic authentication. 
public static byte[] convertStringtoByteArray(string userName, string userPassword)
{
	var byteArray = Encoding.UTF8.GetBytes(userName + ":" + userPassword);
	return byteArray;
}


}
}


