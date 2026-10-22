using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Perform any desired operations on the diagram here.
            // (For this example, we simply save it unchanged.)

            // Save the diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Catch any exception (including Aspose.Diagram specific ones) and rethrow with additional context.
            string message = $"Failed to process Visio file '{inputPath}'. See inner exception for details.";
            // Preserve the original exception as the inner exception.
            throw new Exception(message, ex);
        }
    }
}