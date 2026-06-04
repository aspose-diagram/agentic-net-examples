using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram that contains VBA macros.
            // The constructor automatically detects the format from the file extension.
            string inputPath = "input.vsdm";
            Diagram diagram = new Diagram(inputPath);

            // Create a memory stream to hold the saved diagram data.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the diagram (including macros) to the memory stream in VSDM format.
                diagram.Save(memoryStream, SaveFileFormat.Vsdm);

                // Reset the stream position to the beginning before reading/transmitting.
                memoryStream.Position = 0;

                // Example: obtain the byte array for network transmission.
                byte[] diagramBytes = memoryStream.ToArray();

                // Transmission code would go here (e.g., send via socket, HTTP, etc.).
            }

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
