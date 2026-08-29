using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramThemeProcessor
{
    // Applies a theme from a source diagram to a target diagram loaded from memory streams.
    // Returns the themed diagram as a byte array in VDX format.
    public static byte[] ApplyTheme(byte[] targetDiagramBytes, byte[] sourceThemeBytes)
    {
        // Load the target diagram from the provided byte array.
        using (var targetStream = new MemoryStream(targetDiagramBytes))
        using (var targetDiagram = new Diagram(targetStream))
        {
            // Load the source diagram (containing the desired theme) from its byte array.
            using (var sourceStream = new MemoryStream(sourceThemeBytes))
            using (var sourceDiagram = new Diagram(sourceStream))
            {
                // Copy the theme from the source diagram to the target diagram.
                targetDiagram.CopyTheme(sourceDiagram);
            }

            // Save the themed diagram back to a memory stream.
            using (var resultStream = new MemoryStream())
            {
                // Use DiagramSaveOptions to specify the output format (VDX in this case).
                var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
                targetDiagram.Save(resultStream, saveOptions);

                // Return the resulting byte array.
                return resultStream.ToArray();
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
