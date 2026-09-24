using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape that will hold the paragraph text
            // Parameters: PinX, PinY, Width, Height (all in inches)
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 4.0;
            double height = 2.0;
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Sample paragraph text (you can replace this with any text)
            string paragraphText = "This is a sample paragraph whose length will determine the font size applied to it.";
            // Clear any existing text and add the new paragraph
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(paragraphText));

            // Ensure there is at least one character formatting entry
            shape.Chars.Clear();
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // character index
            shape.Chars.Add(ch);

            // Determine font size based on paragraph length
            // Base size = 0.2 inches (~14pt), increase 0.01 inch per character
            int textLength = paragraphText.Length;
            double baseSizeInches = 0.2;
            double sizeIncrementPerChar = 0.01;
            double calculatedSize = baseSizeInches + (textLength * sizeIncrementPerChar);

            // Apply the calculated font size to the character formatting
            ch.Size.Value = calculatedSize;
            // Optionally set a default font name
            ch.FontName.Value = "Calibri";

            // Save the diagram to a VSDX file
            diagram.Save("ConditionalFormattingOutput.vsdx", SaveFileFormat.Vsdx);
        }
    }