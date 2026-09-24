using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a custom solid red fill color to the shape
                    shape.Fill.FillForegnd.Value = "#FF0000";

                    // Validate that the fill foreground color (used as ThemeColor) is not null or empty
                    if (string.IsNullOrEmpty(shape.Fill.FillForegnd.Value))
                    {
                        // Throw an exception to indicate validation failure for this shape
                        throw new Exception($"Shape ID {shape.ID} on page '{page.Name}' has a null ThemeColor.");
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("All shapes have a non‑null ThemeColor and the diagram was saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}