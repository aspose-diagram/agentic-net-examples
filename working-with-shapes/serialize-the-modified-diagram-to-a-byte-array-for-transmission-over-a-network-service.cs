using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramSerializer
{
    // Serializes a Diagram object to a byte array using VDX format.
    public static byte[] SerializeDiagram(Diagram diagram)
    {
        // MemoryStream will hold the diagram data in memory.
        using (var memoryStream = new MemoryStream())
        {
            // Save the diagram to the stream. This uses the provided Save(Stream, SaveFileFormat) rule.
            diagram.Save(memoryStream, SaveFileFormat.Vdx);

            // Convert the stream contents to a byte array for transmission.
            return memoryStream.ToArray();
        }
    }

    // Example entry point demonstrating loading, modifying, and serializing a diagram.
    public static void Main()
    {
        try
        {

            // Load an existing diagram from a file (replace with your actual file path).
            Diagram diagram = new Diagram("input.vsdx");

            // ----- Perform any diagram modifications here -----
            // e.g., diagram.Pages[0].Shapes[0].Text.Value = "Updated Text";
            // -------------------------------------------------

            // Serialize the modified diagram to a byte array.
            byte[] diagramBytes = SerializeDiagram(diagram);

            // The byte array can now be sent over a network service.
            Console.WriteLine($"Diagram serialized to {diagramBytes.Length} bytes.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
