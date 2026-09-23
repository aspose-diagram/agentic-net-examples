using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, protect flag (true/false)
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <protect:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool protectFlag;

        if (!bool.TryParse(args[2], out protectFlag))
        {
            Console.WriteLine("Invalid protect flag. Use true or false.");
            return;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Set global protection flags based on the runtime parameter
        BOOL flag = protectFlag ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectBkgnds = flag;
        diagram.DocumentSettings.ProtectMasters = flag;
        diagram.DocumentSettings.ProtectShapes = flag;
        diagram.DocumentSettings.ProtectStyles = flag;

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
