using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Optional: configure the folder that contains the custom TrueType font.
            // The second argument indicates whether to search sub‑folders.
            // Adjust the path to the location of your .ttf files.
            FontConfigs.SetFontFolder(@"C:\CustomFonts", true);
            // Set the default fallback font (used if the specified font is missing).
            FontConfigs.DefaultFontName = "MyCustomFont";

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Add a new page to the diagram.
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Draw a rectangle shape.
            // Parameters: pinX, pinY (center of the shape), width, height (all in inches).
            long rectangleId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the rectangle shape object.
            Shape rectangle = page.Shapes.GetShape(rectangleId);

            // Clear any existing text (if any) and add multiline text.
            rectangle.Text.Value.Clear();
            rectangle.Text.Value.Add(new Txt("First line\nSecond line\nThird line"));

            // Apply character formatting: custom TrueType font at 14 point size.
            // Font size in Aspose.Diagram is expressed in inches (points / 72).
            rectangle.Chars.Clear();
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0; // start index of the character run
            charFormat.FontName.Value = "MyCustomFont"; // name of the custom TrueType font
            charFormat.Size.Value = 14.0 / 72.0; // 14 points converted to inches
            rectangle.Chars.Add(charFormat);

            // Save the diagram to a VSDX file.
            diagram.Save("RectangleWithMultilineText.vsdx", SaveFileFormat.Vsdx);
        }
    }