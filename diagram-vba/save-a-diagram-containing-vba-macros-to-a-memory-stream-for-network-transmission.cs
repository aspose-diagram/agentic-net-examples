using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the input Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a memory stream to hold the saved diagram.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the diagram in macro-enabled format (VSDM) to preserve VBA macros.
                    diagram.Save(memoryStream, SaveFileFormat.Vsdm);

                    // Reset the stream position to the beginning for any subsequent reading.
                    memoryStream.Position = 0;

                    // Example: output the size of the generated stream.
                    Console.WriteLine($"Diagram saved to memory stream. Size: {memoryStream.Length} bytes.");

                    // At this point, 'memoryStream' can be transmitted over the network.
                    // For demonstration, you could write it to a file (optional):
                    // File.WriteAllBytes("output.vsdm", memoryStream.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}