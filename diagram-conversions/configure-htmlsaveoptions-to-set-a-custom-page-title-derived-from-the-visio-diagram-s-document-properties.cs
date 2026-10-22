using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.html";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the document title property (built‑in property)
            string pageTitle = diagram.DocumentProps.Title;

            // Fallback title if the document property is empty or null
            if (string.IsNullOrEmpty(pageTitle))
            {
                pageTitle = "Untitled Diagram";
            }

            // Configure HTML save options with the custom page title
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Set the HTML page title derived from the document property
                Title = pageTitle
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}