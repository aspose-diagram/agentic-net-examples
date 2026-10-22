using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
        // Output HTML file path
        string outputPath = "output.html";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // TODO: Configure htmlOptions to embed CSS styles directly within the HTML output
            // (Aspose.Diagram may embed CSS by default; if a specific property exists, set it here.)

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}