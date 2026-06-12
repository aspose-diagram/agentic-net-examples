using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramProcessor
{
    // Loads a Visio diagram, applies modifications, and returns the diagram as a byte array.
    public byte[] GetDiagramBytes(string sourceFilePath)
    {
        // Load the diagram from a file.
        Diagram diagram = new Diagram(sourceFilePath);

        // -------------------------------------------------
        // Place any diagram modifications here.
        // For example, you could add shapes, change properties, etc.
        // -------------------------------------------------

        // Save the modified diagram into a memory stream using VDX format.
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Use the Save method that accepts a Stream and a SaveFileFormat.
            diagram.Save(memoryStream, SaveFileFormat.Vdx);

            // Return the stream contents as a byte array for further processing.
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
