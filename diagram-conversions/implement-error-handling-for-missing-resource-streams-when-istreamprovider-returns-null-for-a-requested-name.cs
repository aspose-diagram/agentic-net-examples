using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that throws an exception when a requested resource is missing
    class MyStreamProvider : IStreamProvider
    {
        public void InitStream(StreamProviderOptions options)
        {
            // The expected file name for the resource is provided via DefaultPath
            string resourcePath = options.DefaultPath;

            if (File.Exists(resourcePath))
            {
                // Open the file stream for the existing resource
                options.Stream = new FileStream(resourcePath, FileMode.Open, FileAccess.Read);
            }
            else
            {
                // Resource not found – raise an informative exception
                throw new FileNotFoundException($"Resource not found: {resourcePath}");
            }
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the opened stream is properly disposed
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            string inputDiagram = "input.vsdx";

            if (!File.Exists(inputDiagram))
            {
                Console.WriteLine($"Input diagram file not found: {inputDiagram}");
                return;
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(inputDiagram))
            {
                // Set up HTML save options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new MyStreamProvider()
                };

                string outputHtml = "output.html";

                try
                {
                    // Save the diagram as HTML using the configured options
                    diagram.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"Diagram successfully saved to {outputHtml}");
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the save process
                    Console.WriteLine($"Error during HTML export: {ex.Message}");
                }
            }
        }
    }
}