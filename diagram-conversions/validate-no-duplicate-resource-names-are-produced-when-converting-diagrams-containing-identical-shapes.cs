using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ResourceNameCollector : IStreamProvider
{
    // Stores all resource names encountered during HTML export.
    private readonly HashSet<string> _uniqueNames = new HashSet<string>();
    private readonly List<string> _allNames = new List<string>();
    public bool DuplicateFound { get; private set; } = false;
    public string DuplicateName { get; private set; } = null;

    // Called by Aspose.Diagram when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Record the resource name.
        string name = options.DefaultPath;
        _allNames.Add(name);
        if (!_uniqueNames.Add(name))
        {
            // Duplicate detected.
            DuplicateFound = true;
            DuplicateName = name;
        }

        // Provide a dummy stream; the actual content is not needed for validation.
        options.Stream = new MemoryStream();
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export all pages in a single HTML file.
            htmlOptions.PageIndex = 0;
            htmlOptions.PageCount = diagram.Pages.Count;

            // Attach the custom stream provider to capture resource names.
            ResourceNameCollector collector = new ResourceNameCollector();
            htmlOptions.StreamProvider = collector;

            // Output HTML file path.
            string outputHtml = "output.html";

            // Perform the conversion.
            diagram.Save(outputHtml, htmlOptions);

            // Validate that no duplicate resource names were generated.
            if (collector.DuplicateFound)
            {
                Console.Error.WriteLine($"Duplicate resource name detected: {collector.DuplicateName}");
                // Optionally, you could throw an exception to signal failure.
                // throw new Exception($"Duplicate resource name: {collector.DuplicateName}");
            }
            else
            {
                Console.WriteLine("Validation passed: No duplicate resource names were produced.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}