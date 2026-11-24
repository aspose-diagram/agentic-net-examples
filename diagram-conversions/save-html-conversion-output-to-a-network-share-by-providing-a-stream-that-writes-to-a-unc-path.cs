using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class NetworkStreamProvider : IStreamProvider
{
    private readonly string _uncPath;

    public NetworkStreamProvider(string uncPath)
    {
        _uncPath = uncPath;
    }

    // Called by Aspose.Diagram when it needs a stream to write HTML resources.
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Ensure the target directory exists.
            string directory = Path.GetDirectoryName(_uncPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a file stream that writes directly to the UNC path.
            options.Stream = new FileStream(_uncPath, FileMode.Create, FileAccess.Write);

            // Set a custom base URL/path if required by the HTML export.
            options.CustomPath = _uncPath;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to initialize stream for '{_uncPath}': {ex.Message}");
            throw;
        }
    }

    // Called by Aspose.Diagram after writing is complete.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            options.Stream?.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to close stream for '{_uncPath}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and UNC output path for HTML.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <uncOutputPath>");
            return;
        }

        string inputPath = args[0];
        string uncOutputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the UNC path is not empty.
        if (string.IsNullOrWhiteSpace(uncOutputPath))
        {
            Console.Error.WriteLine("UNC output path is empty or invalid.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Assign a custom stream provider that writes to the network share.
            htmlOptions.StreamProvider = new NetworkStreamProvider(uncOutputPath);

            // Perform the export. The dummy file name is required by the API but
            // the actual data will be written via the stream provider.
            diagram.Save("dummy.html", htmlOptions);

            Console.WriteLine($"HTML export completed successfully to UNC path: {uncOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}