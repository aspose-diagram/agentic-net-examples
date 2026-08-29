using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

        // Guard: ensure the input Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape contains text (non‑null and not just whitespace)
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Retrieve the plain text of the shape
                        string plainText = shape.Text.Value.Text;

                        // Clear any existing character‑level formatting to start fresh
                        shape.Chars.Clear();

                        // Process each character individually
                        for (int i = 0; i < plainText.Length; i++)
                        {
                            // Current character from the shape's text
                            char currentChar = plainText[i];

                            // Create a new Aspose.Diagram.Char object for this character
                            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();

                            // Set the character index (IX) to match its position in the string
                            charFormat.IX = i;

                            // If the character is a digit, increase its font size (e.g., 14 pt)
                            if (char.IsDigit(currentChar))
                            {
                                // Font size is specified in inches (points ÷ 72)
                                charFormat.Size.Value = 14.0 / 72.0;
                            }
                            else
                            {
                                // For non‑numeric characters, keep a standard size (e.g., 10 pt)
                                charFormat.Size.Value = 10.0 / 72.0;
                            }

                            // Add the character formatting to the shape's Char collection
                            shape.Chars.Add(charFormat);
                        }
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}