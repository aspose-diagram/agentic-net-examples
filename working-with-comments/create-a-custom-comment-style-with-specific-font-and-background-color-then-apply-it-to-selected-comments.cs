using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Define position and size for a placeholder shape that will represent the comment background
                double pinX = 2.0;   // X coordinate (in inches)
                double pinY = 2.0;   // Y coordinate (in inches)
                double width = 3.0;  // Width (in inches)
                double height = 1.5; // Height (in inches)

                // Draw a rectangle shape that will serve as the visual background for the comment
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);
                Shape commentShape = page.Shapes.GetShape(shapeId);

                // Set the fill pattern to solid and apply a background color (e.g., light yellow)
                commentShape.Fill.FillPattern.Value = 1;               // Solid fill
                commentShape.Fill.FillForegnd.Value = "#FFFF99";      // Light yellow background

                // Remove any existing text and add the comment text
                commentShape.Text.Value.Clear();
                commentShape.Text.Value.Add(new Txt("Review this item"));

                // Apply character formatting: font, size, color, and bold style
                commentShape.Chars.Clear();
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0;                                   // Index of the character run
                ch.FontName.Value = "Arial";                 // Font name
                ch.Size.Value = 12.0 / 72.0;                 // Font size in inches (12 pt)
                ch.Color.Value = "#0000FF";                  // Text color (blue)
                ch.Style.Value = StyleValue.Bold;            // Bold style
                commentShape.Chars.Add(ch);

                // Add an annotation (comment) linked to the shape
                page.AddComment(commentShape, "This is a custom comment with styled background and font.");

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }