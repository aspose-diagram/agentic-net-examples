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
            // Configure custom font folder and set default font for the diagram
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            FontConfigs.DefaultFontName = "CustomFont";

            // Create a new empty diagram and add a single page
            Diagram diagram = new Diagram();
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Define diamond vertices (closed polyline) centered at (5,5) with size 2x2
            double[] diamondPoints = new double[]
            {
                5, 4,   // top point
                6, 5,   // right point
                5, 6,   // bottom point
                4, 5,   // left point
                5, 4    // close back to top
            };
            // Draw the diamond shape and obtain its ID
            long diamondId = page.DrawPolyline(diamondPoints);
            // Retrieve the shape object using the returned ID
            Shape diamond = page.Shapes.GetShape(diamondId);

            // Remove any existing text runs from the shape
            diamond.Text.Value.Clear();
            // Add a new centered text run
            diamond.Text.Value.Add(new Txt("Center Label"));

            // Create character formatting for the first text run
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // index of the character run
            ch.FontName.Value = "CustomFont"; // apply custom font
            ch.Size.Value = 12.0 / 72.0; // 12 pt converted to inches
            ch.Color.Value = "#000000"; // black color
            // Attach the character formatting to the shape
            diamond.Chars.Add(ch);

            // Position the text block at the center of the shape (relative 0‑1 coordinates)
            diamond.TextXForm.TxtPinX.Value = 0.5;
            diamond.TextXForm.TxtPinY.Value = 0.5;
            diamond.TextXForm.TxtLocPinX.Value = 0.5;
            diamond.TextXForm.TxtLocPinY.Value = 0.5;

            // Ensure vertical alignment is middle
            diamond.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
            // If a paragraph exists, set its horizontal alignment to center
            if (diamond.Paras.Count > 0)
            {
                diamond.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // Save the diagram as a VSDX file
            diagram.Save("DiamondLabel.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}