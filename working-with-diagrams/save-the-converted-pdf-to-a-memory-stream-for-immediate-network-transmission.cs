using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare PDF save options (default options are sufficient for this example)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // MemoryStream to hold the generated PDF for immediate transmission
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    // Save the diagram as PDF into the memory stream
                    diagram.Save(pdfStream, pdfOptions);

                    // Reset stream position to the beginning if further reading is needed
                    pdfStream.Position = 0;

                    // Example: output the size of the generated PDF
                    Console.WriteLine($"PDF generated successfully. Size: {pdfStream.Length} bytes.");

                    // (Optional) Write the PDF to a file for verification
                    // File.WriteAllBytes("output.pdf", pdfStream.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}