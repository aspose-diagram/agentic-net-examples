using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Define rectangle dimensions (in inches)
                double rectPinX = 5.0;   // Center X position
                double rectPinY = 5.0;   // Center Y position
                double rectWidth = 4.0; // Width of the rectangle
                double rectHeight = 2.0; // Height of the rectangle

                // Add a rectangle shape using the built‑in "Rectangle" master
                long rectShapeId = page.AddShape(rectPinX, rectPinY, rectWidth, rectHeight, "Rectangle");

                // Retrieve the shape object for further modifications
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Clear any existing text (if any) and add a long sentence
                rectShape.Text.Value.Clear();
                string longText = "This is a very long sentence that should automatically wrap inside the rectangle shape to improve readability and demonstrate text wrapping functionality.";
                rectShape.Text.Value.Add(new Txt(longText));

                // Enable text wrapping by setting the TextBlock's "TextBkgnd" cell to a non‑empty value.
                // In Visio, the presence of a background color forces the text block to wrap.
                // Using a transparent background keeps the visual appearance unchanged.
                rectShape.TextBlock.TextBkgnd.Value = "#00000000"; // Transparent background (hex ARGB)

                // Optionally, adjust the text block margins to give some padding
                rectShape.TextBlock.LeftMargin.Value = 0.1;   // 0.1 inch left margin
                rectShape.TextBlock.RightMargin.Value = 0.1;  // 0.1 inch right margin
                rectShape.TextBlock.TopMargin.Value = 0.05;   // 0.05 inch top margin
                rectShape.TextBlock.BottomMargin.Value = 0.05; // 0.05 inch bottom margin

                // Save the diagram to a VSDX file
                string outputPath = "WrappedTextRectangle.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }