using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    public void InitStream(StreamProviderOptions options)
    {
        // Set the custom base URL for all linked resources in the HTML output
        options.CustomPath = "https://mycdn.example.com/visio/";

        // Provide a dummy stream (required by the interface)
        options.Stream = new MemoryStream();
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // Clean up the stream created in InitStream
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options and assign the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider();

            // Save the diagram as HTML with the custom base URL applied
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram saved to HTML with custom base URL.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}