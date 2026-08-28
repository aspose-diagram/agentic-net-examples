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
            try
            {

                // Path to the source Visio diagram
                string diagramPath = @"C:\Diagrams\sample.vsdx";

                // SharePoint site and target folder (e.g., Site Pages library)
                string sharepointSiteUrl = "https://contoso.sharepoint.com/sites/YourSite";
                string targetFolderRelativeUrl = "/sites/YourSite/SitePages";
                string targetFileName = "sampleDiagram.html";

                // Access token for SharePoint REST API (obtain via Azure AD or other auth flow)
                string accessToken = "<YOUR_ACCESS_TOKEN>";

                // Convert diagram to HTML and upload
                ConvertDiagramToHtmlAndUpload(diagramPath, sharepointSiteUrl, targetFolderRelativeUrl, targetFileName, accessToken);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        static void ConvertDiagramToHtmlAndUpload(string diagramFilePath, string siteUrl, string folderRelativeUrl, string fileName, string accessToken)
        {
            // Load the Visio diagram using Aspose.Diagram constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(diagramFilePath))
            {
                // Prepare HTML save options (rule-provided class)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Example: save as a single HTML file
                    SaveAsSingleFile = true,
                    // Optional: set title
                    Title = Path.GetFileNameWithoutExtension(diagramFilePath)
                };

                // Save the diagram to a memory stream as HTML (using provided Save method)
                using (MemoryStream htmlStream = new MemoryStream())
                {
                    diagram.Save(htmlStream, htmlOptions);
                    htmlStream.Position = 0; // Reset stream position for reading

                    // Read the HTML content as a byte array
                    byte[] htmlBytes = htmlStream.ToArray();

                    // Upload the HTML to SharePoint via REST API
                    UploadFileToSharePoint(siteUrl, folderRelativeUrl, fileName, htmlBytes, accessToken);
                }
            }
        }

        static void UploadFileToSharePoint(string siteUrl, string folderRelativeUrl, string fileName, byte[] fileContent, string accessToken)
        {
            // Construct the REST endpoint for adding a file to a folder
            string requestUri = $"{siteUrl}/_api/web/GetFolderByServerRelativeUrl('{folderRelativeUrl}')/Files/add(url='{fileName}',overwrite=true)";

            using (HttpClient httpClient = new HttpClient())
            {
                // Set authentication header (Bearer token)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Accept JSON response
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;odata=verbose"));

                // Prepare the content of the request (HTML file)
                using (ByteArrayContent content = new ByteArrayContent(fileContent))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                    // POST the file content
                    HttpResponseMessage response = httpClient.PostAsync(requestUri, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("HTML file uploaded successfully to SharePoint.");
                    }
                    else
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Failed to upload file. Status: {response.StatusCode}. Details: {responseBody}");
                    }
                }
            }
        }
    }