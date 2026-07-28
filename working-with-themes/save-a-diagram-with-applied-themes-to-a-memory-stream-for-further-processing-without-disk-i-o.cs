using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramProcessor
{
    // Loads a diagram, applies a theme from another diagram, and returns the result in a memory stream.
    public MemoryStream GetDiagramWithTheme(string diagramPath, string themePath)
    {
        // Load the main diagram from file.
        using (var diagram = new Diagram(diagramPath))
        {
            // Load the diagram that contains the desired theme.
            using (var themeDiagram = new Diagram(themePath))
            {
                // Apply the theme from the source diagram.
                diagram.CopyTheme(themeDiagram);
            }

            // Prepare a memory stream to hold the saved diagram.
            var memoryStream = new MemoryStream();

            // Save the diagram to the memory stream using a specific format (e.g., VDX).
            diagram.Save(memoryStream, SaveFileFormat.Vdx);

            // Reset the stream position so it can be read from the beginning.
            memoryStream.Position = 0;

            // Return the stream to the caller for further processing.
            return memoryStream;
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
