using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramProcessor
{
    // Loads a diagram, applies a theme from another diagram,
    // and returns the resulting diagram as a memory stream.
    public MemoryStream GetDiagramWithThemeStream(string diagramPath, string themePath)
    {
        // Load the main diagram from file.
        using (var diagram = new Diagram(diagramPath))
        {
            // Load the diagram that contains the desired theme.
            using (var themeDiagram = new Diagram(themePath))
            {
                // Apply the theme from the theme diagram to the main diagram.
                diagram.CopyTheme(themeDiagram);
            }

            // Prepare a memory stream to hold the saved diagram.
            var outputStream = new MemoryStream();

            // Save the diagram to the memory stream in VDX format.
            // The Save method that accepts a Stream and SaveFileFormat is used as per the provided rules.
            diagram.Save(outputStream, SaveFileFormat.Vdx);

            // Reset the stream position to the beginning for downstream consumers.
            outputStream.Position = 0;

            // Return the memory stream containing the diagram with the applied theme.
            return outputStream;
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
