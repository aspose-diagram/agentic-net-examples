using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (adjust selection logic as needed)
            Shape shape = page.Shapes[0];

            // Ensure the shape is not marked for deletion
            if (shape.Del == BOOL.False)
            {
                // Center the text vertically within the shape
                shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                // Center the text horizontally within the shape if at least one paragraph exists
                if (shape.Paras.Count > 0)
                {
                    // Use the correct enum member for horizontal centering
                    shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                }
            }

            // Save the modified diagram using the appropriate SaveFileFormat
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}