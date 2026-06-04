using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider for HTML export.
    // Routes resources (images, CSS, scripts) into subfolders based on file type.
    public class CustomStreamProvider : IStreamProvider
    {
        private readonly string _outputRoot;

        public CustomStreamProvider(string outputRoot)
        {
            _outputRoot = outputRoot ?? throw new ArgumentNullException(nameof(outputRoot));
        }

        // Called by Aspose.Diagram when a resource stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            // DefaultPath contains the relative path of the resource (e.g., "image1.png").
            // Determine subfolder based on file extension.
            string fileName = Path.GetFileName(options.DefaultPath);
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            string subFolder = extension switch
            {
                ".png" or ".jpg" or ".jpeg" or ".gif" or ".bmp" or ".tiff" => "Images",
                ".css" => "Styles",
                ".js" => "Scripts",
                ".svg" => "SVG",
                _ => "Resources"
            };

            // Build full directory and ensure it exists.
            string targetDir = Path.Combine(_outputRoot, subFolder);
            Directory.CreateDirectory(targetDir);

            // Full file path for the resource.
            string targetPath = Path.Combine(targetDir, fileName);

            // Assign a writable file stream to the options.
            options.Stream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file.
                string sourcePath = "input.vsdx";

                // Directory where HTML and resources will be saved.
                string outputFolder = "ExportedHtml";

                // Ensure the output directory exists.
                Directory.CreateDirectory(outputFolder);

                // Load the diagram.
                Diagram diagram = new Diagram(sourcePath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new CustomStreamProvider(outputFolder)
                };

                // Save the diagram as HTML. Resources will be placed in subfolders.
                string htmlPath = Path.Combine(outputFolder, "diagram.html");
                diagram.Save(htmlPath, htmlOptions);

                Console.WriteLine("HTML export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}