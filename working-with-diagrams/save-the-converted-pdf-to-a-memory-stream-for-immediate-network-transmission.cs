using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a memory stream to hold the PDF output
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    // Save the diagram as PDF directly into the memory stream
                    diagram.Save(pdfStream, SaveFileFormat.Pdf);

                    // Reset stream position for any subsequent reading/transmission
                    pdfStream.Position = 0;

                    // Example output: report the size of the generated PDF
                    Console.WriteLine($"PDF successfully saved to memory stream. Size: {pdfStream.Length} bytes.");

                    // At this point, pdfStream can be sent over the network as needed.
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