using System;
using System.Collections.Generic;
using System.Net.Http;
using UIKit;

namespace RestDemo.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			testButton.TouchUpInside += (sender, e) => createBasicAuth();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

		async void createBasicAuth()
		{
			var nativeHttpClient = new HttpClient(new NSUrlSessionHandler());

			var testModelList = new List<TestModel>();

			// TODO : Enter your own api key and key value 
			testModelList = await HttpTestClass.basicAuthTask(nativeHttpClient, "UserName", "UserPassword", "ApiUrl");


			// TODO : Remove following codes and customize as you need by using testModelList

			nameLabel.Text = testModelList[0].userName;

			lastNameLabel.Text = testModelList[0].userLastName;

			httpStatusLabel.Text = testModelList[0].httpStatus;

			httpMessageLabel.Text = testModelList[0].httpMessage;
		}

	}

}
