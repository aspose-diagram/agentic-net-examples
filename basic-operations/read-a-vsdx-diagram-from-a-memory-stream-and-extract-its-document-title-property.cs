using System;
using System.IO;
using Aspose.Diagram;

public static class VisioHelper
{
    /// <summary>
    /// Reads a VSDX diagram from a memory stream and returns its document title.
    /// </summary>
    /// <param name="memoryStream">MemoryStream containing the VSDX file data.</param>
    /// <returns>The title stored in the diagram's DocumentProps, or null if not set.</returns>
    public static string GetDiagramTitle(MemoryStream memoryStream)
    {
        // Ensure the stream is positioned at the beginning before loading.
        memoryStream.Position = 0;

        // Load the diagram from the stream using the Diagram(Stream) constructor.
        using (Diagram diagram = new Diagram(memoryStream))
        {
            // Access the DocumentProps.Title property which holds the document's title.
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
