using System;
using System.IO;
using Aspose.Diagram;

public class DiagramProcessor
{
    // Loads a Visio diagram from a network stream, modifies a page, and writes it back to the same stream.
    public void ProcessDiagram(Stream networkStream, int pageIndex)
    {
        // Load the diagram from the provided stream.
        // Diagram(Stream) constructor reads the diagram data from the stream.
        using (var diagram = new Diagram(networkStream))
        {
            // Example modification: rename the specified page.
            if (pageIndex >= 0 && pageIndex < diagram.Pages.Count)
            {
                var page = diagram.Pages[pageIndex];
                page.Name = page.Name + "_Modified";
            }

            // Prepare the stream for writing the updated diagram.
            // Reset position to the beginning and truncate any existing data.
            if (networkStream.CanSeek)
            {
                networkStream.Position = 0;
                networkStream.SetLength(0);
            }

            // Save the diagram back to the same stream.
            // Save(Stream, SaveFileFormat) writes the diagram in the chosen format.
            diagram.Save(networkStream, SaveFileFormat.Vdx);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramProcessor();
            obj.ProcessDiagram(null, 0);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
