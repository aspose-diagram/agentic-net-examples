using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Entry point
        static async Task Main(string[] args)
        {
            try
            {

                // Input diagram file path
                string diagramPath = @"C:\Diagrams\sample.vsdx";

                // SharePoint site and page details
                string sharepointSiteUrl = "https://contoso.sharepoint.com/sites/ProjectSite";
                string pageServerRelativeUrl = "/sites/ProjectSite/SitePages/DiagramPage.aspx";
                string accessToken = "YOUR_ACCESS_TOKEN"; // Obtain via Azure AD/OAuth

                // Convert diagram to HTML string
                string htmlContent = ConvertDiagramToHtml(diagramPath);

                // Embed HTML into SharePoint page
                await UpdateSharePointPageAsync(sharepointSiteUrl, pageServerRelativeUrl, htmlContent, accessToken);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Converts a Visio diagram to an HTML string using Aspose.Diagram
        private static string ConvertDiagramToHtml(string diagramFilePath)
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramFilePath);

            // Save to a memory stream in HTML format
            using (MemoryStream htmlStream = new MemoryStream())
            {
                diagram.Save(htmlStream, SaveFileFormat.Html);
                htmlStream.Position = 0;
                using (StreamReader reader = new StreamReader(htmlStream))
                {
                    // Return the HTML content as a string
                    return reader.ReadToEnd();
                }
            }
        }

        // Updates the SharePoint page's CanvasContent1 field with the provided HTML
        private static async Task UpdateSharePointPageAsync(string siteUrl, string pageRelativeUrl, string html, string bearerToken)
        {
            // Build the REST endpoint for the page's ListItem
            string requestUrl = $"{siteUrl}/_api/web/GetFileByServerRelativeUrl('{pageRelativeUrl}')/ListItemAllFields";

            // Prepare the JSON payload to update CanvasContent1
            string payload = $"{{ '__metadata': {{ 'type': 'SP.Data.SitePagesItem' }}, 'CanvasContent1': '{EscapeForJson(html)}' }}";

            using (HttpClient client = new HttpClient())
            {
                // Set authentication header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                // Set Accept header
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;odata=verbose"));
                // Set X-RequestDigest header (required for POST). In production, retrieve it via /_api/contextinfo.
                client.DefaultRequestHeaders.Add("X-RequestDigest", await GetFormDigestAsync(siteUrl, bearerToken));

                // Prepare the request content
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json;odata=verbose");

                // Use MERGE method to update existing fields
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("MERGE"), requestUrl)
                {
                    Content = content
                };
                request.Headers.Add("IF-MATCH", "*"); // Overwrite regardless of version

                // Send the request
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("SharePoint page updated successfully.");
            }
        }

        // Retrieves the FormDigestValue required for POST/MERGE operations
        private static async Task<string> GetFormDigestAsync(string siteUrl, string bearerToken)
        {
            string contextInfoUrl = $"{siteUrl}/_api/contextinfo";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;odata=verbose"));

                HttpResponseMessage response = await client.PostAsync(contextInfoUrl, null);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                // Simple extraction of FormDigestValue from the JSON response
                // In production, use a proper JSON parser like Newtonsoft.Json or System.Text.Json
                const string tokenKey = "\"FormDigestValue\":\"";
                int start = json.IndexOf(tokenKey) + tokenKey.Length;
                int end = json.IndexOf("\"", start);
                return json.Substring(start, end - start);
            }
        }

        // Escapes single quotes and line breaks for JSON string value
        private static string EscapeForJson(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Replace backslashes first
            string escaped = input.Replace("\\", "\\\\");
            // Escape double quotes
            escaped = escaped.Replace("\"", "\\\"");
            // Escape newlines
            escaped = escaped.Replace("\r", "").Replace("\n", "\\n");
            // Escape single quotes (required for SharePoint JSON payload)
            escaped = escaped.Replace("'", "''");
            return escaped;
        }
    }