using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Calculate the increment in inches (1 point = 1/72 inch)
            double pointIncrement = 2.0 / 72.0;

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify title shapes by checking the universal name (case‑insensitive)
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Increase the font size for every character run within the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Add the point increment to the existing size (size is stored in inches)
                            ch.Size.Value += pointIncrement;
                        }
                    }
                }
            }

            // Save the modified diagram to the output file using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}