using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile> [prefix]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        // Optional prefix argument; default to "Prefix_".
        string prefix = args.Length >= 3 ? args[2] : "Prefix_";

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // The original task referenced an "EventData1" cell, which does not exist in the Aspose.Diagram API.
                    // The closest available event cell is "TheData". We will prepend the prefix to this cell's formula.
                    // Ensure the Event and TheData objects are not null before modifying.
                    if (shape.Event != null && shape.Event.TheData != null)
                    {
                        // Retrieve the existing formula; if null, treat as empty string.
                        string currentFormula = shape.Event.TheData.Ufe.F ?? string.Empty;

                        // Prepend the specified prefix.
                        shape.Event.TheData.Ufe.F = prefix + currentFormula;
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}