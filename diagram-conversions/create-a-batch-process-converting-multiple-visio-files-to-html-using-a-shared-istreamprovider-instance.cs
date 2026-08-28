using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioBatchHtmlExport
{
    // Custom IStreamProvider that writes resources to a shared output directory
    public class SharedStreamProvider : IStreamProvider
    {
        private readonly string _baseOutputPath;

        public SharedStreamProvider(string baseOutputPath)
        {
            _baseOutputPath = baseOutputPath;
        }

        // Called when a resource stream is needed
        public void InitStream(StreamProviderOptions options)
        {
            // Combine base path with the default relative path provided by Aspose
            string fullPath = Path.Combine(_baseOutputPath, options.DefaultPath);

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Assign a writable file stream to the options
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
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

            // Output folder where HTML files and resources will be placed
            string outputFolder = @"C:\VisioHtmlOutput";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Create a single shared IStreamProvider instance
            SharedStreamProvider streamProvider = new SharedStreamProvider(outputFolder);

            // Get all Visio files (any supported extension) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            List<string> supportedExtensions = new List<string> { ".vsdx", ".vsd", ".vsdx", ".vssx", ".vstx", ".vdx", ".vsx", ".vtx" };

            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (!supportedExtensions.Contains(extension))
                {
                    continue; // Skip non-Visio files
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Prepare HTML save options
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        StreamProvider = streamProvider,
                        SaveAsSingleFile = false,
                        Title = Path.GetFileNameWithoutExtension(filePath)
                    };

                    // Determine output HTML file path
                    string outputHtmlPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".html");

                    // Save diagram as HTML
                    diagram.Save(outputHtmlPath, htmlOptions);

                    Console.WriteLine($"Successfully exported '{filePath}' to HTML.");
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