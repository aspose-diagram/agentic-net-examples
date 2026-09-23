using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Define position and size for the text shape (in inches)
        double pinX = 5.0;
        double pinY = 5.0;
        double width = 2.0;
        double height = 1.0;

        // Add a text shape with the desired content
        Shape textShape = page.AddText(pinX, pinY, width, height, "Outlined Text");

        // Ensure character formatting collection is empty before adding new formatting
        textShape.Chars.Clear();

        // Create a character formatting object for the text
        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
        ch.IX = 0;                         // Index of the first character
        ch.Color.Value = "#FFFFFF";        // White inner text color
        ch.Size.Value = 12.0 / 72.0;       // Font size: 12 pt converted to inches

        // Apply the character formatting to the shape
        textShape.Chars.Add(ch);

        // Simulate a one‑pixel black outline by setting a solid black background behind the text
        // (Visio renders this as an outline effect for white text)
        textShape.TextBlock.TextBkgnd.Ufe.F = "RGB(0,0,0)"; // Black background
        textShape.TextBlock.TextBkgndTrans.Value = 0;      // Fully opaque

        // Export the diagram to PNG to verify the result
        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
        diagram.Save("OutlinedText.png", saveOptions);
    }
}
