using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // TODO: Apply any desired modifications to the diagram here

                // Save the modified diagram into a memory stream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);
                    // Reset the stream position for further processing
                    memoryStream.Position = 0;
                    Console.WriteLine($"Diagram saved to memory stream. Length = {memoryStream.Length} bytes.");
                    // The memoryStream can now be used for further processing in a web service
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}