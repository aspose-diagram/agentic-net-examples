using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramHelper
{
    // Reads a VSDX diagram from a memory stream and returns its document title.
    public static string GetDiagramTitle(MemoryStream memoryStream)
    {
        // Ensure the stream is positioned at the beginning before loading.
        memoryStream.Position = 0;

        // Load the diagram from the provided stream using the Diagram(Stream) constructor.
        using (Diagram diagram = new Diagram(memoryStream))
        {
            // Access the Title property from the document's properties.
            return diagram.DocumentProps.Title;
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
