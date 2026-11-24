using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting arguments: [0]=Visio file path, [1]=SharePoint site URL, [2]=Page endpoint (REST URL), [3]=OAuth access token
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <visioFilePath> <sharepointSiteUrl> <sharepointPageRestUrl> <accessToken>");
            return;
        }

        string visioPath = args[0];
        string siteUrl = args[1];
        string pageRestUrl = args[2];
        string accessToken = args[3];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Prepare a temporary folder for HTML files
                string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramHtmlExport");
                Directory.CreateDirectory(tempFolder);

                // Export each page to a separate HTML file and collect the HTML content
                StringBuilder combinedHtml = new StringBuilder();

                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML export options for a single page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1
                    };

                    string htmlFilePath = Path.Combine(tempFolder, $"page_{i + 1}.html");

                    // Save the page as HTML
                    diagram.Save(htmlFilePath, htmlOptions);

                    // Read the generated HTML and append to the combined result
                    if (File.Exists(htmlFilePath))
                    {
                        string pageHtml = File.ReadAllText(htmlFilePath);
                        combinedHtml.AppendLine(pageHtml);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to generate HTML for page {i + 1}");
                    }
                }

                // Prepare the HTTP client for SharePoint REST call
                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the Authorization header with the provided access token
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Build the JSON payload expected by SharePoint (CanvasContent1 holds the HTML)
                    var payload = new
                    {
                        CanvasContent1 = combinedHtml.ToString()
                    };
                    string jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // SharePoint uses PATCH to update page content
                    HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), pageRestUrl)
                    {
                        Content = content
                    };

                    // Send the request synchronously
                    HttpResponseMessage response = httpClient.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("HTML content successfully embedded into SharePoint page.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to embed HTML. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        Console.Error.WriteLine($"Response body: {responseBody}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}