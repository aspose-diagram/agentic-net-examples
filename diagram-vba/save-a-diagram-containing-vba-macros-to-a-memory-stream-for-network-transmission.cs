using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram that contains VBA macros.
            // VSDM is the macro‑enabled Visio format.
            Diagram diagram = new Diagram("input.vsdm");

            // Create a memory stream that will hold the diagram data for transmission.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the diagram (including its macros) into the memory stream.
                // The Save method with a Stream and SaveFileFormat preserves all macro data.
                diagram.Save(memoryStream, SaveFileFormat.Vsdm);

                // Reset the stream position to the beginning if the data will be read afterwards.
                memoryStream.Position = 0;

                // Example: obtain the byte array to send over a network.
                byte[] diagramBytes = memoryStream.ToArray();

                // Network transmission logic would go here, using diagramBytes.
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
