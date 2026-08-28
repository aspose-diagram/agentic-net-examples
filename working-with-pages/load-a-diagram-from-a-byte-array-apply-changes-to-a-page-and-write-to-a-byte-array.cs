using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramProcessor
{
    // Loads a Visio diagram from a byte array, modifies the active page, and returns the updated diagram as a byte array.
    public static byte[] ProcessDiagram(byte[] diagramBytes)
    {
        // Load the diagram from the input byte array using a MemoryStream.
        using (var inputStream = new MemoryStream(diagramBytes))
        using (var diagram = new Diagram(inputStream))
        {
            // Example modification: rename the active page.
            // The Page class provides a Name property that can be set.
            var activePage = diagram.ActivePage;
            if (activePage != null)
            {
                activePage.Name = "ModifiedPage";
            }

            // Save the modified diagram to an output MemoryStream.
            using (var outputStream = new MemoryStream())
            {
                // Save in VDX format (Visio XML). Adjust the format as needed.
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
