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
            var diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a 10‑degree rotation around the Z‑axis
                    // ThreeDFormat.RotationZAngle is a DoubleValue; set its Value property
                    shape.ThreeDFormat.RotationZAngle.Value = 10.0;
                }
            }

            // Save the modified diagram as PDF
            var pdfOptions = new PdfSaveOptions(); // default options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
