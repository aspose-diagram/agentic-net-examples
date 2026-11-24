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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolderPath>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Define the path for the generated HTML file.
        string htmlFilePath = Path.Combine(outputFolder, "output.html");

        // Load the Visio diagram and export it to HTML.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options to generate a single HTML file.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export only the first page as a single HTML file.
                    PageIndex = 0,
                    PageCount = 1
                };

                // Save the diagram as HTML.
                diagram.Save(htmlFilePath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
            return;
        }

        // Verify that the HTML file was created.
        if (!File.Exists(htmlFilePath))
        {
            Console.Error.WriteLine($"HTML file was not created: {htmlFilePath}");
            return;
        }

        // Define placeholder‑to‑CDN URL mappings.
        var replacements = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Example mappings – adjust as needed.
            { "http://placeholder.com/image1.png", "https://cdn.example.com/assets/image1.png" },
            { "http://placeholder.com/style.css", "https://cdn.example.com/assets/style.css" }
        };

        // Read the generated HTML content.
        string htmlContent;
        try
        {
            htmlContent = File.ReadAllText(htmlFilePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read HTML file: {ex.Message}");
            return;
        }

        // Perform string replacements for each placeholder.
        foreach (KeyValuePair<string, string> kvp in replacements)
        {
            if (!string.IsNullOrEmpty(kvp.Key))
            {
                htmlContent = htmlContent.Replace(kvp.Key, kvp.Value);
            }
        }

        // Write the updated HTML back to the file.
        try
        {
            File.WriteAllText(htmlFilePath, htmlContent);
            Console.WriteLine($"HTML export and URL replacement completed successfully. File: {htmlFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write updated HTML file: {ex.Message}");
        }
    }
}