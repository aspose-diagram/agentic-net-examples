using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Configure custom TrueType font folder and set the default font name
        string fontFolder = @"C:\CustomFonts";
        FontConfigs.SetFontFolder(fontFolder, true);
        FontConfigs.DefaultFontName = "MyCustomFont";

        // Create a new diagram
        Diagram diagram = new Diagram();

        // Access the first page (default page)
        Page page = diagram.Pages[0];

        // Add a rectangle shape (centered at 5,5 inches, size 4x2 inches)
        double pinX = 5.0;
        double pinY = 5.0;
        double width = 4.0;
        double height = 2.0;
        long rectId = page.DrawRectangle(pinX, pinY, width, height);
        Shape rect = page.Shapes.GetShape(rectId);

        // Insert multiline text into the rectangle
        rect.Text.Value.Clear();
        rect.Text.Value.Add(new Cp(0));
        rect.Text.Value.Add(new Txt("First line\nSecond line\nThird line"));

        // Apply character formatting: custom font, 14‑point size
        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
        ch.IX = 0;
        ch.FontName.Value = "MyCustomFont";
        ch.Size.Value = 14.0 / 72.0; // convert points to inches
        ch.Color.Value = "#000000";
        rect.Chars.Add(ch);

        // Save the diagram
        diagram.Save("RectangleWithText.vsdx", SaveFileFormat.Vsdx);
    }
}
