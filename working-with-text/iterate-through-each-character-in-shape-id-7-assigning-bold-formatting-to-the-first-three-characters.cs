using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with ID 7 across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == 7L)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("Shape with ID 7 was not found in the diagram.");
                return;
            }

            // Iterate over the characters in the shape and apply bold formatting
            // to the first three characters (or fewer if the shape has less than three).
            int charCount = targetShape.Chars.Count;
            int limit = Math.Min(3, charCount);
            for (int i = 0; i < limit; i++)
            {
                // Set the style of the character to Bold.
                targetShape.Chars[i].Style.Value = StyleValue.Bold;
            }

            // Save the modified diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}