using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page; create a new one if necessary
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Add a blank page and use it
                    diagram.Pages.Add(new Page());
                    page = diagram.Pages[0];
                }

                // Define rectangle geometry (center position in inches)
                double pinX = 5.0;   // horizontal center
                double pinY = 5.0;   // vertical center
                double width = 3.0;  // rectangle width
                double height = 2.0; // rectangle height

                // Add a rectangle shape; DrawRectangle returns the shape ID (long)
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);

                // Retrieve the newly added shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add the desired diagonal text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Diagonal Text"));

                // Rotate the text 45 degrees (TxtAngle expects radians)
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                shape.TextXForm.TxtAngle.Value = angleRad;

                // Create a margin of 4 points and apply to all sides of the text block
                DoubleValue margin = new DoubleValue(4, MeasureConst.PT);
                shape.TextBlock.LeftMargin = margin;
                shape.TextBlock.RightMargin = margin;
                shape.TextBlock.TopMargin = margin;
                shape.TextBlock.BottomMargin = margin;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}