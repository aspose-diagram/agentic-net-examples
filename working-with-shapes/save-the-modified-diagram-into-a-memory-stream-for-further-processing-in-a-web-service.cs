using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramProcessor
{
    // Loads a Visio diagram, performs optional modifications, and returns it as a memory stream.
    public MemoryStream GetDiagramAsStream(string sourceFilePath)
    {
        // Load the diagram from a file using the default constructor.
        Diagram diagram = new Diagram(sourceFilePath);

        // ----- Place for diagram modifications -----
        // Example: change the background color of the first page (optional).
        // if (diagram.Pages.Count > 0)
        // {
        //     diagram.Pages[0].Background = 2; // set to a predefined background index
        // }
        // -------------------------------------------

        // Prepare a memory stream to hold the saved diagram.
        MemoryStream memoryStream = new MemoryStream();

        // Save the diagram into the memory stream in VDX format.
        diagram.Save(memoryStream, SaveFileFormat.Vdx);

        // Reset the stream position so it can be read from the beginning by the caller.
        memoryStream.Position = 0;

        return memoryStream;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
