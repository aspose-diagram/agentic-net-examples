using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (absolute or relative path)
            string inputDiagramPath = "input.vsdx";

            // Directory where HTML output will be placed
            string htmlOutputDirectory = "HtmlOutput";

            // Ensure the output directory exists
            Directory.CreateDirectory(htmlOutputDirectory);

            // Full path for the main HTML file
            string htmlFilePath = Path.Combine(htmlOutputDirectory, "diagram.html");

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example: do not save as a single file to keep resources separate
                SaveAsSingleFile = false
            };

            // Export the diagram to HTML
            diagram.Save(htmlFilePath, htmlOptions);

            // Post‑process the generated HTML to replace absolute paths with relative ones
            string htmlContent = File.ReadAllText(htmlFilePath);

            // Example pattern: replace Windows absolute paths (e.g., C:\Folder\) with a relative path "./"
            // Adjust the pattern as needed for your specific absolute path formats.
            string pattern = @"[A-Za-z]:\\[^\s""']*";
            string replacedContent = Regex.Replace(htmlContent, pattern, "./");

            // Write the updated HTML back to the file
            File.WriteAllText(htmlFilePath, replacedContent);

            Console.WriteLine("HTML export completed and absolute paths have been replaced.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
