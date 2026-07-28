using System;
using System.IO;
using Aspose.Diagram;

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
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a custom solid red fill to the shape's foreground
                    shape.Fill.FillForegnd.Value = "#FF0000";

                    // Validate that the shape now has a non‑empty fill color (ThemeColor does not exist)
                    if (string.IsNullOrEmpty(shape.Fill.FillForegnd.Value))
                    {
                        throw new Exception($"Shape ID {shape.ID} has an empty FillForegnd after color assignment.");
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}