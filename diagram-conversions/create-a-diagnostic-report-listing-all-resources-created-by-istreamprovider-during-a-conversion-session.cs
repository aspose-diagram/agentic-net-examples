using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDiagnostic
{
    // Custom IStreamProvider that records each resource path requested during HTML export
    public class DiagnosticStreamProvider : IStreamProvider
    {
        // List to hold the default paths of created resources
        public List<string> CreatedResources { get; } = new List<string>();

        // Called by Aspose.Diagram when a new stream for a resource is needed
        public void InitStream(StreamProviderOptions options)
        {
            // Record the resource identifier (DefaultPath is read‑only but provides the name)
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                CreatedResources.Add(options.DefaultPath);
            }

            // Provide a memory stream for the resource (could be a FileStream if needed)
            options.Stream = new MemoryStream();
        }

        // Called when the stream is no longer needed
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly closed and disposed
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Create a simple diagram with one rectangle shape
                Diagram diagram = new Diagram();
                // Add a rectangle shape at position (1,1) on the first page (master index 0)
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

                // Prepare HTML save options and attach the diagnostic stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                DiagnosticStreamProvider provider = new DiagnosticStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                // Generate diagnostic report of all resources created by the stream provider
                Console.WriteLine("=== Diagnostic Report: Resources Created by IStreamProvider ===");
                if (provider.CreatedResources.Count == 0)
                {
                    Console.WriteLine("No resources were created during the conversion.");
                }
                else
                {
                    for (int i = 0; i < provider.CreatedResources.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {provider.CreatedResources[i]}");
                    }
                }

                // Clean up diagram resources
                diagram.Dispose();

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}