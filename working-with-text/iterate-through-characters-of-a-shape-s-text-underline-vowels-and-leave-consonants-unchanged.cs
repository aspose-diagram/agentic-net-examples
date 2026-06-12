using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page that contains text
                // (adjust the shape ID or selection logic as needed)
                Shape shape = page.Shapes.GetShape(1);

                // Get the plain text of the shape
                string plainText = shape.Text.Value.ToString();

                // Clear existing text runs and character formatting
                shape.Text.Value.Clear();
                shape.Chars.Clear();

                // Iterate through each character, add a text run and apply underline to vowels
                for (int i = 0; i < plainText.Length; i++)
                {
                    char ch = plainText[i];

                    // Add a character position marker (Cp) before the character
                    shape.Text.Value.Add(new Cp(i));

                    // Add the character as a text run (Txt)
                    shape.Text.Value.Add(new Txt(ch.ToString()));

                    // Create a Char object to define formatting for this character
                    Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                    charFormat.IX = i; // zero‑based index of the character

                    // Check if the character is a vowel (case‑insensitive)
                    if ("AEIOUaeiou".IndexOf(ch) >= 0)
                    {
                        // Apply underline style to vowels
                        charFormat.Style.Value = StyleValue.Underline;
                    }
                    else
                    {
                        // No special style for consonants
                        charFormat.Style.Value = StyleValue.Undefined;
                    }

                    // Add the Char formatting to the shape
                    shape.Chars.Add(charFormat);
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Processing complete. Modified diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }