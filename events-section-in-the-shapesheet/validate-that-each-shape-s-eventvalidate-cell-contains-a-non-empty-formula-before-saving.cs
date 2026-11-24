using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
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
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // TODO: The Aspose.Diagram API does not expose an EventValidate cell.
                    // If such a cell exists, it would be accessed via the shape.Event object.
                    // Replace the following placeholder with the correct property when available.
                    // Example (hypothetical):
                    // string formula = shape.Event.EventValidate.Ufe.F;
                    // if (string.IsNullOrWhiteSpace(formula))
                    // {
                    //     Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.ID} has an empty EventValidate formula.");
                    // }

                    // Since the API reference does not define EventValidate, validation cannot be performed here.
                }
            }

            // Save the diagram after (hypothetical) validation.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}