using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputFolderPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        string zipPath = args[2];

        // Guard: ensure the input Visio file exists.
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
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the diagram and export each page to HTML.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure HTML export for a single page.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1
                    };

                    // Build the HTML file name (e.g., page_1.html).
                    string htmlFilePath = Path.Combine(outputFolder, $"page_{i + 1}.html");

                    // Export the page.
                    diagram.Save(htmlFilePath, htmlOptions);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
            return;
        }

        // Compress the output folder into a ZIP archive.
        try
        {
            // Delete existing ZIP if it exists to avoid IOException.
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            ZipFile.CreateFromDirectory(outputFolder, zipPath, CompressionLevel.Optimal, false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during ZIP compression: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram successfully exported to HTML and compressed into ZIP.");
    }
}