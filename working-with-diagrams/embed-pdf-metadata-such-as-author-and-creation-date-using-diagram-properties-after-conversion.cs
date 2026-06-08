using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Set PDF metadata via DocumentProperties
            diagram.DocumentProps.Creator = "John Doe";               // Author
            diagram.DocumentProps.Title = "Project Overview Diagram"; // Title
            diagram.DocumentProps.TimeCreated = DateTime.Now;         // Creation date

            // Configure PDF save options (default settings are sufficient for metadata)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as PDF with the embedded metadata
            diagram.Save("output.pdf", pdfOptions);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
