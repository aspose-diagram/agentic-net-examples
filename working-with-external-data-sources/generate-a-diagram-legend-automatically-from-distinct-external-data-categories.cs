using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Distinct external data categories to be displayed in the legend
            string[] categories = new string[]
            {
                "Category A",
                "Category B",
                "Category C",
                "Category D"
            };

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define legend box dimensions
            double legendX = 1.0; // left position (in inches)
            double legendY = 1.0; // top position (in inches)
            double legendWidth = 2.5; // width of the legend box
            double lineHeight = 0.3; // vertical space for each category entry
            double legendHeight = 0.2 + categories.Length * lineHeight; // total height including padding

            // Draw the legend background rectangle
            long rectId = page.DrawRectangle(legendX, legendY, legendWidth, legendHeight);
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Set rectangle fill (white) and border (black)
            rectShape.Fill.FillForegnd.Value = "#FFFFFF";
            rectShape.Line.LineColor.Value = "#000000";

            // Add a text entry for each category inside the legend box
            for (int i = 0; i < categories.Length; i++)
            {
                double textX = legendX + 0.1; // small left padding
                double textY = legendY + 0.1 + i * lineHeight; // position each line vertically
                double textW = legendWidth - 0.2; // width minus horizontal padding
                double textH = 0.25; // height of the text shape

                // AddText creates a shape with the specified text
                Shape txtShape = page.AddText(textX, textY, textW, textH, categories[i]);

                // Optional: set text color (black) if needed
                txtShape.Text.Value.Clear();
                txtShape.Text.Value.Add(new Txt(categories[i]));
                txtShape.Line.LineColor.Value = "#000000";
            }

            // Save the diagram with the legend to a VSDX file
            diagram.Save("LegendDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }