using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No visible shape found on the page.");
            }

            // Desired text orientation angle in degrees (modify as needed)
            double angleDeg = 0.0;
            double angleRad = (Math.PI / 180.0) * angleDeg;

            // Apply rotation to the text block
            targetShape.TextXForm.TxtAngle.Value = angleRad;

            // Position text at the bottom of the shape
            // Set the local pin Y to the height of the text block (so the bottom aligns)
            targetShape.TextXForm.TxtLocPinY.Value = targetShape.TextXForm.TxtHeight.Value;
            // Set the pin Y to 0 (bottom edge of the shape)
            targetShape.TextXForm.TxtPinY.Value = 0.0;

            // Optional: adjust bottom margin of the text block (e.g., 0.1 inches)
            targetShape.TextBlock.BottomMargin.Value = 0.1;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
