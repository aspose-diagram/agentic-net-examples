using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Add a text shape with white font color
        // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize(in inches)
        Shape textShape = page.AddText(5, 5, 2, 1, "Sample Text", "Arial", "#FFFFFF", 0.2);

        // Apply a black background to the text block to simulate an outline
        textShape.TextBlock.TextBkgnd.Value = "#000000";
        textShape.TextBlock.TextBkgndTrans.Value = 0; // fully opaque

        // Ensure the inner text is white via the Char collection
        textShape.Chars.Clear(); // remove any existing character formatting
        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
        ch.IX = 0;                     // apply to the whole text run
        ch.Color.Value = "#FFFFFF";   // white text color
        ch.Style.Value = StyleValue.Bold; // optional bold style for better contrast
        textShape.Chars.Add(ch);

        // Save the diagram as VSDX
        diagram.Save("TextOutline.vsdx", SaveFileFormat.Vsdx);
    }
}
