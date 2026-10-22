using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ResourceNameCollector : IStreamProvider
{
    // Stores the names of all resources generated during HTML export.
    public List<string> ResourceNames { get; } = new List<string>();

    // Called when a new resource stream is initialized.
    public void InitStream(StreamProviderOptions options)
    {
        // DefaultPath contains the resource file name (e.g., "image1.png").
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            ResourceNames.Add(options.DefaultPath);
        }
    }

    // Called when the resource stream is closed. No action needed for validation.
    public void CloseStream(StreamProviderOptions options)
    {
        // Intentionally left blank.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output folder path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputFolderPath>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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

        // Load the diagram inside a try/catch to capture Aspose errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare HTML export options with a custom stream provider to capture resource names.
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
        // Export all pages.
        htmlOptions.PageIndex = 0;
        htmlOptions.PageCount = diagram.Pages.Count;

        // Instantiate the collector and assign it.
        ResourceNameCollector collector = new ResourceNameCollector();
        htmlOptions.StreamProvider = collector;

        // Define the output HTML file path.
        string outputHtmlPath = Path.Combine(outputFolder, "output.html");

        // Perform the save operation.
        try
        {
            diagram.Save(outputHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
            return;
        }

        // After export, validate that no duplicate resource names were generated.
        HashSet<string> seenNames = new HashSet<string>();
        bool duplicateFound = false;

        foreach (string name in collector.ResourceNames)
        {
            if (!seenNames.Add(name))
            {
                // Duplicate detected.
                duplicateFound = true;
                Console.Error.WriteLine($"Duplicate resource name detected: {name}");
            }
        }

        if (duplicateFound)
        {
            Console.Error.WriteLine("Validation failed: duplicate resource names were produced.");
        }
        else
        {
            Console.WriteLine("Validation succeeded: no duplicate resource names were produced.");
        }
    }
}