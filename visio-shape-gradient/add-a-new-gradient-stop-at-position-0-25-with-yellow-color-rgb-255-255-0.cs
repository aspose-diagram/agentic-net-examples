using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No suitable shape found in the diagram.");
            }

            // Enable gradient fill on the selected shape
            targetShape.Fill.FillPattern.Value = 25; // Gradient fill pattern identifier
            targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            // Set gradient direction; cast enum to int because .Value expects an integer
            targetShape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Remove any existing gradient stops and add a new stop at position 0.25 with yellow color
            targetShape.Fill.GradientFill.GradientStops.Clear();
            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.25, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Define output file path
            string outputPath = "output.vsdx";

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}