using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HtmlExportWithCustomBaseUrl
{
    // Implements IStreamProvider to set a custom base URL for linked resources.
    class CustomStreamProvider : IStreamProvider
    {
        // Called before the HTML export starts.
        public void InitStream(StreamProviderOptions options)
        {
            // Set the custom base URL that will be prefixed to all linked resources.
            options.CustomPath = "https://cdn.example.com/visio-resources/";
        }

        // Called after the HTML export finishes.
        public void CloseStream(StreamProviderOptions options)
        {
            // No additional cleanup required.
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Export the diagram to HTML; linked resources will use the custom base URL.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}