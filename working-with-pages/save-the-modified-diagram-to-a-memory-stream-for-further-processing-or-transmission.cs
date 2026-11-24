using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // TODO: Apply any modifications to the diagram here.
                // Example: diagram.Title = "Modified Diagram";

                // Save the diagram to a memory stream for further processing.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save using the VSDX format directly to the stream.
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                    // Reset stream position if the caller needs to read from the beginning.
                    memoryStream.Position = 0;

                    // Example output indicating success and stream size.
                    Console.WriteLine($"Diagram successfully saved to memory stream. Size: {memoryStream.Length} bytes.");
                    
                    // The memoryStream can now be used for transmission or further processing.
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}