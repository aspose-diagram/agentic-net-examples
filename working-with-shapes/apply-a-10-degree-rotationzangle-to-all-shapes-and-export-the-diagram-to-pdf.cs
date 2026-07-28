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
            using (var diagram = new Diagram("input.vsdx"))
            {
                // Apply a 10‑degree rotation around the Z‑axis to every shape in the document
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // RotationZAngle is a DoubleValue; set its numeric value
                        shape.ThreeDFormat.RotationZAngle.Value = 10.0;
                    }
                }

                // Export the modified diagram to PDF using PdfSaveOptions
                var pdfOptions = new PdfSaveOptions();
                diagram.Save("output.pdf", pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
