using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = @"C:\Input\diagram.vsdx";

                // Output directory for EPS files
                string outputDir = @"C:\Output\EpsPages";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Construct the output file name for the current page
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex}.eps");

                    // Configure image save options for vector format (EMF used as EPS substitute)
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                    // Set the page index to export the specific page
                    saveOptions.PageIndex = pageIndex;

                    // Save the page as an EPS file (EMF content with .eps extension)
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Exported page {pageIndex} to {outputPath}");
                    pageIndex++;
                }

                // Optional: clean up resources
                diagram.Dispose();

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }