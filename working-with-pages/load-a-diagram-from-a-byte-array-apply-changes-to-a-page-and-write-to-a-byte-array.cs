using System;
using System.IO;
using Aspose.Diagram;

public static class DiagramProcessor
{
    /// <summary>
    /// Loads a Visio diagram from a byte array, modifies the active page, and returns the diagram as a new byte array.
    /// </summary>
    /// <param name="inputBytes">Byte array containing the source Visio file (VSD/VDX/etc.).</param>
    /// <returns>Byte array with the modified diagram saved in VDX format.</returns>
    public static byte[] ProcessDiagram(byte[] inputBytes)
    {
        // Load the diagram from the input byte array using the Diagram(Stream) constructor.
        using (var inputStream = new MemoryStream(inputBytes))
        using (var diagram = new Diagram(inputStream))
        {
            // Example modification: rename the active page.
            // You can replace this with any other page‑level changes you need.
            var activePage = diagram.ActivePage;
            activePage.Name = "ModifiedPage";

            // Save the modified diagram to a new memory stream.
            using (var outputStream = new MemoryStream())
            {
                // Use the Save(Stream, SaveFileFormat) overload to write the diagram.
                diagram.Save(outputStream, SaveFileFormat.Vdx);

                // Return the resulting byte array.
                return outputStream.ToArray();
            }
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
