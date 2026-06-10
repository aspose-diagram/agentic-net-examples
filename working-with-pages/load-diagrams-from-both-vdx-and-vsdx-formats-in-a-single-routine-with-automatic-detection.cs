using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramLoader
{
    /// <summary>
    /// Loads a Visio diagram from a file, automatically detecting whether it is VDX or VSDX (or any other supported format).
    /// </summary>
    /// <param name="filePath">Full path to the Visio file.</param>
    /// <returns>Loaded Diagram object.</returns>
    public static Diagram LoadDiagram(string filePath)
    {
        // Detect the file format using Aspose.Diagram's built‑in utility.
        // This returns a FileFormatInfo object that contains the detected LoadFileFormat.
        var formatInfo = FileFormatUtil.DetectFileFormat(filePath);

        // Retrieve the detected format (e.g., LoadFileFormat.Vdx, LoadFileFormat.Vsdx, etc.).
        LoadFileFormat detectedFormat = formatInfo.LoadFormat;

        // Use the constructor that accepts both the file name and the detected format.
        // This ensures the diagram is loaded correctly without needing to guess the format.
        Diagram diagram = new Diagram(filePath, detectedFormat);

        return diagram;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
