using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an input file path was provided.
        string inputPath = args.Length > 0 ? args[0] : "";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the output file path; default to the same folder with a suffix.
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_inherit.vsdx");

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over every page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Copy the inherited fill pattern value to the shape's fill pattern.
                    // This effectively applies the inherited fill settings to the shape.
                    shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;

                    // Optionally copy other inherited fill properties (foreground, background colors).
                    shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                    shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                }
            }

            // Save the modified diagram to the output path using the VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Fill inheritance enabled and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}