using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                try
                {
                    // Save the diagram to the specified output path preserving all
                    // ActiveX controls, layout, and other configurations.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Error saving diagram: {saveEx.Message}");
                }
            }
        }
        catch (Exception loadEx)
        {
            Console.Error.WriteLine($"Error loading diagram: {loadEx.Message}");
        }
    }
}