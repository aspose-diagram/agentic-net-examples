using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply global document protection settings
                // These protect backgrounds, masters, shapes, and styles.
                // Aspose.Diagram does not expose a direct property to prevent adding new pages,
                // so we apply all available document-level protections as the closest alternative.
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Save the protected diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with protection to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}