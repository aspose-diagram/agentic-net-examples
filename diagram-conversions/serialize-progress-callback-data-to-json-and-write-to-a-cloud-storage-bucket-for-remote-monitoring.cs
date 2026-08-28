using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProgressMonitoring
{
    // Class to hold progress information for each page saved
    public class ProgressInfo
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // Implementation of IPageSavingCallback to capture page saving events
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Collected progress data
        public List<ProgressInfo> ProgressData { get; } = new List<ProgressInfo>();

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start of page saving for this scenario
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Record progress after each page is saved
            ProgressData.Add(new ProgressInfo
            {
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Timestamp = DateTime.UtcNow
            });

            // Example: stop after first page (optional)
            // args.HasMorePages = false;
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare PDF save options and assign the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                MyPageSavingCallback callback = new MyPageSavingCallback();
                pdfOptions.PageSavingCallback = callback;

                // Output PDF file path
                string outputPdfPath = "output.pdf";

                // Save the diagram as PDF, invoking the callback for each page
                diagram.Save(outputPdfPath, pdfOptions);

                // Serialize the collected progress data to JSON
                string json = JsonSerializer.Serialize(callback.ProgressData, new JsonSerializerOptions { WriteIndented = true });

                // Define a path representing the cloud storage bucket (replace with actual bucket integration as needed)
                string bucketFolder = "cloud_bucket";
                string bucketFilePath = Path.Combine(bucketFolder, "progress.json");

                // Ensure the bucket folder exists
                Directory.CreateDirectory(bucketFolder);

                // Write JSON to the bucket location
                File.WriteAllText(bucketFilePath, json);

                // Inform the user
                Console.WriteLine($"Progress data written to {bucketFilePath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}