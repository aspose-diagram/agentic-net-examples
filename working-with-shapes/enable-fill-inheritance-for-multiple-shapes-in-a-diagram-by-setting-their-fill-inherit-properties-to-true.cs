using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // NOTE: Aspose.Diagram does not expose a Fill.Inherit boolean property.
                    // The inherited fill values are accessible via shape.InheritFill (read‑only).
                    // If explicit inheritance control is required, it must be handled via shape properties
                    // such as FillPattern, FillForegnd, etc., copying values from shape.InheritFill as needed.
                    // Here we simply ensure the shape's fill pattern matches the inherited pattern.
                    shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                }
            }

            // Save the modified diagram to the output file using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}