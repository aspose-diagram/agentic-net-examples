using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // List to keep track of all resource file paths created during conversion
    public List<string> CreatedResources { get; } = new List<string>();

    // Base directory where HTML and its resources will be saved
    private readonly string _baseOutputDir;

    public MyStreamProvider(string baseOutputDir)
    {
        _baseOutputDir = baseOutputDir;
    }

    // Called by Aspose.Diagram before writing each resource (image, shape, etc.)
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Combine base output directory with the default relative path provided by Aspose
            string resourcePath = Path.Combine(_baseOutputDir, options.DefaultPath);

            // Ensure the directory for the resource exists
            string resourceDir = Path.GetDirectoryName(resourcePath);
            if (!Directory.Exists(resourceDir))
            {
                Directory.CreateDirectory(resourceDir);
            }

            // Create a file stream for the resource and assign it to the options
            FileStream fs = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
            options.Stream = fs;

            // Record the created resource path for later reporting
            CreatedResources.Add(resourcePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing stream for resource '{options.DefaultPath}': {ex.Message}");
            // In case of failure, ensure the stream is null to avoid further errors
            options.Stream = null;
        }
    }

    // Called by Aspose.Diagram after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            // Close the stream if it was successfully created
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream = null;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error closing stream for resource '{options.DefaultPath}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramConverter <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists or create it
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Define the main HTML file path (index.html) inside the output folder
        string htmlOutputPath = Path.Combine(outputFolder, "index.html");

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Initialize the custom stream provider with the output folder as base path
        var streamProvider = new MyStreamProvider(outputFolder);

        // Configure HTML save options and assign the stream provider
        var htmlOptions = new HTMLSaveOptions();
        htmlOptions.StreamProvider = streamProvider;

        try
        {
            // Save the diagram to HTML using the custom stream provider
            diagram.Save(htmlOutputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML conversion: {ex.Message}");
            return;
        }

        // Generate diagnostic report of all resources created by the stream provider
        Console.WriteLine("=== Diagnostic Report: Resources Created by IStreamProvider ===");
        if (streamProvider.CreatedResources.Count == 0)
        {
            Console.WriteLine("No resources were created.");
        }
        else
        {
            foreach (var resource in streamProvider.CreatedResources)
            {
                Console.WriteLine(resource);
            }
        }

        // Optionally write the report to a text file in the output folder
        string reportPath = Path.Combine(outputFolder, "resource_report.txt");
        try
        {
            File.WriteAllLines(reportPath, streamProvider.CreatedResources);
            Console.WriteLine($"Diagnostic report saved to: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write diagnostic report file: {ex.Message}");
        }
    }
}