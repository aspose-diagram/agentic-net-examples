using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PdfExportService
{
    public MemoryStream ExportDiagramToPdf(string diagramPath)
    {
        // Load the diagram from file (using Aspose.Diagram's constructor)
        Diagram diagram = new Diagram(diagramPath);

        // Prepare a memory stream to hold the PDF data
        MemoryStream pdfStream = new MemoryStream();

        // Save the diagram directly to the memory stream in PDF format
        // This uses the provided Diagram.Save(Stream, SaveFileFormat) rule
        diagram.Save(pdfStream, SaveFileFormat.Pdf);

        // Reset the stream position so it can be read from the beginning
        pdfStream.Position = 0;

        // The caller can now transmit the MemoryStream over the network
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
