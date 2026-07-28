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

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape to reset its text margins (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Create a zero‑margin value (0 inches)
            DoubleValue zeroMargin = new DoubleValue(0, MeasureConst.IN);

            // Reset the text block margins to their default (zero)
            shape.TextBlock.LeftMargin = zeroMargin;
            shape.TextBlock.RightMargin = zeroMargin;
            shape.TextBlock.TopMargin = zeroMargin;
            shape.TextBlock.BottomMargin = zeroMargin;

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
