using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape name
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeName>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape by its NameU property
            Shape targetShape = null;
            Page targetPage = null;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the universal name of the shape
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        targetPage = page;
                        break;
                    }
                }

                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with name \"{targetShapeName}\" not found.");
                return;
            }

            // Read the LocPinY cell (local pin Y) value
            double locPinY = targetShape.XForm.LocPinY.Value;

            // Compute the absolute PinY value.
            // For a simple (non‑grouped) shape, the absolute PinY can be approximated as:
            //   absolutePinY = PinY + LocPinY
            // If the shape is inside a group, a more complex calculation would be required.
            double absolutePinY = targetShape.XForm.PinY.Value + locPinY;

            // Output the results
            Console.WriteLine($"Shape NameU: {targetShape.NameU}");
            Console.WriteLine($"LocPinY (local pin Y): {locPinY}");
            Console.WriteLine($"Computed absolute PinY: {absolutePinY}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}