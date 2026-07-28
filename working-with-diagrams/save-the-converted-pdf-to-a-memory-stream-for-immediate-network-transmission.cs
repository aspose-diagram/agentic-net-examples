using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PdfExporter
{
    // Loads a Visio diagram, converts it to PDF and returns the PDF as a MemoryStream.
    public MemoryStream ConvertDiagramToPdf(string diagramPath)
    {
        // Load the Visio diagram from the specified file.
        Diagram diagram = new Diagram(diagramPath);

        // Prepare a memory stream to hold the PDF data.
        MemoryStream pdfStream = new MemoryStream();

        // Create PDF save options (default settings are sufficient for most cases).
        PdfSaveOptions pdfOptions = new PdfSaveOptions();

        // Save the diagram to the memory stream in PDF format using the provided Save rule.
        diagram.Save(pdfStream, pdfOptions);

        // Reset the stream position to the beginning so it can be read immediately.
        pdfStream.Position = 0;

        // The caller can now transmit pdfStream over the network.
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
