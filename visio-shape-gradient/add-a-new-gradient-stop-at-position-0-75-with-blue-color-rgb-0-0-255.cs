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
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
                throw new Exception("No shapes found on the page.");

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Add a new gradient stop at position 0.75 with blue color (RGB 0,0,255)
            // Position is expressed as a fraction (0 to 1) using MeasureConst.NUM
            // Color is specified as a hex string with MeasureConst.Undefined for the unit
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.75, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
