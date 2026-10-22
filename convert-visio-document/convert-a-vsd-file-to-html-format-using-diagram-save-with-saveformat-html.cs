using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input VSD file path> <output HTML file path>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified VSD file.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as HTML. The second argument must be a valid SaveFileFormat.
            diagram.Save(outputPath, SaveFileFormat.Html);

            // Release resources held by the Diagram object.
            diagram.Dispose();

            Console.WriteLine($"Successfully converted \"{inputPath}\" to HTML at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}