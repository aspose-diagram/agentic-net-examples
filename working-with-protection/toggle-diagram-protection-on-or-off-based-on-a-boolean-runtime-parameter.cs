using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, protection flag (true/false)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <input.vsdx> <output.vsdx> <protect:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string protectArg = args[2];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse protection flag
        if (!bool.TryParse(protectArg, out bool protect))
        {
            Console.Error.WriteLine($"Invalid protection flag value: {protectArg}. Expected true or false.");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Toggle global document protection settings
                if (protect)
                {
                    // Enable protection
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                    diagram.DocumentSettings.ProtectMasters = BOOL.True;
                    diagram.DocumentSettings.ProtectShapes = BOOL.True;
                    diagram.DocumentSettings.ProtectStyles = BOOL.True;
                }
                else
                {
                    // Disable protection
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                    diagram.DocumentSettings.ProtectMasters = BOOL.False;
                    diagram.DocumentSettings.ProtectShapes = BOOL.False;
                    diagram.DocumentSettings.ProtectStyles = BOOL.False;
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram protection {(protect ? "enabled" : "disabled")} and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}