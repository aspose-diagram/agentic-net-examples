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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolderPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        string zipPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists.
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the Visio diagram and export each page to HTML.
        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and save each as a separate HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML export options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,   // Zero‑based page index.
                    PageCount = 1    // Export only the current page.
                };

                // Build the output HTML file name.
                string htmlFile = Path.Combine(outputFolder, $"page_{i + 1}.html");

                // Save the current page as HTML.
                diagram.Save(htmlFile, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Compress the output folder into a ZIP archive.
        try
        {
            // If a ZIP file already exists, delete it to avoid IOException.
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(outputFolder, zipPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating ZIP archive: {ex.Message}");
            return;
        }

        Console.WriteLine("Conversion and compression completed successfully.");
    }
}