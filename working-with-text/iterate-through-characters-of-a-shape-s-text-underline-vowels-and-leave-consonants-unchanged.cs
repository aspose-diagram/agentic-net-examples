using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first shape that contains text
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is not deleted and has text
                    if (shape.Del == BOOL.False && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape with text found in the diagram.");
                }

                // Retrieve the plain text of the shape
                string plainText = targetShape.Text.Value.Text;

                // Clear existing text runs and character formatting
                targetShape.Text.Value.Clear();
                targetShape.Chars.Clear();

                // Add the full text back as a single Txt run
                targetShape.Text.Value.Add(new Txt(plainText));

                // Apply underline style to vowels
                for (int i = 0; i < plainText.Length; i++)
                {
                    char c = plainText[i];
                    bool isVowel = "AEIOUaeiou".IndexOf(c) >= 0;

                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = i; // zero‑based character index

                    if (isVowel)
                    {
                        // Underline vowel
                        ch.Style.Value = StyleValue.Underline;
                    }
                    else
                    {
                        // No special style for consonants
                        ch.Style.Value = StyleValue.Undefined;
                    }

                    targetShape.Chars.Add(ch);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }