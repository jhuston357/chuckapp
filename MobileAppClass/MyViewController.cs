using System;
using System.Collections.Generic;
using UIKit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace MobileAppClass
{
    public partial class MyViewController : UIViewController
    {

		private List<ChuckData> Chucks = new List<ChuckData>();

		FileManager web;

		System.Timers.Timer timer;

        public MyViewController() : base("MyViewController", null)
        {

			web = FileManager.getinstance();

			timer = new System.Timers.Timer();
			timer.Interval = 10000;

        }

		void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
			Task.Run(async () =>
			{
                Console.WriteLine("testone");

				Chucks = await web.Read();
                Console.WriteLine("test zone");
                Console.WriteLine(Chucks);
				web.currentset = Chucks;

				InvokeOnMainThread(() =>
				{
					tableview1.Source = new TableSource(Chucks, this);
					tableview1.ReloadData();

				});

			});


		}


		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;

		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			NavigationItem.Title = "Chuck Quotes";
			UIBarButtonItem plus = new UIBarButtonItem("+", UIBarButtonItemStyle.Done, newchuck);
			NavigationItem.RightBarButtonItem = plus;

		


		}

		public override void ViewWillDisappear(bool animated)
		{

			timer.Elapsed -= Timer_Elapsed;
			base.ViewWillDisappear(animated);



		}

		private void newchuck(Object sender, EventArgs args)
		{

			var vc = new EditViewController(false);
			NavigationController.PushViewController(vc, true);


		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}




	}
}

