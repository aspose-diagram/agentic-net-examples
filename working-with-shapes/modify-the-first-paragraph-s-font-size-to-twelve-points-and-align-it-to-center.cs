using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape firstShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    firstShape = shape;
                    break;
                }
            }

            if (firstShape == null)
            {
                Console.WriteLine("No visible shape found on the first page.");
                return;
            }

            // Verify the shape contains at least one paragraph
            if (firstShape.Paras.Count == 0)
            {
                Console.WriteLine("The first shape does not contain any paragraphs.");
                return;
            }

            // Set the first paragraph's horizontal alignment to center
            firstShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // Ensure there is at least one character run for font size adjustment
            if (firstShape.Chars.Count == 0)
            {
                // Create a default character entry
                Aspose.Diagram.Char defaultChar = new Aspose.Diagram.Char();
                defaultChar.IX = 0;
                firstShape.Chars.Add(defaultChar);
            }

            // Set the font size of the first character (12 points = 12/72 inches)
            firstShape.Chars[0].Size.Value = 12.0 / 72.0;

            // Define output file path
            string outputPath = "output.vsdx";

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("First paragraph updated and diagram saved to " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}