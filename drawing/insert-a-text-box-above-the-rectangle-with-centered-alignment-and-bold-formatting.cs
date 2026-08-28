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
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (avoid using ActivePage)
            Page page = diagram.Pages[0];

            // Locate the rectangle shape on the page
            Shape rectangle = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False && shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangle = shape;
                    break;
                }
            }

            if (rectangle == null)
            {
                throw new Exception("Rectangle shape not found on the page.");
            }

            // Calculate a position just above the rectangle
            double rectPinX = rectangle.XForm.PinX.Value;
            double rectPinY = rectangle.XForm.PinY.Value;
            double rectHeight = rectangle.XForm.Height.Value;

            // Position the new text box (0.5 inch above the rectangle)
            double textPinX = rectPinX;
            double textPinY = rectPinY + rectHeight / 2 + 0.5;

            // Define size of the text box
            double textWidth = 2.0;   // inches
            double textHeight = 0.5;  // inches

            // Add a text shape (text box) to the page
            Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, "Sample Text");

            // Center-align the text within the text box
            if (textShape.Paras.Count > 0)
            {
                textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // Apply bold formatting to the text
            textShape.Chars.Clear();
            Aspose.Diagram.Char boldChar = new Aspose.Diagram.Char();
            boldChar.IX = 0; // character index
            boldChar.Style.Value = StyleValue.Bold;
            textShape.Chars.Add(boldChar);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
