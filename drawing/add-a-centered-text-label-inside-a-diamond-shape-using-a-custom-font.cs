using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (use Pages collection, not ActivePage)
            Page page = diagram.Pages[0];

            // Add a diamond shape using the built‑in master named "Diamond"
            // PinX and PinY are the center coordinates of the shape on the page
            double pinX = 5.0; // inches
            double pinY = 5.0; // inches
            long diamondShapeId = page.AddShape(pinX, pinY, "Diamond");

            // Retrieve the shape object from its ID
            Shape diamond = page.Shapes.GetShape(diamondShapeId);

            // Clear any existing text and add the desired label
            diamond.Text.Value.Clear();
            diamond.Text.Value.Add(new Txt("Centered Label"));

            // Center the text block within the shape (relative coordinates 0.5 = 50%)
            diamond.TextXForm.TxtPinX.Value = 0.5;
            diamond.TextXForm.TxtPinY.Value = 0.5;

            // Align paragraph horizontally to center and vertically to middle
            if (diamond.Paras.Count > 0)
            {
                diamond.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }
            diamond.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Apply custom font formatting to the text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // character index (first character)
            ch.FontName.Value = "CustomFontName"; // replace with the actual font name installed on the system
            ch.Size.Value = 12.0 / 72.0; // font size in inches (12 pt)
            diamond.Chars.Add(ch);

            // Save the diagram to a VSDX file
            diagram.Save("DiamondLabel.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
