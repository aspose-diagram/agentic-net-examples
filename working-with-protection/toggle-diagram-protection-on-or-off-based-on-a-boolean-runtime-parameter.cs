using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input file, output file, optional protect flag (true/false)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <input.vsdx> <output.vsdx> [protect:true|false]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool protect = false;

        if (args.Length >= 3)
        {
            if (!bool.TryParse(args[2], out protect))
            {
                Console.WriteLine("Invalid protect flag; defaulting to false.");
                protect = false;
            }
        }

        // Load the diagram from the specified file
        Diagram diagram = new Diagram(inputPath);

        // Toggle global protection settings based on the runtime flag
        diagram.DocumentSettings.ProtectBkgnds = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectMasters = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectShapes = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectStyles = protect ? BOOL.True : BOOL.False;

        // Save the modified diagram to the output file
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
