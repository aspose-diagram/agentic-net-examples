using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, template diagram path, output HTML path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagram> <templateDiagram> <outputHtml>");
            return;
        }

        string sourcePath = args[0];
        string templatePath = args[1];
        string outputPath = args[2];

        // Guard: verify source diagram exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source diagram file not found: {sourcePath}");
            return;
        }

        // Guard: verify template diagram exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template diagram file not found: {templatePath}");
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
                    // ------------------------------------------------------------
                    // Merge: copy all pages from the source diagram into the template
                    // ------------------------------------------------------------
                    foreach (Page srcPage in sourceDiagram.Pages)
                    {
                        // Add the source page to the template diagram's Pages collection
                        // The Pages collection supports Add(Page) to copy a page.
                        templateDiagram.Pages.Add(srcPage);
                    }

                    // ------------------------------------------------------------
                    // Export the merged diagram to HTML with embedded CSS
                    // ------------------------------------------------------------
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                    // The HTMLSaveOptions class does not expose a direct property for
                    // embedding CSS; by default Aspose.Diagram embeds CSS into the HTML.
                    // Additional configuration can be added here if needed.

                    // Save the merged diagram as HTML
                    templateDiagram.Save(outputPath, htmlOptions);
                }
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