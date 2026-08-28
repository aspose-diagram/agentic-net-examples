using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (index 0)
            Page page = diagram.Pages[0];

            // Define points for a diamond shape (top, right, bottom, left, back to top)
            double[] diamondPoints = new double[]
            {
                5.0, 6.0,   // Top
                6.0, 5.0,   // Right
                5.0, 4.0,   // Bottom
                4.0, 5.0,   // Left
                5.0, 6.0    // Close back to Top
            };

            // Draw the diamond using DrawPolyline (returns void)
            page.DrawPolyline(diamondPoints);

            // Retrieve the newly created shape (assume it has the highest ID)
            long maxId = 0;
            foreach (Shape s in page.Shapes)
            {
                if (s.ID > maxId)
                    maxId = s.ID;
            }

            Shape diamondShape = page.Shapes.GetShape(maxId);

            // Add centered text label inside the diamond
            diamondShape.Text.Value.Clear();
            diamondShape.Text.Value.Add(new Txt("Custom Label"));

            // Center the text block within the shape
            diamondShape.TextXForm.TxtPinX.Value = 0.5; // 50% width
            diamondShape.TextXForm.TxtPinY.Value = 0.5; // 50% height

            // Vertically center the text block
            diamondShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Apply custom font to the text via Char collection
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // start index
            ch.FontName.Value = "CustomFontName"; // replace with your font name
            ch.Size.Value = 12.0 / 72.0; // 12 pt in inches
            ch.Color.Value = "#000000"; // black text
            diamondShape.Chars.Add(ch);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }