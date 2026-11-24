using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ProgressStreamProvider : IStreamProvider
{
    private int _callCount = 0;

    // Called before any stream is requested.
    public void InitStream(StreamProviderOptions options)
    {
        // No initialization required for this simple implementation.
    }

    // Called each time Aspose.Diagram needs a stream to write a page.
    public Stream GetStream(StreamProviderOptions options)
    {
        _callCount++;
        // Update console progress bar in place.
        Console.Write($"\rGetStream called {_callCount} time(s).");

        // Ensure the directory for the target file exists.
        string targetPath = options.DefaultPath;
        string directory = Path.GetDirectoryName(targetPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Return a writable file stream for the requested path.
        return new FileStream(targetPath, FileMode.Create, FileAccess.Write);
    }

    // Called after the stream has been used.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required for this simple implementation.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Output directory for HTML files (second argument or default).
        string outputDir = args.Length > 1 ? args[1] : "HtmlOutput";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new ProgressStreamProvider();

            // Export each page as a separate HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure page-specific options.
                htmlOptions.PageIndex = i;
                htmlOptions.PageCount = 1;

                // Build the output file path for the current page.
                string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                // The StreamProvider will receive the target path via options.DefaultPath.
                // To make it available, set the DefaultPath property indirectly by using the Save method's path argument.
                diagram.Save(outputPath, htmlOptions);
            }

            // Move to the next line after the progress bar.
            Console.WriteLine();
            Console.WriteLine("HTML export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}