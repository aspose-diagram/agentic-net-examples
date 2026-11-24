using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0) – do not use ActivePage
                Page page = diagram.Pages[0];

                // Draw a rectangle on the page
                // Parameters: pinX, pinY, width, height (all in inches)
                double rectPinX = 2.0;
                double rectPinY = 2.0;
                double rectWidth = 3.0;
                double rectHeight = 1.5;
                long rectId = page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);

                // Draw an ellipse on the page
                double ellipsePinX = 6.0;
                double ellipsePinY = 2.0;
                double ellipseWidth = 2.5;
                double ellipseHeight = 1.5;
                long ellipseId = page.DrawEllipse(ellipsePinX, ellipsePinY, ellipseWidth, ellipseHeight);

                // Retrieve the Shape objects for grouping
                Shape rectShape = page.Shapes.GetShape(rectId);
                Shape ellipseShape = page.Shapes.GetShape(ellipseId);

                // Group the rectangle and ellipse together
                Shape groupShape = page.Shapes.Group(new Shape[] { rectShape, ellipseShape });

                // Assign a common name to the group
                groupShape.Name = "MyGroup";

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}