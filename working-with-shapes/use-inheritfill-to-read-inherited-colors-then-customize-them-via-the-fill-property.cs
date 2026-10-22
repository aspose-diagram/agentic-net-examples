using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ----- Read inherited fill colors -----
                    // Inherited foreground (fill) color.
                    string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
                    // Inherited background color (if any).
                    string inheritedBackColor = shape.InheritFill.FillBkgnd.Value;

                    Console.WriteLine($"Shape ID {shape.ID}: Inherited FillForegnd = {inheritedForeColor}, Inherited FillBkgnd = {inheritedBackColor}");

                    // ----- Customize the shape's fill -----
                    // Example: set a solid red foreground fill.
                    shape.Fill.FillForegnd.Value = "#FF0000";

                    // Optionally, set a solid blue background fill.
                    shape.Fill.FillBkgnd.Value = "#0000FF";

                    // Ensure the fill pattern is solid (value 1).
                    shape.Fill.FillPattern.Value = 1;
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}