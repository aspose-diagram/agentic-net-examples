using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Load the diagram from file
        Diagram diagram = new Diagram(inputPath);

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Set up auto‑spacing options (distance in inches)
        AutoSpaceOptions spacingOptions = new AutoSpaceOptions
        {
            DistanceInHorizontal = 2,
            DistanceInVertical = 2
        };

        // Apply auto‑spacing to all shapes on the page inside a try/catch to capture any errors
        try
        {
            page.AutoSpaceShapes(page.Shapes, spacingOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during auto‑spacing: {ex.Message}");
            return;
        }

        // NOTE: Aspose.Diagram does not provide a Validate method. 
        // The spacing operation is assumed successful if no exception was thrown.

        // Path for the output Visio file
        string outputPath = "output.vsdx";
        // Verify the output directory is writable (optional guard)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Save the updated diagram using the correct overload with SaveFileFormat
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}