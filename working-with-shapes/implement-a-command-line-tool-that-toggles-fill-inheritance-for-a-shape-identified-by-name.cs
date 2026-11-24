using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape name, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by its universal name (NameU)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Compare the shape's NameU with the requested name
                    if (shape.NameU == shapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with name \"{shapeName}\" not found.");
                return;
            }

            // Determine current fill inheritance state
            // Inheritance is enabled when the Ufe.F string equals "INHERITED()"
            bool isInherited = string.Equals(targetShape.Fill.FillForegnd.Ufe.F, "INHERITED()", StringComparison.OrdinalIgnoreCase);

            if (isInherited)
            {
                // Disable inheritance: assign the current explicit value back to the cell
                // This breaks the inheritance link.
                targetShape.Fill.FillForegnd.Value = targetShape.Fill.FillForegnd.Value;
                Console.WriteLine($"Fill inheritance disabled for shape \"{shapeName}\".");
            }
            else
            {
                // Enable inheritance by setting the Ufe.F formula to INHERITED()
                targetShape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                Console.WriteLine($"Fill inheritance enabled for shape \"{shapeName}\".");
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}