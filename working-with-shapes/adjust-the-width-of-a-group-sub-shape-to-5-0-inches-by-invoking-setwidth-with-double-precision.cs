using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the target sub‑shape was found
            bool subShapeAdjusted = false;

            // Iterate through all pages to locate the sub‑shape inside a group
            foreach (Page page in diagram.Pages)
            {
                // Attempt to retrieve the sub‑shape by name, searching within groups as well
                // Replace "SubShapeName" with the actual NameU of the target sub‑shape
                Shape subShape = page.Shapes.GetShapeIncludingChild("SubShapeName");
                if (subShape != null && subShape.Del == BOOL.False)
                {
                    // Adjust the width of the sub‑shape to 5.0 inches using SetWidth
                    subShape.SetWidth(5.0);
                    subShapeAdjusted = true;
                    break; // Assuming only one such shape needs adjustment
                }
            }

            if (!subShapeAdjusted)
            {
                Console.Error.WriteLine("Target sub‑shape not found or could not be adjusted.");
                // Continue to save the diagram unchanged if desired, or exit
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}