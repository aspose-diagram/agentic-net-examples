using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Initializes a read‑only stream for the requested resource.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath property points to the resource file that Aspose.Diagram wants to write.
        // We open it as a read‑only stream to avoid creating duplicate files.
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        string path = options.DefaultPath;

        if (File.Exists(path))
        {
            // Open the existing file in read‑only mode.
            options.Stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        else
        {
            // If the file does not exist, provide an empty stream to satisfy the contract.
            options.Stream = Stream.Null;
        }
    }

    // Closes and disposes the stream created in InitStream.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options?.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new MyStreamProvider();

            // Optional: export only the first page (adjust as needed).
            // htmlOptions.PageIndex = 0;
            // htmlOptions.PageCount = 1;

            // Save the diagram to HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}