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

            // Path to the encrypted Visio file.
            string inputPath = "encrypted.vsdx";

            // Path for the converted output (PDF in this example).
            string outputPath = "converted.pdf";

            // LoadOptions does not support a password property in this version.
            // Create a LoadOptions instance and use it when loading the diagram.
            LoadOptions loadOptions = new LoadOptions();

            // Load the diagram using the LoadOptions constructor overload.
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Simple progress tracking.
            Console.WriteLine("Conversion started...");

            // Perform a conversion operation (e.g., save as PDF).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
