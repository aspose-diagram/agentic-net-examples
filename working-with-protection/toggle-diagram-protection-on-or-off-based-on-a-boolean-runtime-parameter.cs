using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file path, output file path, enableProtection (true/false)
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <enableProtection>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string enableProtectionArg = args[2];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the boolean parameter
        bool enableProtection;
        if (!bool.TryParse(enableProtectionArg, out enableProtection))
        {
            Console.Error.WriteLine($"Invalid boolean value for enableProtection: {enableProtectionArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Toggle global document protection based on the runtime flag
                if (enableProtection)
                {
                    // Enable protection for backgrounds, masters, shapes, and styles
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                    diagram.DocumentSettings.ProtectMasters = BOOL.True;
                    diagram.DocumentSettings.ProtectShapes = BOOL.True;
                    diagram.DocumentSettings.ProtectStyles = BOOL.True;
                }
                else
                {
                    // Disable protection for all mentioned aspects
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                    diagram.DocumentSettings.ProtectMasters = BOOL.False;
                    diagram.DocumentSettings.ProtectShapes = BOOL.False;
                    diagram.DocumentSettings.ProtectStyles = BOOL.False;
                }

                // Save the modified diagram preserving the original format (VSDX assumed)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram protection toggled successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}