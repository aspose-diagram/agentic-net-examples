using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ReadOnlyStreamProvider : IStreamProvider
{
    // Initializes a read‑only stream for the resource identified by options.DefaultPath.
    public void InitStream(StreamProviderOptions options)
    {
        // Ensure the default path is provided.
        if (string.IsNullOrEmpty(options.DefaultPath))
        {
            // No path supplied; assign a null stream to avoid errors.
            options.Stream = Stream.Null;
            return;
        }

        // If the resource file exists, open it as a read‑only stream.
        if (File.Exists(options.DefaultPath))
        {
            options.Stream = new FileStream(
                options.DefaultPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
        }
        else
        {
            // Resource does not exist; use an empty stream.
            options.Stream = Stream.Null;
        }
    }

    // Closes and disposes the stream created in InitStream.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null && options.Stream != Stream.Null)
        {
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (change as needed).
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output HTML file path.
        string outputPath = "output.html";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Assign the custom provider to avoid duplicating existing resources.
                StreamProvider = new ReadOnlyStreamProvider()
            };

            // Save the diagram as HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}