using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PdfExportService
{
    // Loads a diagram, converts it to PDF and returns the PDF as a MemoryStream
    public MemoryStream GetDiagramPdfStream(string diagramPath)
    {
        // Load the diagram from file (replace with appropriate load method if needed)
        Diagram diagram = new Diagram(diagramPath);

        // Prepare a memory stream to hold the PDF data
        MemoryStream pdfStream = new MemoryStream();

        // Create PDF save options (optional – customize if required)
        PdfSaveOptions pdfOptions = new PdfSaveOptions();

        // Save the diagram directly to the memory stream in PDF format using the provided Save method
        diagram.Save(pdfStream, pdfOptions);

        // Reset the stream position to the beginning so it can be read by the caller/network layer
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
