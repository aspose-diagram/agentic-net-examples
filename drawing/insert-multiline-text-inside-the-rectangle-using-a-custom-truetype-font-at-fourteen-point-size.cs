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

            // Set the default font to the custom TrueType font name.
            // Ensure the font is installed or its folder is added to FontConfigs if needed.
            FontConfigs.DefaultFontName = "MyCustomFont";

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Get the first page of the diagram.
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page.
            // PinX and PinY specify the position of the shape's center.
            long rectangleId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the rectangle shape object using its ID.
            Shape rectangle = page.Shapes.GetShape(rectangleId);

            // Clear any existing text in the shape.
            rectangle.Text.Value.Clear();

            // Add multiline text (line breaks are represented by '\n').
            rectangle.Text.Value.Add(new Txt("First line\nSecond line\nThird line"));

            // Apply character formatting to use the custom font at 14 point size.
            // Font size is specified in inches (points / 72).
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0; // Index of the character run.
            charFormat.FontName.Value = "MyCustomFont";
            charFormat.Size.Value = 14.0 / 72.0; // 14 points → inches.
            rectangle.Chars.Add(charFormat);

            // Save the diagram to a VSDX file.
            diagram.Save("RectangleWithMultilineText.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
