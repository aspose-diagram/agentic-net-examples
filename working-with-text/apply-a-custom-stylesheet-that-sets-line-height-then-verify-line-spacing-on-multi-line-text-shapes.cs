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

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a rectangle shape (returns the shape ID)
            // Use float literals as DrawRectangle expects float parameters
            long rectShapeId = page.DrawRectangle(pinX: 2.0f, pinY: 2.0f, width: 4.0f, height: 2.0f);

            // Retrieve the shape instance using the returned ID
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Add multi‑line text to the shape
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Line 1\nLine 2\nLine 3"));

            // Create a custom stylesheet (ID must be unique)
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;

            // Example character formatting (optional)
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0;
            charFormat.FontName.Value = "Calibri";
            charFormat.Size.Value = 12.0 / 72.0; // 12 pt in inches
            charFormat.Color.Value = "#000000";
            customStyle.Chars.Add(charFormat);

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to the page (apply to shape, line, and fill)
            page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

            // Set line spacing (SpLine) for the first paragraph of the shape
            // SpLine is measured in inches; here we set it to 0.2 inches
            double expectedLineSpacing = 0.2;
            rectShape.Paras[0].SpLine.Value = expectedLineSpacing;

            // Verify that the line spacing was applied correctly
            double actualLineSpacing = rectShape.Paras[0].SpLine.Value;
            if (Math.Abs(actualLineSpacing - expectedLineSpacing) > 0.0001)
            {
                throw new Exception($"Line spacing verification failed. Expected: {expectedLineSpacing}, Actual: {actualLineSpacing}");
            }
            else
            {
                Console.WriteLine($"Line spacing verified successfully: {actualLineSpacing} inches.");
            }

            // Save the diagram to a VSDX file
            diagram.Save("LineHeightDemo.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved as LineHeightDemo.vsdx");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}