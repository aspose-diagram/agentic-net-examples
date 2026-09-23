using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Define distinct categories and their associated colors (hex strings)
            var categories = new List<(string Name, string Color)>
            {
                ("Category A", "#FF0000"), // Red
                ("Category B", "#00FF00"), // Green
                ("Category C", "#0000FF")  // Blue
            };

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // Legend layout parameters (in inches)
            double startX = 1.0;               // Left margin
            double startY = 1.0;               // Top margin
            double boxWidth = 0.5;             // Width of color box
            double boxHeight = 0.5;            // Height of color box
            double verticalSpacing = 0.2;      // Space between entries
            double textOffsetX = 0.2;          // Space between box and text
            double textWidth = 3.0;            // Width of text shape
            double textHeight = 0.5;           // Height of text shape

            // Iterate over categories and create legend entries
            for (int i = 0; i < categories.Count; i++)
            {
                double currentY = startY + i * (boxHeight + verticalSpacing);

                // Draw a filled rectangle as the color marker
                long rectId = page.DrawRectangle(startX, currentY, boxWidth, boxHeight);
                Shape rectShape = page.Shapes.GetShape(rectId);
                rectShape.Fill.FillForegnd.Value = categories[i].Color;

                // Add a text shape next to the rectangle
                double textX = startX + boxWidth + textOffsetX;
                Shape textShape = page.AddText(textX, currentY, textWidth, textHeight, categories[i].Name);
                // Optional: set text color (black) and ensure no background fill
                textShape.Fill.FillForegnd.Value = "#000000";
                textShape.Fill.FillBkgnd.Value = "#FFFFFF";
            }

            // Save the diagram with the legend
            diagram.Save("LegendDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }