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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page
            Page page = diagram.Pages[0];

            // Get the first shape on the page (adjust as needed)
            Shape shape = page.Shapes[0];

            // Retrieve the plain text of the shape
            string text = shape.Text.Value.ToString();

            // If there is no text, nothing to process
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Shape contains no text.");
                return;
            }

            // Clear existing text runs and character formatting
            shape.Text.Value.Clear();
            shape.Chars.Clear();

            // Iterate through each character, add text runs and apply underline to vowels
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                // Mark the start of a new character formatting run
                shape.Text.Value.Add(new Cp(i));

                // Add the character as a text run
                shape.Text.Value.Add(new Txt(c.ToString()));

                // Create character formatting
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = i;

                // Check if the character is a vowel (case‑insensitive)
                if ("AEIOUaeiou".IndexOf(c) >= 0)
                {
                    // Apply underline style
                    ch.Style.Value = StyleValue.Underline;
                }
                else
                {
                    // No special style
                    ch.Style.Value = StyleValue.Undefined;
                }

                // Add the character formatting to the shape
                shape.Chars.Add(ch);
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
