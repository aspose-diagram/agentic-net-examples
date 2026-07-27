using System;
using System.IO;
using Aspose.Diagram;

public class VisioHelper
{
    // Reads a VSDX diagram from a memory stream and returns its document title.
    public static string GetDiagramTitle(MemoryStream visioStream)
    {
        // Ensure the stream position is at the beginning.
        if (visioStream.Position != 0)
            visioStream.Position = 0;

        // Load the diagram from the provided stream.
        using (Diagram diagram = new Diagram(visioStream))
        {
            // Access the Title property from the document's properties.
            // It may be null or empty if not set.
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
