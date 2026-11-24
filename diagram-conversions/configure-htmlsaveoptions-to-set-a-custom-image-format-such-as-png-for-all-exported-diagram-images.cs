using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed or pass as first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for HTML files
        string outputDir = "HtmlOutput";

        // Guard: ensure the output directory exists or create it
        try
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // HTMLSaveOptions does not expose an image format property.
                // PNG is the default image format for HTML export, which satisfies the requirement.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Export each page as a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    htmlOptions.PageIndex = i;   // Zero‑based page index
                    htmlOptions.PageCount = 1;   // Export only the current page

                    string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                    try
                    {
                        diagram.Save(outputPath, htmlOptions);
                        Console.WriteLine($"Saved page {i + 1} to {outputPath}");
                    }
                    catch (Exception saveEx)
                    {
                        Console.Error.WriteLine($"Error saving page {i + 1}: {saveEx.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}