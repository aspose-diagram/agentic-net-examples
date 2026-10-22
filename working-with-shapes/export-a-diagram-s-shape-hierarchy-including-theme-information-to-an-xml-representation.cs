using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputXmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as VDX (Visio XML) which includes shape hierarchy and theme information
            diagram.Save(outputPath, SaveFileFormat.Vdx);

            Console.WriteLine($"Diagram exported successfully to XML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}