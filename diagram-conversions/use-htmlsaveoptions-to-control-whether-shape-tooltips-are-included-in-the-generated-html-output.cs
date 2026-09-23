using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file path
        string inputPath = "sample.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output HTML file path
        string outputPath = "output.html";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Create HTML export options; tooltips are included by default
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

            // Inform the user that the export completed
            Console.WriteLine($"Diagram saved to {outputPath} (tooltips are included by default).");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}