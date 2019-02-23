using System;
namespace MobileAppClass
{

	public class ChuckData
	{
		public int ID { get; set; }
		public string ChuckQuote { get; set; }
		public string EnteredBy { get; set; }
		public DateTime QuoteDate { get; set; }

		public ChuckData(int ID, string Quote, string author, DateTime date)
		{
          
			this.ID = ID;
			this.ChuckQuote = Quote;
			this.EnteredBy = author;
			this.QuoteDate = date;
            
		}

		public override string ToString()
		{
			return string.Format("[ChuckData: Id={0}, ChuckQuote={1}, EnteredBy={2}, QuoteDate={3}]", ID, ChuckQuote, EnteredBy, QuoteDate);
		}


	}
}