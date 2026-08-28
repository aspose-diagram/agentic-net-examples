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

            // Load an existing Visio diagram (replace with your source)
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Perform any modifications to the diagram here.
            // For example, you could add shapes, change properties, etc.
            // -------------------------------------------------

            // Save the modified diagram into a memory stream in VDX format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Use the provided Save method that accepts a Stream and a SaveFileFormat
                diagram.Save(memoryStream, SaveFileFormat.Vdx);

                // Reset the stream position if you need to read from it later
                memoryStream.Position = 0;

                // Example usage of the memory stream (e.g., write to a file, send over network)
                // File.WriteAllBytes("output.vdx", memoryStream.ToArray());
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
