using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram and get a reference to it
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Define the diamond shape points as a flat double array (x1, y1, x2, y2, ...)
            double[] diamondPoints = new double[]
            {
                2.0, 0.0,   // Top
                4.0, 2.0,   // Right
                2.0, 4.0,   // Bottom
                0.0, 2.0    // Left
            };

            // Draw the diamond shape; the method returns the shape ID
            long shapeId = page.DrawPolyline(diamondPoints);

            // Retrieve the shape object using its ID
            Shape diamondShape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add new text to the shape
            diamondShape.Text.Value.Clear();
            diamondShape.Text.Value.Add(new Txt("Diamond"));

            // Ensure there is at least one paragraph to set alignment on
            if (diamondShape.Paras.Count == 0)
            {
                // Add a default paragraph if none exist
                diamondShape.Paras.Add(new Para());
            }

            // Set horizontal alignment of the paragraph to center
            diamondShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // Set vertical alignment of the text block to middle
            diamondShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Save the diagram to a VSDX file
            diagram.Save("Diamond.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}