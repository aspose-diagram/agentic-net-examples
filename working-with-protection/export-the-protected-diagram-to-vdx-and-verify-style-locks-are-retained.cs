using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <protected_diagram_path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder, .vdx extension).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_exported.vdx");

        try
        {
            // Load the protected diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Capture original protection settings.
                BOOL protectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
                BOOL protectMasters = diagram.DocumentSettings.ProtectMasters;
                BOOL protectShapes = diagram.DocumentSettings.ProtectShapes;
                BOOL protectStyles = diagram.DocumentSettings.ProtectStyles;

                // Export to VDX format.
                diagram.Save(outputPath, SaveFileFormat.Vdx);

                // Reload the exported diagram to verify protection settings.
                using (Diagram reloaded = new Diagram(outputPath))
                {
                    BOOL reloadedBkgnds = reloaded.DocumentSettings.ProtectBkgnds;
                    BOOL reloadedMasters = reloaded.DocumentSettings.ProtectMasters;
                    BOOL reloadedShapes = reloaded.DocumentSettings.ProtectShapes;
                    BOOL reloadedStyles = reloaded.DocumentSettings.ProtectStyles;

                    // Verify that each protection flag matches the original.
                    if (protectBkgnds != reloadedBkgnds ||
                        protectMasters != reloadedMasters ||
                        protectShapes != reloadedShapes ||
                        protectStyles != reloadedStyles)
                    {
                        Console.Error.WriteLine("Verification failed: Style lock (protection) settings were not retained after export.");
                        Console.Error.WriteLine($"Original - Bkgnds: {protectBkgnds}, Masters: {protectMasters}, Shapes: {protectShapes}, Styles: {protectStyles}");
                        Console.Error.WriteLine($"Exported - Bkgnds: {reloadedBkgnds}, Masters: {reloadedMasters}, Shapes: {reloadedShapes}, Styles: {reloadedStyles}");
                    }
                    else
                    {
                        Console.WriteLine("Export successful. All style lock (protection) settings are retained.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}