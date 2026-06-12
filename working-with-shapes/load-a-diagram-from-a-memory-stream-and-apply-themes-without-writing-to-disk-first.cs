using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramThemeApplier
{
    /// <summary>
    /// Loads a Visio diagram from a memory stream, copies a theme from another diagram (also loaded from memory),
    /// and returns the themed diagram as a new memory stream in the desired format.
    /// </summary>
    /// <param name="targetDiagramBytes">Byte array containing the target diagram.</param>
    /// <param name="sourceThemeBytes">Byte array containing the source diagram that holds the theme.</param>
    /// <param name="saveFormat">The format in which the resulting diagram should be saved.</param>
    /// <returns>A MemoryStream containing the themed diagram.</returns>
    public static MemoryStream ApplyThemeFromStream(byte[] targetDiagramBytes, byte[] sourceThemeBytes, SaveFileFormat saveFormat)
    {
        // Load the target diagram from the provided byte array using a memory stream.
        using (var targetStream = new MemoryStream(targetDiagramBytes))
        using (var targetDiagram = new Diagram(targetStream))
        // Load the source diagram (which contains the desired theme) from its byte array.
        using (var sourceStream = new MemoryStream(sourceThemeBytes))
        using (var sourceDiagram = new Diagram(sourceStream))
        {
            // Copy the theme from the source diagram to the target diagram.
            targetDiagram.CopyTheme(sourceDiagram);

            // Prepare a memory stream to hold the saved, themed diagram.
            var resultStream = new MemoryStream();

            // Save the diagram to the result stream using the specified format.
            // Using DiagramSaveOptions allows more control if needed; here we use the simple overload.
            targetDiagram.Save(resultStream, saveFormat);

            // Reset the position of the stream so it can be read from the beginning by the caller.
            resultStream.Position = 0;
            return resultStream;
        }
    }
}

// Example usage:
// byte[] diagramData = File.ReadAllBytes("input.vsdx");          // Replace with actual source.
// byte[] themeData   = File.ReadAllBytes("themeSource.vsdx");    // Replace with actual theme source.
// using (MemoryStream themedStream = DiagramThemeApplier.ApplyThemeFromStream(diagramData, themeData, SaveFileFormat.Vsdx))
// {
//     // The themedStream now contains the diagram with the applied theme.
//     // It can be written to a file, sent over a network, etc.
//     File.WriteAllBytes("output.vsdx", themedStream.ToArray());
// }

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
