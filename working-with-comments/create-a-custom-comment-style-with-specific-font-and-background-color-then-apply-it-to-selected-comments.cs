using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Desired comment style
            string fontName = "Calibri";
            string fontColor = "#FF0000";       // Red text
            string backgroundColor = "#FFFF00"; // Yellow fill

            // Iterate through all comments (annotations) on the page
            foreach (Annotation comment in page.PageSheet.Annotations)
            {
                // Example selection criteria: apply to every comment.
                // If you need specific filtering, add conditions here.

                // Retrieve the shape associated with the comment (if any)
                int shapeId = comment.ShapeID;
                Shape shape;
                try
                {
                    shape = page.Shapes.GetShape(shapeId);
                }
                catch
                {
                    // No associated shape; skip this comment
                    continue;
                }

                // Apply background fill to the shape representing the comment
                shape.Fill.FillPattern.Value = 1;               // Solid fill
                shape.Fill.FillForegnd.Value = backgroundColor; // Background color

                // Replace the shape's text with the comment text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(comment.Comment.Value));

                // Apply font formatting to the first character run
                shape.Chars.Clear();
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0;
                ch.FontName.Value = fontName;
                ch.Color.Value = fontColor;
                ch.Size.Value = 12.0 / 72.0; // 12 pt expressed in inches
                shape.Chars.Add(ch);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
