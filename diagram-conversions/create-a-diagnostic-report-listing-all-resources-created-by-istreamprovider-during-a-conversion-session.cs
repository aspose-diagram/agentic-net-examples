using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // List to store the paths of resources created during conversion.
    private readonly List<string> _createdResources = new List<string>();

    // Expose the collected resources as a read‑only collection.
    public IReadOnlyList<string> CreatedResources => _createdResources.AsReadOnly();

    // Called by Aspose.Diagram when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Record the default path where the resource is expected to be saved.
        _createdResources.Add(options.DefaultPath);

        // Provide a memory stream for the resource data.
        options.Stream = new MemoryStream();
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure the stream is properly disposed.
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
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: DiagramDiagnostic <inputVisioFile> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
        try
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the custom stream provider to capture resource creation.
            MyStreamProvider streamProvider = new MyStreamProvider();

            // Configure HTML save options with the stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;

            // Define the output HTML file path.
            string outputHtmlPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + ".html");

            // Save the diagram as HTML using the configured options.
            diagram.Save(outputHtmlPath, htmlOptions);

            // After conversion, generate a diagnostic report of created resources.
            string reportPath = Path.Combine(outputFolder, "diagnostic_report.txt");
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("Diagnostic Report: Resources Created by IStreamProvider");
                writer.WriteLine("------------------------------------------------------");
                foreach (string resourcePath in streamProvider.CreatedResources)
                {
                    writer.WriteLine(resourcePath);
                }
            }

            Console.WriteLine($"Conversion completed. HTML saved to: {outputHtmlPath}");
            Console.WriteLine($"Diagnostic report written to: {reportPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}