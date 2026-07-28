using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process shapes on the first page (index 0)
                Page page = diagram.Pages[0];

                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has text
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        string plainText = shape.Text.Value.Text;

                        // Iterate over each character in the text
                        for (int i = 0; i < plainText.Length; i++)
                        {
                            char c = plainText[i];

                            // Check if the character is a digit
                            if (char.IsDigit(c))
                            {
                                // Try to find an existing Char object for this index
                                Aspose.Diagram.Char targetChar = null;
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    if (ch.IX == i)
                                    {
                                        targetChar = ch;
                                        break;
                                    }
                                }

                                // Desired font size in points (e.g., 14 pt) converted to inches
                                double sizeInInches = 14.0 / 72.0;

                                if (targetChar != null)
                                {
                                    // Update the size of the existing character formatting
                                    targetChar.Size.Value = sizeInInches;
                                }
                                else
                                {
                                    // Create a new Char entry for this character index
                                    Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                                    newChar.IX = i;
                                    newChar.Size.Value = sizeInInches;
                                    shape.Chars.Add(newChar);
                                }
                            }
                            // Non‑numeric characters are left unchanged
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
