using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
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

        // Load the diagram inside a using block as required by the API.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export each page as a separate HTML file.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML save options for a single page.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1
                    };

                    string htmlFileName = $"page_{i + 1}.html";
                    string htmlFilePath = Path.Combine(outputFolder, htmlFileName);

                    // Save the page to HTML.
                    diagram.Save(htmlFilePath, htmlOptions);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Post‑process: replace absolute file paths in generated HTML files with relative paths.
        try
        {
            string[] htmlFiles = Directory.GetFiles(outputFolder, "*.html");
            foreach (string htmlFile in htmlFiles)
            {
                string content = File.ReadAllText(htmlFile);

                // Regex to match Windows absolute paths (e.g., C:\Folder\file.png).
                string pattern = @"[A-Za-z]:\\[^""\s]*";

                // Replace each absolute path with just the file name (relative reference).
                string updatedContent = Regex.Replace(content, pattern, match =>
                {
                    string absolutePath = match.Value;
                    string fileName = Path.GetFileName(absolutePath);
                    // Return the file name as a relative path.
                    return fileName;
                });

                // Write the modified content back to the HTML file.
                File.WriteAllText(htmlFile, updatedContent);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML post‑processing: {ex.Message}");
            return;
        }

        Console.WriteLine("HTML export and path conversion completed successfully.");
    }
}