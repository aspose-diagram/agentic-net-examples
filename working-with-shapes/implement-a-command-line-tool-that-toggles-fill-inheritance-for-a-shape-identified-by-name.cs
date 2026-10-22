using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape name, output diagram path
        if (args.Length < 3)
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

            // Locate the shape by its NameU property (case‑sensitive)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

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
                Console.Error.WriteLine($"Shape with name '{shapeName}' not found.");
                return;
            }

            // Toggle fill inheritance
            // When inherited, the formula is "INHERITED()"
            string currentFormula = targetShape.Fill.FillForegnd.Ufe.F;

            if (!string.IsNullOrEmpty(currentFormula) &&
                currentFormula.Equals("INHERITED()", StringComparison.OrdinalIgnoreCase))
            {
                // Currently inherited – disable inheritance by assigning the effective fill color
                string effectiveValue = targetShape.Fill.FillForegnd.Value; // resolved color (e.g., "#FF0000")
                targetShape.Fill.FillForegnd.Ufe.F = effectiveValue;
                Console.WriteLine($"Fill inheritance disabled for shape '{shapeName}'. New fill value: {effectiveValue}");
            }
            else
            {
                // Currently explicit – enable inheritance
                targetShape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                Console.WriteLine($"Fill inheritance enabled for shape '{shapeName}'.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}