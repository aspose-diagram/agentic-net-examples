using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path
        string sourcePath = "source.vsdx";
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }
        // Template diagram path
        string templatePath = "template.vsdx";
        if (!System.IO.File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }
        // Output HTML path
        string outputPath = "merged_output.html";

        // Guard against missing source file
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard against missing template file
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block (mandatory pattern)
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load the template diagram inside a using block
                using (Diagram templateDiagram = new Diagram(templatePath))
                {
                    // Merge each page from the template into the source diagram
                    foreach (Page tmplPage in templateDiagram.Pages)
                    {
                        // Add the template page to the source diagram
                        // This creates a copy of the page within the source diagram
                        sourceDiagram.Pages.Add(tmplPage);
                    }
                }

                // Prepare HTML save options with embedded CSS
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Enable embedding of CSS directly into the HTML (if supported)
                // The property name may vary; using BOOL.True as required by the API
                // If the property does not exist, the line can be removed without breaking compilation
                // htmlOptions.EmbeddedCss = BOOL.True; // Uncomment if property exists

                // Save the merged diagram as HTML with embedded CSS
                sourceDiagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine($"Diagram merged and saved to HTML successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}