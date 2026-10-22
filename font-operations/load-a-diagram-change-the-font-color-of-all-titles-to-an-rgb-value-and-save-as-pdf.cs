using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [hexColor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        // Default color is red if not supplied.
        string colorHex = args.Length > 2 ? args[2] : "#FF0000";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify title shapes by name (case‑insensitive contains "Title").
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Change the font color of every character in the title shape.
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.Color.Value = colorHex;
                        }
                    }
                }
            }

            // Prepare PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format for clarity.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}