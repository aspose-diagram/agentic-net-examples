using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (a diagram always has at least one page)
            Page page = diagram.Pages[0];

            // Add a simple rectangle shape that we will comment on
            double pinX = 5.0;      // X coordinate of the shape centre
            double pinY = 5.0;      // Y coordinate of the shape centre
            double width = 2.0;     // Width of the rectangle (in inches)
            double height = 1.0;    // Height of the rectangle (in inches)

            // DrawRectangle returns the shape identifier (long)
            long rectId = page.DrawRectangle(pinX, pinY, width, height);
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Add a comment attached to the rectangle shape
            string commentText = "Review this shape";
            page.AddComment(rectShape, commentText);

            // Define the custom style for comments
            string customFontName = "Calibri";
            string customFontColor = "#FF0000";        // Red text
            string customBackgroundColor = "#FFFF00";  // Yellow fill

            // Apply the custom style to every comment on the page
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // The comment is rendered as a separate shape; retrieve it
                Shape commentShape = page.Shapes.GetShape(annotation.ShapeID);

                // Replace the existing text run with the annotation text
                commentShape.Text.Value.Clear();
                commentShape.Text.Value.Add(new Txt(annotation.Comment.Value));

                // Ensure there is at least one Char object for character formatting
                if (commentShape.Chars.Count == 0)
                {
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0;
                    commentShape.Chars.Add(ch);
                }

                // Apply font name and color to the first character run
                Aspose.Diagram.Char firstChar = commentShape.Chars[0];
                firstChar.FontName.Value = customFontName;
                firstChar.Color.Value = customFontColor;

                // Apply background fill to the comment shape
                commentShape.Fill.FillForegnd.Value = customBackgroundColor;
                commentShape.Fill.FillPattern.Value = 1; // Solid fill
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}