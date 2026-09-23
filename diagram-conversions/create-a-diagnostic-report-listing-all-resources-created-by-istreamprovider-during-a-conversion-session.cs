using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionDiagnostic
{
    // Custom IStreamProvider that records each resource created during HTML export
    public class DiagnosticStreamProvider : IStreamProvider
    {
        // List to hold the identifiers (paths) of created resources
        public List<string> CreatedResources { get; } = new List<string>();

        // Called by Aspose.Diagram when a new resource stream is needed
        public void InitStream(StreamProviderOptions options)
        {
            // Record the default path (resource name) provided by the library
            CreatedResources.Add(options.DefaultPath);

            // Provide a stream for the resource; using MemoryStream as a placeholder
            options.Stream = new MemoryStream();
        }

        // Called when the resource stream is no longer needed
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (adjust as needed)
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Prepare HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                DiagnosticStreamProvider streamProvider = new DiagnosticStreamProvider();
                htmlOptions.StreamProvider = streamProvider;

                // Export the diagram to HTML (output folder will be created)
                string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                // After saving, output the list of resources created by the stream provider
                Console.WriteLine("Resources created by IStreamProvider during conversion:");
                foreach (string resource in streamProvider.CreatedResources)
                {
                    Console.WriteLine(resource);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}