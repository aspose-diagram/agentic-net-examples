using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, new line color (hex string, e.g., "#FF0000")
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <newLineColorHex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newLineColor = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    bool hasDashedLine = false;

                    // First pass: determine if any shape in the master uses a dashed line pattern
                    for (int i = 0; i < master.Shapes.Count; i++)
                    {
                        Shape shape = master.Shapes[i];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's line pattern is dashed
                        if (shape.Line.LinePattern.Value == LinePatternValue.Dash)
                        {
                            hasDashedLine = true;
                            break;
                        }
                    }

                    // If the master uses a dashed line style, update the line color of all its shapes
                    if (hasDashedLine)
                    {
                        for (int i = 0; i < master.Shapes.Count; i++)
                        {
                            Shape shape = master.Shapes[i];

                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Update the line color to the specified hex value
                            shape.Line.LineColor.Value = newLineColor;
                        }
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}