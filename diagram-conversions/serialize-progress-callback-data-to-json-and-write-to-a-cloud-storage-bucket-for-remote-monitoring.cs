using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProgressMonitoring
{
    // Represents progress information for each page saved.
    public class PageProgress
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // Implements the page saving callback to capture progress.
    public class ProgressCallback : IPageSavingCallback
    {
        private readonly List<PageProgress> _progressData = new List<PageProgress>();

        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start for this example.
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            _progressData.Add(new PageProgress
            {
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Timestamp = DateTime.UtcNow
            });
        }

        // Serializes the collected progress data to JSON and writes it to a cloud bucket.
        public void UploadProgress(string bucketPath)
        {
            string json = JsonSerializer.Serialize(_progressData, new JsonSerializerOptions { WriteIndented = true });

            // Simulate writing to a cloud storage bucket by writing to a file.
            // In a real scenario, replace this with the appropriate cloud SDK call.
            File.WriteAllText(bucketPath, json);
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram.
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Prepare PDF save options and assign the custom page saving callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                ProgressCallback callback = new ProgressCallback();
                pdfOptions.PageSavingCallback = callback;

                // Save the diagram as PDF.
                string outputPdfPath = "output.pdf";
                diagram.Save(outputPdfPath, pdfOptions);

                // After saving, upload the progress data to the cloud bucket.
                string bucketJsonPath = "cloud_bucket/progress.json";
                callback.UploadProgress(bucketJsonPath);

                Console.WriteLine("Diagram saved and progress uploaded successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}