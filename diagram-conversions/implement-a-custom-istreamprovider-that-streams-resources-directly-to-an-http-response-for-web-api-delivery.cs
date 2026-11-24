using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Factory to create a stream for each resource based on its default path/name.
    private readonly Func<string, Stream> _streamFactory;

    public CustomStreamProvider(Func<string, Stream> streamFactory)
    {
        _streamFactory = streamFactory;
    }

    // Called by Aspose.Diagram when a resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath property contains the relative path/name of the resource.
        string resourcePath = options.DefaultPath;

        // Obtain a writable stream for the resource (e.g., a file stream, response stream, etc.).
        Stream resourceStream = _streamFactory(resourcePath);

        // Assign the stream to the options so Aspose can write the resource data.
        options.Stream = resourceStream;
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created.
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder for HTML resources.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Assign the custom stream provider to handle resource streams.
            htmlOptions.StreamProvider = new CustomStreamProvider(resourceName =>
            {
                // Build the full path for the resource inside the output folder.
                string resourcePath = Path.Combine(outputFolder, resourceName);

                // Ensure the directory for the resource exists.
                string resourceDir = Path.GetDirectoryName(resourcePath);
                if (!string.IsNullOrEmpty(resourceDir))
                {
                    Directory.CreateDirectory(resourceDir);
                }

                // Return a file stream that Aspose will write the resource into.
                return new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
            });

            // Define the main HTML file path.
            string htmlOutputPath = Path.Combine(outputFolder, "output.html");

            // Save the diagram as HTML using the custom stream provider.
            diagram.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML at: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}