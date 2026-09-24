using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        string outputPath = "output.svg";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the diagram to SVG
            diagram.Save(outputPath, svgOptions);
            Console.WriteLine($"Diagram successfully saved as SVG to '{outputPath}'.");
        }
        catch (DiagramException ex)
        {
            // Log detailed error information
            Console.WriteLine("An error occurred during SVG conversion:");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected exceptions
            Console.WriteLine("An unexpected error occurred:");
            Console.WriteLine($"Message: {ex.Message}");
        }
    }
}
