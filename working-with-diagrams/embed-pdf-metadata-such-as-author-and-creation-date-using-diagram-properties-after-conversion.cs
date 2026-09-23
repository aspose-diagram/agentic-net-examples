using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set built‑in document properties
            diagram.DocumentProps.Title = "Sample Diagram";
            diagram.DocumentProps.TimeCreated = DateTime.Now; // Creation date

            // Add a custom property for the author
            CustomProp authorProp = new CustomProp();
            authorProp.Name = "Author";
            authorProp.PropType = PropType.String;
            authorProp.CustomValue.ValueString = "John Doe";
            diagram.DocumentProps.CustomProps.Add(authorProp);

            // Configure PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF; custom and built‑in properties are embedded in the PDF metadata
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
