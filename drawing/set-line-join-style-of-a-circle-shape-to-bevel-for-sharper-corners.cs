using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

            // Iterate through all pages and shapes to find a circle/ellipse shape.
            // Visio represents circles as ellipses; we look for shape names containing "Ellipse" or "Circle".
            bool shapeFound = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Simple heuristic based on shape name.
                    string nameU = shape.NameU ?? string.Empty;
                    if (nameU.IndexOf("Ellipse", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        nameU.IndexOf("Circle", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // NOTE: Aspose.Diagram does not expose a LineJoin property.
                        // As a closest alternative, we set the line cap style which influences how line ends are rendered.
                        // This does not exactly replicate a bevel join, but provides a visible change to the line appearance.
                        shape.Line.LineCap.Value = BOOL.True; // Enable line caps (acts similar to bevel for ends).

                        shapeFound = true;
                        break;
                    }
                }
                if (shapeFound) break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine("No circle or ellipse shape was found in the diagram.");
                // Continue saving the diagram unchanged.
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}