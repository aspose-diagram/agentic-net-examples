using System;
using System.IO;
using Aspose.Diagram;

public class DiagramToPdfConverter
{
    /// <summary>
    /// Loads a Visio diagram from the specified file path,
    /// converts it to PDF, and returns the PDF data in a MemoryStream.
    /// The stream is positioned at the beginning for immediate transmission.
    /// </summary>
    /// <param name="diagramPath">Full path to the source Visio file.</param>
    /// <returns>MemoryStream containing the PDF representation of the diagram.</returns>
    public static MemoryStream ConvertDiagramToPdf(string diagramPath)
    {
        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Prepare a memory stream to receive the PDF output
        MemoryStream pdfStream = new MemoryStream();

        // Save the diagram as PDF directly into the memory stream
        diagram.Save(pdfStream, SaveFileFormat.Pdf);

        // Reset the stream position so it can be read from the start
        pdfStream.Position = 0;

        return pdfStream;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
