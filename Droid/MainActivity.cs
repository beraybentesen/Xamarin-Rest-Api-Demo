using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net.Http;
using Xamarin.Android.Net;
using System.Collections.Generic;

namespace RestDemo.Droid
{
	[Activity(Label = "RestDemo", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += (sender, e) => createBasicAuth();


		}

		async void createBasicAuth()
		{ 
			var nativeHttpClient = new HttpClient(new AndroidClientHandler());

			var testModelList = new List<TestModel>();

			testModelList = await HttpTestClass.basicAuthTask(nativeHttpClient, "UserName", "UserPassword", "ApiUrl");

			// TODO : Remove following code and customize as you need by using testModelList

			Console.WriteLine(testModelList[0].httpMessage + " " +  testModelList[0].httpStatus);

		}
	}
}

