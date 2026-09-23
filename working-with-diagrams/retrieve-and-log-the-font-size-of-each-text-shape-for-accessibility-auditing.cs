using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains visible text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                            // If the shape has character formatting, log each character's font size
                            if (shape.Chars != null && shape.Chars.Count > 0)
                            {
                                int charIndex = 0;
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    // Font size is stored in inches; convert to points (1 inch = 72 points)
                                    double sizeInInches = ch.Size.Value;
                                    double sizeInPoints = sizeInInches * 72.0;
                                    Console.WriteLine($"  Char {charIndex}: Font = {ch.FontName.Value}, Size = {sizeInPoints:F2} pt");
                                    charIndex++;
                                }
                            }
                            else
                            {
                                // No character-level formatting; attempt to infer size from the shape's default text style
                                // (If needed, additional logic can be added here)
                                Console.WriteLine("  No character formatting found.");
                            }
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
