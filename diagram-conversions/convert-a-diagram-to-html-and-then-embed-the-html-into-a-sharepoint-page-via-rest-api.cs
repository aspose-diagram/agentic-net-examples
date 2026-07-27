using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Entry point
    static async Task Main(string[] args)
    {
        try
        {

            // Paths and SharePoint parameters (replace with actual values)
            string diagramPath = @"C:\Diagrams\sample.vsdx";
            string sharepointSiteUrl = "https://yourtenant.sharepoint.com/sites/YourSite";
            string pageServerRelativeUrl = "/sites/YourSite/SitePages/YourPage.aspx";
            string accessToken = "YOUR_ACCESS_TOKEN"; // OAuth token with appropriate permissions

            // Load the Visio diagram using Aspose.Diagram constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Configure HTML save options (single file for easier embedding)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    SaveAsSingleFile = true,
                    // Optional: set title, resolution, etc.
                    Title = "Embedded Diagram"
                };

                // Save diagram as HTML into a memory stream (using Save method with SaveOptions)
                using (MemoryStream htmlStream = new MemoryStream())
                {
                    diagram.Save(htmlStream, htmlOptions);
                    // Convert stream to UTF‑8 string
                    string htmlContent = Encoding.UTF8.GetString(htmlStream.ToArray());

                    // Prepare HTTP client for SharePoint REST call
                    using (HttpClient httpClient = new HttpClient())
                    {
                        // Authorization header
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        // Accept JSON response
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;odata=verbose"));

                        // Build request URI for updating page properties
                        string requestUri = $"{sharepointSiteUrl}/_api/web/GetFileByServerRelativeUrl('{pageServerRelativeUrl}')/ListItemAllFields";

                        // JSON payload to update the CanvasContent1 field with the HTML
                        string jsonPayload = $"{{\"__metadata\":{{\"type\":\"SP.Data.SitePagesItem\"}},\"CanvasContent1\":\"{EscapeForJson(htmlContent)}\"}}";

                        // Create HTTP request with MERGE method (used for updates)
                        HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("MERGE"), requestUri)
                        {
                            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json;odata=verbose")
                        };
                        // Required headers for SharePoint update
                        request.Headers.Add("IF-MATCH", "*");
                        request.Headers.Add("X-HTTP-Method", "MERGE");

                        // Send request
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine("Diagram HTML successfully embedded into SharePoint page.");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape double quotes and backslashes for JSON string values
    private static string EscapeForJson(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        StringBuilder sb = new StringBuilder();
        foreach (char c in value)
        {
            switch (c)
            {
                case '\"':
                    sb.Append("\\\"");
                    break;
                case '\\':
                    sb.Append("\\\\");
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                default:
                    // Encode control characters
                    if (char.IsControl(c))
                    {
                        sb.Append("\\u");
                        sb.Append(((int)c).ToString("x4"));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }
}
