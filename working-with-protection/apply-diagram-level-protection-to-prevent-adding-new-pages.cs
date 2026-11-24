using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
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
                // ------------------------------------------------------------
                // Apply diagram-level protection.
                // Aspose.Diagram does not expose a specific property to
                // prevent adding new pages. The closest available protection
                // is to lock backgrounds, masters, shapes and styles at the
                // document level. This will restrict many editing actions.
                // ------------------------------------------------------------
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Save the protected diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with protection to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}