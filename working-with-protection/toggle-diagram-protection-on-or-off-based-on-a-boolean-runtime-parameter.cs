using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input file path and a boolean flag indicating protection state.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioFile> <protect:true|false>");
            return;
        }

        string inputPath = args[0];
        bool protect;
        try
        {
            protect = bool.Parse(args[1]);
        }
        catch (Exception)
        {
            Console.WriteLine("Second argument must be 'true' or 'false'.");
            return;
        }

        // Load the diagram from the specified file.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Toggle global document protection cells.
        // TRUE enables protection, FALSE disables it.
        diagram.DocumentSettings.ProtectBkgnds = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectMasters = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectShapes = protect ? BOOL.True : BOOL.False;
        diagram.DocumentSettings.ProtectStyles = protect ? BOOL.True : BOOL.False;

        // Prepare output path.
        string outputPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(inputPath) ?? "",
            System.IO.Path.GetFileNameWithoutExtension(inputPath) + (protect ? "_protected" : "_unprotected") + ".vsdx");

        // Save the modified diagram.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'. Protection set to {protect}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
