using System;
using System.IO;
using Aspose.Diagram;

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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Set the EventShapeDataChanged equivalent.
            // The specific cell does not exist in the API, so we use EventXFMod as the closest substitute.
            // The formula invokes a custom script (e.g., a Visio Add-On) to recalculate dependent values.
            shape.Event.EventXFMod.Ufe.F = "RUNADDON(\"MyAddon\", \"Recalc\")";

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}