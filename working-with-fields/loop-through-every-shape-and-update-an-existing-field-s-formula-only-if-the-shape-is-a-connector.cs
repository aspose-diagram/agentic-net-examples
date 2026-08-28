using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Ensure the shape contains at least one field
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Update the formula of the first field
                            Field field = shape.Fields[0];
                            // Assign a dynamic formula to the field (using the Val property for compatibility)
                            field.Value.Val = "Width*Height";
                        }
                    }
                }
            }

            // Save the modified diagram using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}