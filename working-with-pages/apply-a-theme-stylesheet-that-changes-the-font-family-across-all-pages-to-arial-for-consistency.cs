using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file location.
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path where the themed diagram will be saved.
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Set the global default font to Arial for any missing font references.
            FontConfigs.DefaultFontName = "Arial";

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // If the shape contains character formatting, update each character's font.
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Assign Arial as the font name for the character.
                        ch.FontName.Value = "Arial";
                    }

                    // If the shape has no explicit characters but contains text, ensure the default font is applied.
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        // Add a character run covering the existing text with Arial font.
                        // Clear existing character formatting to avoid conflicts.
                        shape.Chars.Clear();

                        // Create a new character entry starting at index 0.
                        Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                        newChar.IX = 0; // Position index for the character run.
                        newChar.FontName.Value = "Arial";
                        shape.Chars.Add(newChar);
                    }
                }
            }

            // Save the modified diagram using the VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any exception details to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}