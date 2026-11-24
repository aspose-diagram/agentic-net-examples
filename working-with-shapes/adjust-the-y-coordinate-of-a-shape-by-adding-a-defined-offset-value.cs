using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, shape name, Y offset, output file
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <shapeNameU> <yOffset> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string targetShapeName = args[1];
        string offsetStr = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse offset value
        if (!double.TryParse(offsetStr, out double yOffset))
        {
            Console.Error.WriteLine($"Invalid Y offset value: {offsetStr}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the target shape by NameU
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Compare the universal name of the shape
                    if (shape.NameU == targetShapeName)
                    {
                        // Adjust the Y coordinate (PinY) by adding the offset
                        shape.XForm.PinY.Value = shape.XForm.PinY.Value + yOffset;
                        shapeFound = true;
                        // Assuming shape names are unique; break out of loops
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with NameU '{targetShapeName}' not found.");
                return;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape Y coordinate adjusted by {yOffset}. Saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}