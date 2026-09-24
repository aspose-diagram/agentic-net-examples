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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Add some text to the shape
            shape.Text.Value.Add(new Txt("Sample paragraph text"));

            // Ensure there is at least one paragraph (Visio creates one by default)
            if (shape.Paras.Count == 0)
            {
                // Create a new paragraph if none exists (unlikely)
                shape.Paras.Add(new Para());
            }

            // Define paragraph spacing via the shape's paragraph cells
            // SpBefore and SpAfter are in inches
            double expectedSpBefore = 0.2; // inches
            double expectedSpAfter = 0.3;  // inches
            shape.Paras[0].SpBefore.Value = expectedSpBefore;
            shape.Paras[0].SpAfter.Value = expectedSpAfter;

            // Create a custom stylesheet (empty for this example, but required by the task)
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            // Example: set a simple character color in the stylesheet
            Aspose.Diagram.Char styleChar = new Aspose.Diagram.Char();
            styleChar.IX = 0;
            styleChar.Color.Value = "#0000FF"; // blue text
            customStyle.Chars.Add(styleChar);
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to the page (apply to text, line, fill styles)
            page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

            // Verify that the paragraph spacing was applied correctly
            double actualSpBefore = shape.Paras[0].SpBefore.Value;
            double actualSpAfter = shape.Paras[0].SpAfter.Value;

            if (Math.Abs(actualSpBefore - expectedSpBefore) > 0.0001)
            {
                throw new Exception($"SpBefore mismatch: expected {expectedSpBefore}, got {actualSpBefore}");
            }

            if (Math.Abs(actualSpAfter - expectedSpAfter) > 0.0001)
            {
                throw new Exception($"SpAfter mismatch: expected {expectedSpAfter}, got {actualSpAfter}");
            }

            Console.WriteLine("Paragraph spacing verified successfully.");

            // Save the diagram to a VSDX file
            diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
