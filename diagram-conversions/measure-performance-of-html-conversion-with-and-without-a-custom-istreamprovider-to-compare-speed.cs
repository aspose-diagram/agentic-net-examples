using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Initialize a custom stream for each resource during HTML export
    public void InitStream(StreamProviderOptions options)
    {
        // Create a folder to store custom resources
        string resourcesDir = Path.Combine(Directory.GetCurrentDirectory(), "custom_resources");
        Directory.CreateDirectory(resourcesDir);

        // Use the default file name provided by Aspose
        string fileName = Path.GetFileName(options.DefaultPath);
        string fullPath = Path.Combine(resourcesDir, fileName);

        // Set a custom base URL (relative path) for the resources
        options.CustomPath = "custom_resources/";

        // Open a file stream that Aspose will write the resource into
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Close the stream after Aspose finishes writing the resource
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare output file paths
        string outputDirectory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        string outputNoProvider = Path.Combine(outputDirectory, "output_no_provider.html");
        string outputWithProvider = Path.Combine(outputDirectory, "output_with_provider.html");

        // Stopwatch for performance measurement
        Stopwatch stopwatch = new Stopwatch();

        // ------------------------------
        // Conversion without IStreamProvider
        // ------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                stopwatch.Start();
                diagram.Save(outputNoProvider, htmlOptions);
                stopwatch.Stop();
            }

            Console.WriteLine($"Conversion without IStreamProvider took {stopwatch.ElapsedMilliseconds} ms.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion without IStreamProvider: {ex.Message}");
            return;
        }

        // Reset stopwatch for the next measurement
        stopwatch.Reset();

        // ------------------------------
        // Conversion with custom IStreamProvider
        // ------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the custom stream provider
                    StreamProvider = new CustomStreamProvider()
                };

                stopwatch.Start();
                diagram.Save(outputWithProvider, htmlOptions);
                stopwatch.Stop();
            }

            Console.WriteLine($"Conversion with IStreamProvider took {stopwatch.ElapsedMilliseconds} ms.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion with IStreamProvider: {ex.Message}");
            return;
        }
    }
}