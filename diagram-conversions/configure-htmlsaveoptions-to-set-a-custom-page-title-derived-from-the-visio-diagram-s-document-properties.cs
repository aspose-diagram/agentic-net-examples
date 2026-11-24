using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the built‑in document title property
            string docTitle = diagram.DocumentProps.Title;

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // TODO: Set the custom page title on htmlOptions if the API provides such a property.
            // Example (if available): htmlOptions.Title = docTitle;
            // Replace the above line with the correct property name when known.

            // Save the diagram as HTML using the configured options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"HTML export completed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}