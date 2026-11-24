using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Stores the resource names (DefaultPath) received during conversion.
    public List<string> ReceivedPaths { get; } = new List<string>();

    // Called by Aspose.Diagram when a resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Record the resource name.
        ReceivedPaths.Add(options.DefaultPath);

        // Provide a writable stream for the resource. Using MemoryStream as a placeholder.
        options.Stream = new MemoryStream();
    }

    // Called after the resource stream has been used.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created.
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path.
        string inputPath = "sample.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output HTML file path.
        string outputPath = "output.html";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Instantiate the custom stream provider.
        CustomStreamProvider streamProvider = new CustomStreamProvider();

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;

            // Perform the conversion to HTML.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            return;
        }

        // Simple verification that the stream provider was invoked.
        if (streamProvider.ReceivedPaths.Count == 0)
        {
            Console.Error.WriteLine("Test failed: IStreamProvider was not called during conversion.");
            return;
        }

        // Output the captured resource names for manual inspection.
        Console.WriteLine("IStreamProvider received the following resource names:");
        foreach (string path in streamProvider.ReceivedPaths)
        {
            Console.WriteLine($"- {path}");
        }

        // Indicate success.
        Console.WriteLine("Test passed: IStreamProvider received resource names as expected.");
    }
}