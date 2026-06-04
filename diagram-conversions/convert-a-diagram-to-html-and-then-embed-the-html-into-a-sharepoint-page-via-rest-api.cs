using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string diagramPath = "input.vsdx";
            using Diagram diagram = new Diagram(diagramPath);

            // Convert the diagram to HTML using HTMLSaveOptions
            using MemoryStream htmlStream = new MemoryStream();
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example option: save as a single HTML file
                SaveAsSingleFile = true
            };
            diagram.Save(htmlStream, htmlOptions);
            htmlStream.Position = 0;

            // Read the generated HTML into a string
            string htmlContent;
            using (StreamReader reader = new StreamReader(htmlStream))
            {
                htmlContent = reader.ReadToEnd();
            }

            // SharePoint REST API details (replace placeholders with real values)
            string siteUrl = "https://contoso.sharepoint.com/sites/mysite";
            string pageServerRelativeUrl = "/sites/mysite/SitePages/MyPage.aspx";
            string requestUrl = $"{siteUrl}/_api/web/GetFileByServerRelativeUrl('{pageServerRelativeUrl}')/ListItemAllFields";
            string accessToken = "YOUR_ACCESS_TOKEN";

            // Prepare the HTTP client
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;odata=verbose"));
            httpClient.DefaultRequestHeaders.Add("IF-MATCH", "*");
            httpClient.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");

            // Payload to update the page's CanvasContent1 field with the HTML
            var payload = new
            {
                __metadata = new { type = "SP.Data.SitePageItem" },
                CanvasContent1 = htmlContent
            };
            string jsonPayload = JsonSerializer.Serialize(payload);
            using StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the request to embed the HTML into the SharePoint page
            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("Diagram successfully converted to HTML and embedded into the SharePoint page.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
