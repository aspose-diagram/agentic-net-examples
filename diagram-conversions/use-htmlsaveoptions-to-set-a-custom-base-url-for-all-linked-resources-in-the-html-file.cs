using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Called when the HTML export process needs to initialize a stream for a resource.
    public void InitStream(StreamProviderOptions options)
    {
        // Set a custom base URL that will be prefixed to all linked resources (images, CSS, etc.).
        options.CustomPath = "https://cdn.example.com/visio-resources/";
        // No actual stream is created here because we only want to modify the URL.
    }

    // Called after the resource stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required for this simple implementation.
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

            // Configure HTML export options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider();

            // Save the diagram as HTML; all linked resources will use the custom base URL.
            diagram.Save("output.html", htmlOptions);

            Console.WriteLine("Diagram exported to HTML with custom base URL.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}