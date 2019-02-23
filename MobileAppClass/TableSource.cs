using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace MobileAppClass
{




	public class TableSource : UITableViewSource
	{
		LoadingOverlay loadPop;
		private List<ChuckData> data;
		MyViewController vc;
		FileManager web;


		public TableSource(List<ChuckData> datain, MyViewController mvc)
		{

			data = datain;

			vc = mvc;
			web = FileManager.getinstance();
			
		}


		public override nint NumberOfSections(UITableView tableView)
		{

			return 1;
		
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			UITableViewCell cell;

			cell = tableView.DequeueReusableCell("chuck");

			if (cell == null)
			{

				cell = new UITableViewCell(UITableViewCellStyle.Default, "chuck");

				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "chuck");

			}

			if (data != null)

			{


				cell.TextLabel.Text = data[indexPath.Row].ChuckQuote;


				cell.DetailTextLabel.Text = data[indexPath.Row].ID + " | " + data[indexPath.Row].EnteredBy + " | " + data[indexPath.Row].QuoteDate;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			
			
			}
			return cell;

		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			web.current = indexPath.Row;
			EditViewController dvc = new EditViewController(true);
			vc.NavigationController.PushViewController(dvc, true);


		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					// remove the item from the underlying data source
					data.RemoveAt(indexPath.Row);
					// delete the row from the table
					tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;
				case UITableViewCellEditingStyle.None:
					Console.WriteLine("CommitEditingStyle:None called");
					break;
			}
		}
		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		public override string TitleForDeleteConfirmation(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new LoadingOverlay(bounds);


			Task.Run(async () =>
			{

				//vc.View.Add(loadPop);
                if (indexPath != null && data != null)
					{
						
						await web.Delete(data[indexPath.Row].ID);
					}

				//Thread.Sleep(300);
				//loadPop.Hide();



			});

			return "delete";



		} 

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (data != null)
				return data.Count;
			return 1;
		}


	}
}
