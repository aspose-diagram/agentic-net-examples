using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDiagnostic
{
    // Custom IStreamProvider that records each resource created during HTML export
    public class DiagnosticStreamProvider : IStreamProvider
    {
        // List to hold the paths of created resources
        public List<string> CreatedResources { get; } = new List<string>();

        // Called when a new stream for a resource is initialized
        public void InitStream(StreamProviderOptions options)
        {
            // Record the default path (resource name) provided by Aspose.Diagram
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                CreatedResources.Add(options.DefaultPath);
            }

            // Assign a writable stream (in this example, a MemoryStream)
            // In real scenarios you might write to a file system location
            options.Stream = new System.IO.MemoryStream();
        }

        // Called when the stream for a resource is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly disposed
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                DiagnosticStreamProvider streamProvider = new DiagnosticStreamProvider();
                htmlOptions.StreamProvider = streamProvider;

                // Perform the HTML export
                string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                // Generate diagnostic report
                Console.WriteLine("=== Diagnostic Report: Resources Created by IStreamProvider ===");
                if (streamProvider.CreatedResources.Count == 0)
                {
                    Console.WriteLine("No resources were created.");
                }
                else
                {
                    foreach (string resource in streamProvider.CreatedResources)
                    {
                        Console.WriteLine($"- {resource}");
                    }
                }

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}