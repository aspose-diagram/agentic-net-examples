using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Name of the shape to locate
            string targetShapeName = "MyShape";

            // Find the shape by its universal name
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == targetShapeName)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with name '{targetShapeName}' was not found.");
            }

            // Read LocPinY and PinY values
            double locPinY = targetShape.XForm.LocPinY.Value;
            double pinY = targetShape.XForm.PinY.Value;

            // Compute the absolute PinY (example calculation)
            double absolutePinY = pinY + locPinY;

            Console.WriteLine($"Shape '{targetShapeName}' LocPinY: {locPinY}");
            Console.WriteLine($"Shape '{targetShapeName}' PinY: {pinY}");
            Console.WriteLine($"Computed absolute PinY: {absolutePinY}");

            // Save the diagram (optional, no modifications made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
