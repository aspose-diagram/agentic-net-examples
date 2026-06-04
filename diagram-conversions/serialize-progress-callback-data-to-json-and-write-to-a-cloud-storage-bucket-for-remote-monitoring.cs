using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProgressMonitoring
{
    // Class to hold progress information for each page saved
    public class PageProgressInfo
    {
        public int PageIndex { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // Implementation of IPageSavingCallback to capture page saving events
    public class ProgressCallback : IPageSavingCallback
    {
        private readonly List<PageProgressInfo> _progressData = new List<PageProgressInfo>();

        public IReadOnlyList<PageProgressInfo> ProgressData => _progressData.AsReadOnly();

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start of page saving for this example
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Record the page index and the time when the page finished saving
            _progressData.Add(new PageProgressInfo
            {
                PageIndex = args.PageIndex,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    class Program
    {
        // Entry point of the console application
        static async Task Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram (replace with actual path)
                const string sourceDiagramPath = "input.vsdx";

                // Path where the PDF will be saved locally (temporary)
                const string outputPdfPath = "output.pdf";

                // URL of the cloud storage bucket endpoint (replace with actual endpoint)
                const string cloudBucketUrl = "https://example.com/upload";

                // Load the diagram
                Diagram diagram = new Diagram(sourceDiagramPath);

                // Create PDF save options and assign the custom progress callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                ProgressCallback progressCallback = new ProgressCallback();
                pdfOptions.PageSavingCallback = progressCallback;

                // Save the diagram as PDF using the options
                diagram.Save(outputPdfPath, pdfOptions);

                // Serialize the captured progress data to JSON
                string jsonProgress = JsonSerializer.Serialize(progressCallback.ProgressData, new JsonSerializerOptions { WriteIndented = true });

                // Upload the JSON data to the cloud storage bucket
                await UploadProgressAsync(cloudBucketUrl, jsonProgress);

                // Clean up temporary PDF file if desired
                if (File.Exists(outputPdfPath))
                {
                    File.Delete(outputPdfPath);
                }

                Console.WriteLine("Progress data uploaded successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to upload JSON data to a cloud storage bucket via HTTP POST
        private static async Task UploadProgressAsync(string url, string jsonContent)
        {
            using HttpClient client = new HttpClient();

            using var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to upload progress data. Status code: {response.StatusCode}");
            }
        }
    }
}