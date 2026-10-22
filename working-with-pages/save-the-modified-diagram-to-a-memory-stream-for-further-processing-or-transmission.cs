using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // TODO: Apply any required modifications to the diagram here.

            // Save the diagram to a memory stream in VSDX format.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, SaveFileFormat.Vsdx);
                memoryStream.Position = 0; // Reset stream position for further processing.

                // Example output: report the size of the generated stream.
                Console.WriteLine($"Diagram saved to memory stream. Size: {memoryStream.Length} bytes.");

                // The memoryStream can now be used for further processing or transmission.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}