using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramMacroNetworkSender
{
    // Loads a Visio diagram (with macros) from a file and saves it to a memory stream.
    // The stream can then be sent over the network.
    public static MemoryStream GetDiagramStream(string filePath)
    {
        // Load the diagram that contains VBA macros.
        // The constructor Diagram(string) loads the file using the appropriate format.
        Diagram diagram = new Diagram(filePath);

        // Prepare a memory stream to hold the saved diagram.
        MemoryStream stream = new MemoryStream();

        // Save the diagram to the stream in VSDM format (macro‑enabled Visio file).
        // This uses the provided Save(Stream, SaveFileFormat) method.
        diagram.Save(stream, SaveFileFormat.Vsdm);

        // Reset the stream position to the beginning so it can be read from the start.
        stream.Position = 0;

        // Dispose the diagram object; the stream remains open for the caller.
        diagram.Dispose();

        return stream;
    }

    // Example usage: send the stream over a network socket or HTTP response.
    static void Main()
    {
        try
        {

            string visioFilePath = "sample_with_macro.vsdm";

            using (MemoryStream diagramStream = GetDiagramStream(visioFilePath))
            {
                // At this point, diagramStream contains the VSDM bytes ready for transmission.
                // Example: write to console the size of the stream.
                Console.WriteLine($"Diagram stream length: {diagramStream.Length} bytes");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
