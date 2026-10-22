using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputFolderPath>");
            return;
        }

        string diagramPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Ensure the output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare placeholder‑to‑CDN replacement map.
            var replacements = new Dictionary<string, string>
            {
                // Add your placeholder and CDN URL pairs here.
                { "PLACEHOLDER_URL_1", "https://cdn.example.com/resource1.png" },
                { "PLACEHOLDER_URL_2", "https://cdn.example.com/resource2.png" }
            };

            // Export each page to a separate HTML file.
            int pageCount = diagram.Pages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                // Configure HTML save options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1,
                    // Assign a custom stream provider to control image/resource paths.
                    StreamProvider = new CustomStreamProvider(outputFolder)
                };

                // Determine the HTML file name for the current page.
                string htmlFilePath = Path.Combine(outputFolder, $"page_{i + 1}.html");

                // Save the page as HTML.
                diagram.Save(htmlFilePath, htmlOptions);

                // Read the generated HTML content.
                string htmlContent = File.ReadAllText(htmlFilePath);

                // Perform placeholder replacements.
                foreach (var kvp in replacements)
                {
                    htmlContent = htmlContent.Replace(kvp.Key, kvp.Value);
                }

                // Write the updated HTML back to the file.
                File.WriteAllText(htmlFilePath, htmlContent);
            }

            // Dispose the diagram to free resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Custom stream provider to control where HTML resources (images, etc.) are written.
class CustomStreamProvider : IStreamProvider
{
    private readonly string _basePath;

    public CustomStreamProvider(string basePath)
    {
        _basePath = basePath;
    }

    // Called by Aspose.Diagram when a resource stream is needed.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base output folder with the default relative path provided by Aspose.
        string fullPath = Path.Combine(_basePath, options.DefaultPath);

        // Ensure the directory for the resource exists.
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Assign a writable file stream to the options.
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
    }
}