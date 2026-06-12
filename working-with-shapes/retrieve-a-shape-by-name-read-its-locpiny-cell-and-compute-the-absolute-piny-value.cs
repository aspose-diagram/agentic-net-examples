using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // The name of the shape to find (universal name)
            string targetShapeName = "MyShape";

            // Variable to hold the found shape
            Shape targetShape = null;

            // Search all pages for the shape with the specified NameU
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with name '{targetShapeName}' not found.");
            }

            // Read the LocPinY cell (local pin Y) and the PinY cell (absolute pin Y)
            double locPinY = targetShape.XForm.LocPinY.Value;   // local coordinate
            double pinY = targetShape.XForm.PinY.Value;        // absolute coordinate of the shape's pin

            // Compute the absolute Y position of the local pin
            double absoluteLocPinY = pinY + locPinY;

            // Output the result
            Console.WriteLine($"Shape '{targetShapeName}' LocPinY (local): {locPinY}");
            Console.WriteLine($"Shape '{targetShapeName}' PinY (shape pin): {pinY}");
            Console.WriteLine($"Computed absolute LocPinY: {absoluteLocPinY}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
