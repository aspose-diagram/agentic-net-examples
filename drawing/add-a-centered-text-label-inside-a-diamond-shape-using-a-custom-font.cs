using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Output file path for the generated diagram
        string outputPath = "DiamondWithLabel.vsdx";

        try
        {
            // -----------------------------------------------------------------
            // Create a new empty diagram. The default constructor creates a
            // diagram with a single page.
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram();

            // -----------------------------------------------------------------
            // Get the first page (index 0) where the shape will be added.
            // -----------------------------------------------------------------
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Define the points that form a diamond shape.
            // The shape will be centered around (5,5) with a width and height of 4.
            // DrawPolyline expects a flat double array: x1, y1, x2, y2, ...
            // -----------------------------------------------------------------
            double[] diamondPoints = new double[]
            {
                5, 3, // top vertex
                7, 5, // right vertex
                5, 7, // bottom vertex
                3, 5, // left vertex
                5, 3  // close the polygon
            };

            // -----------------------------------------------------------------
            // Draw the diamond shape on the page.
            // DrawPolyline returns the structural shape ID (long).
            // -----------------------------------------------------------------
            long shapeId = page.DrawPolyline(diamondPoints);

            // -----------------------------------------------------------------
            // Retrieve the shape object using the returned ID.
            // -----------------------------------------------------------------
            Shape shape = page.Shapes.GetShape(shapeId);

            // -----------------------------------------------------------------
            // Clear any existing text and add the custom label.
            // -----------------------------------------------------------------
            shape.Text.Value.Clear();
            string labelText = "Custom Label";
            shape.Text.Value.Add(new Txt(labelText));

            // -----------------------------------------------------------------
            // Ensure there is at least one paragraph for horizontal alignment.
            // -----------------------------------------------------------------
            if (shape.Paras.Count == 0)
            {
                shape.Paras.Add(new Para());
            }
            // Center the text horizontally within the shape.
            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // -----------------------------------------------------------------
            // Define a character run to set the custom font, size, and color.
            // -----------------------------------------------------------------
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            shape.Chars.Add(ch);
            ch.IX = 0; // associate with the first character run
            ch.FontName.Value = "Arial Unicode MS"; // custom font name
            ch.Color.Value = "#0000FF"; // blue color in hex
            ch.Size.Value = 12.0 / 72.0; // 12 pt converted to inches

            // -----------------------------------------------------------------
            // Center the text vertically within the shape.
            // -----------------------------------------------------------------
            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // -----------------------------------------------------------------
            // Position the text block at the shape's geometric center.
            // -----------------------------------------------------------------
            shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
            shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;

            // -----------------------------------------------------------------
            // Save the diagram to VSDX format.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}