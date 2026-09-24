using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape (returns a shape ID). Use float literals as required by the API.
            long rectId = page.DrawRectangle(pinX: 2f, pinY: 2f, width: 4f, height: 2f);
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Add multi‑line text to the shape
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("First line\nSecond line\nThird line"));

            // Ensure there is at least one paragraph (Visio creates it automatically)
            if (rectShape.Paras.Count == 0)
                throw new Exception("Paragraph collection is empty; cannot set line spacing.");

            // Set line (paragraph) spacing – this controls line height (value in inches)
            rectShape.Paras[0].SpLine.Value = 0.2;

            // Create a custom stylesheet (optional – demonstrates applying a style)
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;

            // Example: set a character color in the stylesheet
            Aspose.Diagram.Char styleChar = new Aspose.Diagram.Char();
            styleChar.IX = 0;
            styleChar.Color.Value = "#0000FF"; // blue text
            customStyle.Chars.Add(styleChar);
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to the shape (text, line, and fill styles)
            rectShape.TextStyle = customStyle;
            rectShape.LineStyle = customStyle;
            rectShape.FillStyle = customStyle;

            // Verification: check that the line spacing was applied
            double appliedSpacing = rectShape.Paras[0].SpLine.Value;
            if (Math.Abs(appliedSpacing - 0.2) > 0.0001)
                throw new Exception($"Line spacing verification failed. Expected 0.2, got {appliedSpacing}");
            else
                Console.WriteLine($"Line spacing correctly set to {appliedSpacing} inches.");

            // Verification: ensure the shape contains multiple lines of text
            string plainText = rectShape.Text.Value.ToString();
            int lineCount = plainText.Split('\n').Length;
            if (lineCount < 2)
                throw new Exception("Multi‑line text verification failed. Expected at least 2 lines.");
            else
                Console.WriteLine($"Shape contains {lineCount} lines of text.");

            // Save the diagram to a VSDX file
            diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved as StyledDiagram.vsdx");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}