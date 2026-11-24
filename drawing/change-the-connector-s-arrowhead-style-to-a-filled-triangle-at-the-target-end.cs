using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Identify connector shapes: connectors are 1‑D shapes.
                        if (shape.OneD)
                        {
                            // Set the target (end) arrowhead style to a filled triangle.
                            // Arrow style values are integer based; 4 corresponds to a filled triangle.
                            shape.Line.EndArrow.Value = 4;

                            // Optionally, you could also set the arrow size if desired:
                            // shape.Line.EndArrowSize.Value = ArrowSizeValue.Large; // Example size.

                            // Continue processing other connectors (remove break if only first is needed).
                        }
                    }
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Connector arrowhead style updated successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}