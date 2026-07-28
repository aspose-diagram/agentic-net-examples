using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramSerializer
{
    // Serializes the provided Diagram into a byte array (VDX format) for network transmission.
    public static byte[] SerializeDiagram(Diagram diagram)
    {
        // MemoryStream will hold the diagram data in memory.
        using (var memoryStream = new MemoryStream())
        {
            // Use DiagramSaveOptions to specify the VDX format.
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            // Save the diagram into the memory stream using the provided Save method.
            diagram.Save(memoryStream, saveOptions);

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Extract the byte array from the stream.
            return memoryStream.ToArray();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
