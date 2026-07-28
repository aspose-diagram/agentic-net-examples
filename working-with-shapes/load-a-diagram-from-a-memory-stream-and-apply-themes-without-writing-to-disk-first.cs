using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThemeApplier
{
    // Loads a Visio diagram from a byte array (memory stream), copies a theme from another diagram,
    // and returns the themed diagram as a byte array without touching the file system.
    public static byte[] ApplyTheme(byte[] targetDiagramBytes, byte[] sourceDiagramBytes)
    {
        // Load the target diagram (the one that will receive the theme) from memory.
        using (var targetStream = new MemoryStream(targetDiagramBytes))
        using (var targetDiagram = new Diagram(targetStream))
        {
            // Load the source diagram (the one that provides the theme) from memory.
            using (var sourceStream = new MemoryStream(sourceDiagramBytes))
            using (var sourceDiagram = new Diagram(sourceStream))
            {
                // Copy the theme from the source diagram to the target diagram.
                targetDiagram.CopyTheme(sourceDiagram);
            }

            // Save the themed diagram back to a memory stream.
            using (var resultStream = new MemoryStream())
            {
                // Choose a save format (e.g., VDX). Adjust as needed.
                targetDiagram.Save(resultStream, SaveFileFormat.Vdx);

                // Return the resulting bytes.
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
