using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace BatchHtmlExport
{
    // Custom stream provider that creates a file stream for each HTML export
    // and disposes it after the save operation to avoid memory leaks.
    public class FileStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram before writing the HTML file.
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath contains the full path of the HTML file to be created.
            // Create a writable file stream and assign it to the options.
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        // Called by Aspose.Diagram after the HTML file has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to release the file handle and free resources.
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files to convert.
            string inputFolder = @"C:\VisioFiles";
            // Folder where the HTML files will be saved.
            string outputFolder = @"C:\VisioHtml";

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get all Visio files (VSDX, VSD, VDX) in the input folder.
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Process only supported Visio extensions.
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Prepare HTML save options and assign the custom stream provider.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.StreamProvider = new FileStreamProvider();

                    // Determine output HTML file path.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                    // Save the diagram as HTML. The stream provider will handle stream creation and disposal.
                    diagram.Save(outputPath, htmlOptions);

                    Console.WriteLine($"Successfully exported '{filePath}' to HTML.");
                }
                catch (Exception ex)
                {
                    // Log any errors for the current file but continue processing others.
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch HTML export completed.");
        }
    }
}