using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDiagnostics
{
    // Custom stream provider that records each resource created during HTML export
    public class DiagnosticStreamProvider : IStreamProvider
    {
        // List to hold the paths (DefaultPath) of created resources
        private readonly List<string> _createdResources = new List<string>();

        // Expose the recorded resources
        public IReadOnlyList<string> CreatedResources => _createdResources.AsReadOnly();

        // Called by Aspose.Diagram when a new resource stream is needed
        public void InitStream(StreamProviderOptions options)
        {
            // Record the resource identifier (DefaultPath)
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                _createdResources.Add(options.DefaultPath);
            }

            // Provide a memory stream for the resource
            options.Stream = new MemoryStream();
        }

        // Called when the resource stream is no longer needed
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly disposed
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio file (adjust as needed)
                const string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                DiagnosticStreamProvider streamProvider = new DiagnosticStreamProvider();
                htmlOptions.StreamProvider = streamProvider;

                // Export the diagram to HTML (output file path can be adjusted)
                const string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                // After export, generate a diagnostic report of created resources
                Console.WriteLine("Diagnostic Report: Resources created by IStreamProvider");
                Console.WriteLine("------------------------------------------------------");
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

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}