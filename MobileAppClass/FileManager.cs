using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MobileAppClass
{
	public class FileManager
	{

		public static FileManager instance;
		private List<ChuckData> data { get; set; }
		public int current { get; set; }
		public List<ChuckData> currentset { get; set; }
        const string uri = "http://192.168.0.127/api/chuckdata.php";

        private FileManager()
		{

		}

		public static FileManager getinstance()
		{
			if (instance == null)
			{

				return instance = new FileManager();

			}
			else
			{

				return instance;

			}


		}



		public async Task<List<ChuckData>> Read()
		{
			// not to use httpsclient, add Reference in project under references, right click references, edit references, 
			HttpClient client;
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;

			var response = await client.GetAsync(uri);
			data = null;
			while (data == null)
			{
				if (response.IsSuccessStatusCode)
				{
                    Console.WriteLine(response);
					var content = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(content);
                    // best website ever  http://json2csharp.com/
                    // add newtonsoft json
                    Console.WriteLine(content);
					data = JsonConvert.DeserializeObject<List<ChuckData>>(content);
                    //Console.WriteLine("**");
                    //Console.WriteLine(data);
                    //Console.WriteLine("**");
				}
			}
            Console.WriteLine("endofread");
            return data;

		}

		public async Task Write(ChuckData myChuck)  // for add
		{
			var client = new HttpClient();
			//client.BaseAddress = new Uri(webAddress);

			string jsonData = JsonConvert.SerializeObject(myChuck);
            Console.WriteLine(jsonData);

			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(uri, content);

			if (response.StatusCode == System.Net.HttpStatusCode.Created)
			{
				Console.WriteLine("it worked!");
			}
			else
			{
                // something went wrong
                Console.WriteLine("notok");
				Console.WriteLine(response.ReasonPhrase);
			}
			var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
			// deserialize result to get the id of the item added
		}


		public async Task Delete(int idToDelete)  // for delete
		{
			var client = new HttpClient();

            HttpResponseMessage response = await client.DeleteAsync(uri+"?ID="+idToDelete);
			
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				Console.WriteLine("it worked!");
			}
			else
			{
                // something went wrong

				Console.WriteLine(response.ReasonPhrase);
			}
			var result = await response.Content.ReadAsStringAsync();
		}

		public async Task Edit(ChuckData myChuck)  // for edits
		{
			var client = new HttpClient();

			string jsonData = JsonConvert.SerializeObject(myChuck);

			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PutAsync(uri, content);

			if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
			{
				Console.WriteLine("it worked!");
			}
			else
			{
				// something went wrong
				Console.WriteLine(response.ReasonPhrase);
			}

			var result = await response.Content.ReadAsStringAsync();  

		}




	}
}
