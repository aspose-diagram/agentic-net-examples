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

                // Access the first page (adjust index as needed)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace 1 with the target shape ID)
                Shape shape = page.Shapes.GetShape(1);

                // Ensure the shape has text
                if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                {
                    // Plain text of the shape
                    string plainText = shape.Text.Value.Text;

                    // Iterate over each character formatting entry
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // IX is the zero‑based character index within the shape's text
                        int charIndex = ch.IX;

                        // Guard against out‑of‑range indices
                        if (charIndex >= 0 && charIndex < plainText.Length)
                        {
                            char currentChar = plainText[charIndex];

                            // If the character is a digit, set a larger font size (e.g., 12 pt)
                            if (char.IsDigit(currentChar))
                            {
                                // Font size is specified in inches (points / 72)
                                ch.Size.Value = 12.0 / 72.0;
                            }
                            // Non‑numeric characters retain their existing size (no action needed)
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