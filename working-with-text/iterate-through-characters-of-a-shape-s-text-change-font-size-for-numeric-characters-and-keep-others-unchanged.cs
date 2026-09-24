using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and a shape on that page (adjust IDs as needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // replace 1 with the actual shape ID

            // Ensure the shape contains text
            if (shape != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
            {
                string plainText = shape.Text.Value.Text;
                double numericFontSizeInches = 14.0 / 72.0; // 14 pt in inches

                // Iterate over each character in the plain text
                for (int i = 0; i < plainText.Length; i++)
                {
                    char currentChar = plainText[i];

                    // Find the corresponding Char object by its index (IX)
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        if (ch.IX == i)
                        {
                            // If the character is a digit, change its font size
                            if (char.IsDigit(currentChar))
                            {
                                ch.Size.Value = numericFontSizeInches;
                            }
                            // Non‑numeric characters retain their existing size
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
