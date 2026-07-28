using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without text
                    string fullText = shape.Text.Value.Text;
                    if (string.IsNullOrWhiteSpace(fullText))
                        continue;

                    // Split the shape's text into paragraphs (lines)
                    string[] paragraphs = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                    // Clear any existing character formatting
                    shape.Chars.Clear();

                    // Apply font size based on paragraph length
                    int charIndex = 0;
                    foreach (string para in paragraphs)
                    {
                        int length = para.Length;

                        // Base font size 12pt, increase 1pt for each 10 characters
                        double fontSizePoints = 12 + (length / 10);
                        double fontSizeInches = fontSizePoints / 72.0; // Aspose.Diagram uses inches for size

                        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                        ch.IX = charIndex++;                     // character run index
                        ch.FontName.Value = "Calibri";           // choose a common font
                        ch.Size.Value = fontSizeInches;          // set calculated size
                        ch.Color.Value = "#000000";              // black text

                        shape.Chars.Add(ch);
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
