using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Example: retrieve distinct categories from an external source.
            // In a real scenario, replace this with actual data retrieval logic.
            string[] categories = new string[]
            {
                "Category A",
                "Category B",
                "Category C",
                "Category D"
            };

            // Define visual parameters for the legend
            double startX = 1.0;      // inches from left page edge
            double startY = 1.0;      // inches from top page edge
            double boxSize = 0.3;     // width and height of color box
            double spacingY = 0.5;    // vertical spacing between entries
            double textOffsetX = 0.4; // offset of text from the color box

            // Define a set of colors for the categories
            string[] colors = new string[]
            {
                "#FF5733", // red-orange
                "#33FF57", // green
                "#3357FF", // blue
                "#FF33A8"  // pink
            };

            for (int i = 0; i < categories.Length; i++)
            {
                double pinX = startX;
                double pinY = startY + i * spacingY;

                // Draw a small rectangle that will serve as the color swatch
                long boxShapeId = page.DrawRectangle(pinX, pinY, boxSize, boxSize);
                Shape boxShape = page.Shapes.GetShape(boxShapeId);
                // Assign fill color (use modulo in case there are more categories than colors)
                boxShape.Fill.FillForegnd.Value = colors[i % colors.Length];

                // Add a text shape next to the color box
                double textPinX = pinX + textOffsetX;
                double textPinY = pinY;
                double textWidth = 2.0;   // enough width for the label
                double textHeight = 0.3;  // height for the label
                Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, categories[i]);

                // Optional: set text formatting (font size in inches, e.g., 0.12 inches ≈ 8.64 points)
                textShape.TextXForm.TxtHeight.Value = 0.12;
                textShape.TextXForm.TxtWidth.Value = textWidth;
                // Set text color to black for readability
                textShape.TextStyle = null; // ensure default style
                // No explicit font setting needed; default will be used
            }

            // Save the diagram as VSDX
            string outputPath = "LegendDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }