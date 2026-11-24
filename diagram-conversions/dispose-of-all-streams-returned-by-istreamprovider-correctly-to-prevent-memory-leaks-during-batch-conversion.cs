using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Initialize the stream for the given resource path.
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Ensure the directory for the target file exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a writable file stream and assign it to the options.
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write, FileShare.None);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to initialize stream for '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }

    // Close and dispose the stream after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to close stream for '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard against missing input folder.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Collect all Visio files for batch conversion.
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found for conversion.");
            return;
        }

        foreach (string visioPath in visioFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added for safety).
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(visioPath);

                // Prepare HTML save options with custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Determine output HTML file path.
                string outputHtmlPath = Path.Combine(
                    outputFolder,
                    Path.GetFileNameWithoutExtension(visioPath) + ".html");

                // Perform the conversion.
                diagram.Save(outputHtmlPath, htmlOptions);

                Console.WriteLine($"Successfully converted '{visioPath}' to HTML.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{visioPath}': {ex.Message}");
            }
        }
    }
}