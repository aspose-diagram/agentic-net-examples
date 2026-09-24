using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with your actual file)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Find the first shape on the page to modify
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True) continue;

                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to modify.");
                return;
            }

            // Rotate the shape by 45 degrees (example rotation)
            targetShape.XForm.Angle.Value = 45.0;

            // Enable KeepTextFlat to keep the text horizontal despite rotation
            targetShape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}