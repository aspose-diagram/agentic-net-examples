using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RetryStreamProvider : IStreamProvider
{
    // Base directory where resource files (images, etc.) will be written.
    private readonly string _basePath;
    // Maximum number of retry attempts for transient I/O errors.
    private const int MaxRetries = 3;
    // Delay between retries in milliseconds.
    private const int DelayMs = 500;

    public RetryStreamProvider(string basePath)
    {
        _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
    }

    // Called by Aspose.Diagram when a resource stream needs to be created.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base path with the default file name provided by Aspose.
        string fileName = options.DefaultPath;
        string fullPath = Path.Combine(_basePath, fileName);

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Attempt to open the file stream with retry logic.
        int attempt = 0;
        while (true)
        {
            try
            {
                // Create a new file stream for writing the resource.
                options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                break; // Success, exit the retry loop.
            }
            catch (IOException ex) when (attempt < MaxRetries)
            {
                // Transient I/O error – wait and retry.
                attempt++;
                Console.Error.WriteLine($"I/O error while creating stream for '{fullPath}'. Retry {attempt}/{MaxRetries}. Error: {ex.Message}");
                Thread.Sleep(DelayMs);
            }
        }
    }

    // Called by Aspose.Diagram after the resource stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        // Safely close the stream if it was created.
        if (options.Stream != null)
        {
            try
            {
                options.Stream.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error closing stream: {ex.Message}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (default to \"output\" if not supplied).
        string outputDir = args.Length > 1 ? args[1] : "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the HTML output file path.
        string htmlOutputPath = Path.Combine(outputDir, "diagram.html");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom retry stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new RetryStreamProvider(outputDir);

            // Save the diagram as HTML, resources will be written via the stream provider.
            diagram.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML at: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O exceptions and report them.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}