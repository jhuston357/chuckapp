using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UIKit;

namespace MobileAppClass
{
	public partial class EditViewController : UIViewController
	{
		LoadingOverlay loadPop;
		FileManager web;
		bool isedit;

		public EditViewController(bool edit) : base("EditViewController", null)
		{

			web = FileManager.getinstance();
			isedit = edit;

		}

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();



        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NavigationItem.Title = "Chuck Quotes";

            qbox.Text = "test";
            abox.Text = "test";

            if (isedit)
			{

				UIBarButtonItem edit = new UIBarButtonItem("Edit", UIBarButtonItemStyle.Done, savechuck);
				NavigationItem.RightBarButtonItem = edit;

                qbox.Text = web.currentset[web.current].ChuckQuote.ToString();
                abox.Text = web.currentset[web.current].EnteredBy.ToString();

            }
			else
			{

				UIBarButtonItem add = new UIBarButtonItem("Add", UIBarButtonItemStyle.Done, savechuck);
				NavigationItem.RightBarButtonItem = add;

			}

		}

		private void savechuck(Object sender, EventArgs args)
		{

            var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new LoadingOverlay(bounds);

			if ( String.IsNullOrWhiteSpace(qbox.Text) || String.IsNullOrWhiteSpace(abox.Text) )
			{

				UIAlertController ac;
				ac = UIAlertController.Create("Error", "Invalid Input", UIAlertControllerStyle.Alert);
				this.PresentViewController(ac, false, null);
				var cancelButton = UIAlertAction.Create("Try Again", UIAlertActionStyle.Cancel, null);
				ac.AddAction(cancelButton);

			}
			else
			{

                NavigationItem.RightBarButtonItem = null;

                if (!isedit)
				{
                
                    View.Add(loadPop);
                   
                    string q = qbox.Text;
                    string a = abox.Text;
                    ChuckData chuck = new ChuckData(1, q, a, DateTime.Now);
                    web.Write(chuck);

                    loadPop.Hide();
				
                }
				else
				{
                
                    View.Add(loadPop);
                   
                    web.currentset[web.current].ChuckQuote = qbox.Text;
                    web.currentset[web.current].EnteredBy = abox.Text;
                    web.Edit(web.currentset[web.current]);

                    loadPop.Hide();

				}
			}
		}


	




		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


        /*

		public async Task Write2(ChuckData myChuck)  // for add
		{

			var client = new HttpClient();
			client.BaseAddress = new Uri("192.168.0.130");

			string jsonData = JsonConvert.SerializeObject(myChuck);

			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync("/chuckdata.php", content);

			if (response.StatusCode == System.Net.HttpStatusCode.Created)
			{
				Console.WriteLine("it worked!");
			}
			else
			{
				// something went wrong
				Console.WriteLine(response.ReasonPhrase);
			}
			var result = await response.Content.ReadAsStringAsync();

			// deserialize result to get the id of the item add
		}
        */

	}
}

