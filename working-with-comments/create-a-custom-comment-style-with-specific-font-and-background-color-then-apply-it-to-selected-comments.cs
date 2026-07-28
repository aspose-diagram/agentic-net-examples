using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first page (default page is created automatically)
            Page page = diagram.Pages[0];

            // Draw a simple rectangle shape on the page
            // Parameters: pinX, pinY, width, height
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;
            long rectShapeId = page.DrawRectangle(pinX, pinY, pinX + width, pinY + height);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Add some text to the rectangle (this will be the visible comment text)
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Review this item"));

            // -----------------------------------------------------------------
            // Create a custom style sheet that defines the desired font and
            // background color for the comment appearance.
            // -----------------------------------------------------------------
            StyleSheet commentStyle = new StyleSheet();
            commentStyle.ID = diagram.StyleSheets.Count + 1;
            commentStyle.Name = "CustomCommentStyle";

            // ----- Character (font) formatting -----
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0; // index of the character run
            charFormat.FontName.Value = "Calibri";          // specific font
            charFormat.Color.Value = "#FFFFFF";            // white text color
            charFormat.Size.Value = 12.0 / 72.0;            // 12 pt in inches
            commentStyle.Chars.Add(charFormat);

            // ----- Fill (background) formatting -----
            commentStyle.Fill.FillForegnd.Value = "#007ACC"; // background color (blue)
            commentStyle.Fill.FillPattern.Value = 1;        // solid fill

            // Add the style sheet to the diagram
            diagram.StyleSheets.Add(commentStyle);

            // Apply the custom style to the rectangle shape
            // This gives the shape the desired font and background appearance.
            rectShape.TextStyle = commentStyle;
            rectShape.FillStyle = commentStyle;
            rectShape.LineStyle = commentStyle;

            // -----------------------------------------------------------------
            // Add a comment (annotation) to the shape.
            // The comment itself will inherit the visual appearance from the
            // shape because we are using the shape as a visual comment holder.
            // -----------------------------------------------------------------
            page.AddComment(rectShape, "Please verify the dimensions.");

            // -----------------------------------------------------------------
            // Save the diagram to a VSDX file to inspect the result.
            // -----------------------------------------------------------------
            string outputPath = "CustomCommentStyle.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }