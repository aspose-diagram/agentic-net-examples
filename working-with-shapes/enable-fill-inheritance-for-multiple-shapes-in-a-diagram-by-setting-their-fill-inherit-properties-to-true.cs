using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Copy inherited fill values to the shape's own fill cells
                    // This effectively enables the shape to use the inherited fill settings
                    shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                    shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                }
            }

            // Path for the modified diagram output
            string outputPath = "output.vsdx";

            // Save the updated diagram using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}