using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments:
        // 0 - Path to the Visio diagram file
        // 1 - SharePoint site base URL (e.g., https://contoso.sharepoint.com/sites/mysite)
        // 2 - Target folder relative URL within the site (e.g., SitePages)
        // 3 - Access token for Bearer authentication
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <sharepointSiteUrl> <targetFolderRelativeUrl> <accessToken>");
            return;
        }

        string diagramPath = args[0];
        string sharepointSiteUrl = args[1].TrimEnd('/');
        string targetFolderRelativeUrl = args[2].Trim('/');
        string accessToken = args[3];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Temporary HTML file path
        string tempHtmlPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".html");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Export the entire diagram to HTML using SaveFileFormat.Html
            diagram.Save(tempHtmlPath, SaveFileFormat.Html);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram conversion: {ex.Message}");
            return;
        }

        // Read the generated HTML content
        string htmlContent;
        try
        {
            htmlContent = File.ReadAllText(tempHtmlPath, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading temporary HTML file: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary file
            try { if (File.Exists(tempHtmlPath)) File.Delete(tempHtmlPath); } catch { }
        }

        // Prepare the SharePoint REST API endpoint for uploading the HTML file
        // The file will be named "diagram.html" in the target folder
        string uploadFileName = "diagram.html";
        if (!System.IO.File.Exists(uploadFileName))
        {
            Console.Error.WriteLine($"File not found: {uploadFileName}");
            return;
        }
        string requestUri = $"{sharepointSiteUrl}/_api/web/GetFolderByServerRelativeUrl('{targetFolderRelativeUrl}')/Files/add(url='{uploadFileName}',overwrite=true)";

        // Perform the HTTP request
        using (HttpClient httpClient = new HttpClient())
        {
            // Set the Authorization header with the Bearer token
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Prepare the content
            ByteArrayContent content = new ByteArrayContent(Encoding.UTF8.GetBytes(htmlContent));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

            // Send the POST request
            HttpResponseMessage response;
            try
            {
                response = httpClient.PostAsync(requestUri, content).Result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"HTTP request failed: {ex.Message}");
                return;
            }

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("HTML successfully uploaded to SharePoint.");
            }
            else
            {
                Console.Error.WriteLine($"Failed to upload HTML. Status: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.Error.WriteLine($"Response body: {responseBody}");
            }
        }
    }
}