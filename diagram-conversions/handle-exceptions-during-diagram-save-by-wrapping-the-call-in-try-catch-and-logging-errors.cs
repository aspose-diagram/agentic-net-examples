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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Save the diagram to the specified output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during the save operation.
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            diagram?.Dispose();
        }
    }
}