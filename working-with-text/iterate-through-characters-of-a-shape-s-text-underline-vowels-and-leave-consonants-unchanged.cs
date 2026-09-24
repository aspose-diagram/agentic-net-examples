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

            // Get the first shape on the first page
            Shape targetShape = null;
            foreach (Shape shp in diagram.Pages[0].Shapes)
            {
                targetShape = shp;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found in the diagram.");
                return;
            }

            // Retrieve plain text of the shape
            string plainText = targetShape.Text.Value.ToString();

            // Clear any existing character formatting
            targetShape.Chars.Clear();

            // Iterate through each character and apply underline to vowels
            for (int i = 0; i < plainText.Length; i++)
            {
                char c = plainText[i];
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = i; // character index

                // Check if the character is a vowel (case‑insensitive)
                if ("AEIOUaeiou".IndexOf(c) >= 0)
                {
                    ch.Style.Value = StyleValue.Underline;
                }
                else
                {
                    ch.Style.Value = StyleValue.Undefined;
                }

                targetShape.Chars.Add(ch);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with underlined vowels.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
