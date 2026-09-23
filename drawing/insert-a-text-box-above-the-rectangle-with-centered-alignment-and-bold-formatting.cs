using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page
        Page page = diagram.Pages[0];

        // Add a rectangle shape
        double rectPinX = 5.0;
        double rectPinY = 5.0;
        double rectWidth = 2.0;
        double rectHeight = 1.0;
        long rectId = page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);
        Shape rectShape = page.Shapes.GetShape(rectId);

        // Calculate position for the text box above the rectangle
        double textPinX = rectPinX;
        double textPinY = rectPinY + rectHeight / 2 + 0.5; // offset above the rectangle
        double textWidth = rectWidth;
        double textHeight = 0.5;

        // Add a text box shape with the desired text
        Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, "Sample Text");

        // Center align the paragraph (horizontal alignment)
        if (textShape.Paras.Count > 0)
        {
            textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
        }

        // Apply bold formatting to the text
        Aspose.Diagram.Char boldChar = new Aspose.Diagram.Char();
        boldChar.IX = 0; // first character index
        boldChar.Style.Value = StyleValue.Bold;
        textShape.Chars.Add(boldChar);

        // Save the diagram
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
