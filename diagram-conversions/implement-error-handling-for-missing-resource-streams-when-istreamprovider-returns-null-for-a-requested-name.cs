using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that throws an exception when a requested resource cannot be found.
    public class CustomStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram before a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // The requested resource path is provided via DefaultPath.
            string resourcePath = options.DefaultPath;

            // Validate the path.
            if (string.IsNullOrEmpty(resourcePath) || !File.Exists(resourcePath))
            {
                // Throw an exception to indicate the missing resource.
                throw new Exception($"Resource not found: '{resourcePath}'.");
            }

            // Open the file stream and assign it to the options.
            options.Stream = new FileStream(resourcePath, FileMode.Open, FileAccess.Read);
        }

        // Called after the resource has been processed.
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is properly closed and disposed.
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new CustomStreamProvider(),
                    // Example: export only the first page.
                    PageIndex = 0,
                    PageCount = 1,
                    SaveAsSingleFile = true
                };

                // Save the diagram as HTML. The custom provider will handle resource streams.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine("HTML export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}