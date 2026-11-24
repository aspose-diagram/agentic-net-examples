using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output VDX path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputVdxPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the protected diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that document protection is enabled (example: ProtectShapes)
                if (diagram.DocumentSettings.ProtectShapes != BOOL.True)
                {
                    Console.Error.WriteLine("The diagram is not protected for shapes as expected.");
                    // Continue anyway; depending on requirements you might abort here
                }

                // Export the diagram to VDX format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram exported to VDX: {outputPath}");
            }

            // Reload the exported VDX to verify protection flags are retained
            using (Diagram reloaded = new Diagram(outputPath))
            {
                // Check the same protection flag
                if (reloaded.DocumentSettings.ProtectShapes == BOOL.True)
                {
                    Console.WriteLine("Verification succeeded: ProtectShapes flag retained after export.");
                }
                else
                {
                    Console.Error.WriteLine("Verification failed: ProtectShapes flag not retained after export.");
                }

                // Additional checks for other protection flags can be added similarly
                // Example:
                // if (reloaded.DocumentSettings.ProtectBkgnds == BOOL.True) { ... }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions and report
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}