using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired text rotation (degrees) and bottom margin (inches)
            double angleDegrees = 0;      // No rotation; change as needed
            double bottomMargin = 0.1;    // Example bottom margin

            // Convert degrees to radians for the TxtAngle property
            double angleRadians = (Math.PI / 180) * angleDegrees;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the text orientation angle
                    shape.TextXForm.TxtAngle.Value = angleRadians;

                    // Position the text at the bottom of the shape
                    // TxtLocPinY defines the offset from the text block's bottom edge to its local pin
                    // Subtract the desired margin so the text sits above the bottom edge
                    shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value - bottomMargin;

                    // TxtPinY defines the distance from the shape's bottom edge to the text block origin
                    shape.TextXForm.TxtPinY.Value = bottomMargin;
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
