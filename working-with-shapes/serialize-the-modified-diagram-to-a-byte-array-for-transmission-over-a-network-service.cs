using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input diagram file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // TODO: Apply any modifications to the diagram here.
            // Example: diagram.Pages[0].Name = "ModifiedPage";

            // Serialize the diagram to a memory stream in VSDX format.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the diagram into the stream using the VSDX format.
                diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                // Convert the stream contents to a byte array for transmission.
                byte[] diagramBytes = memoryStream.ToArray();

                // Example usage: output the size of the serialized data.
                Console.WriteLine($"Diagram serialized to byte array. Size: {diagramBytes.Length} bytes.");

                // The byte array (diagramBytes) can now be sent over a network service.
                // For demonstration, you could write it to a file:
                // File.WriteAllBytes("output_bytes.bin", diagramBytes);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}