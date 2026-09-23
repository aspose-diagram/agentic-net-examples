using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioBatchHtmlExport
{
    // Shared stream provider for HTML export
    public class CustomStreamProvider : IStreamProvider
    {
        // Called before each file is saved
        public void InitStream(StreamProviderOptions options)
        {
            // Create a file stream for the target HTML file
            // options.DefaultPath contains the full output file path
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        // Called after each file is saved
        public void CloseStream(StreamProviderOptions options)
        {
            // Close the stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Output folder for generated HTML files
            string outputFolder = @"C:\VisioHtml";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Create a single shared IStreamProvider instance
            IStreamProvider sharedProvider = new CustomStreamProvider();

            // Get all Visio files (VSDX, VSD, VDX) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    continue; // Skip non‑Visio files

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Prepare HTML save options and assign the shared stream provider
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.StreamProvider = sharedProvider;

                    // Determine output HTML file path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                    // Save diagram as HTML
                    diagram.Save(outputPath, htmlOptions);

                    Console.WriteLine($"Converted '{filePath}' to HTML successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch conversion completed.");
        }
    }
}