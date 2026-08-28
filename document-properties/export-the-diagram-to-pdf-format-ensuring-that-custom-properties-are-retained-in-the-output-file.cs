using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // (Optional) Add or modify custom document properties here.
            // diagram.DocumentProps.Add(new DocumentProperty { Name = "MyCustomProp", Value = "CustomValue" });

            // Create PDF save options – default settings retain document properties
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Export the diagram to PDF while preserving custom properties
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
