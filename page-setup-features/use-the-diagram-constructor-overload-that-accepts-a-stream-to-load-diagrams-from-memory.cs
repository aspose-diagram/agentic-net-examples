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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the Visio file as a stream and load it into a Diagram instance.
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Use the Diagram constructor that accepts a Stream.
                Diagram diagram = new Diagram(stream);

                // Save the loaded diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully loaded from memory and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}