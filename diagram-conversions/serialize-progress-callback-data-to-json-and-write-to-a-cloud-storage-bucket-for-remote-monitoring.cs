using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramProgressMonitoring
{
    // Class to hold progress information for each page event
    public class PageProgressInfo
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public string Event { get; set; } // "Start" or "End"
        public DateTime Timestamp { get; set; }
    }

    // Implementation of the page saving callback to capture progress
    public class PageSavingCallback : IPageSavingCallback
    {
        private readonly List<PageProgressInfo> _progressList;

        public PageSavingCallback(List<PageProgressInfo> progressList)
        {
            _progressList = progressList;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            _progressList.Add(new PageProgressInfo
            {
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Event = "Start",
                Timestamp = DateTime.UtcNow
            });
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            _progressList.Add(new PageProgressInfo
            {
                PageIndex = args.PageIndex,
                PageCount = args.PageCount,
                Event = "End",
                Timestamp = DateTime.UtcNow
            });
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Prepare a list to collect progress data
                List<PageProgressInfo> progressData = new List<PageProgressInfo>();

                // Configure PDF save options with the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageSavingCallback(progressData);

                // Output PDF path (could be any format; PDF is used to trigger page callbacks)
                string pdfOutputPath = "output.pdf";

                // Save the diagram using the options (this will invoke the callback)
                diagram.Save(pdfOutputPath, pdfOptions);

                // Serialize progress data to JSON
                string json = JsonSerializer.Serialize(progressData, new JsonSerializerOptions { WriteIndented = true });

                // Define the "cloud storage bucket" path (simulated as a local folder)
                string bucketFolder = Path.Combine("cloud_bucket");
                Directory.CreateDirectory(bucketFolder);
                string jsonFilePath = Path.Combine(bucketFolder, "progress.json");

                // Write JSON to the bucket
                File.WriteAllText(jsonFilePath, json);

                // Optional: inform the user
                Console.WriteLine($"Progress data written to: {jsonFilePath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}