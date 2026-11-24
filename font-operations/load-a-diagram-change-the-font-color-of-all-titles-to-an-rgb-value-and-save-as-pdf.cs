using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF file path
        string outputPath = "output.pdf";
        // Desired font color in hex format (e.g., red)
        string fontColorHex = "#FF0000";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    var page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        var shape = page.Shapes[shapeIndex];

                        // Iterate through all characters in the shape and set the font color
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            var ch = shape.Chars[charIndex];
                            ch.Color.Value = fontColorHex;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format for the options
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Example: do not export hidden pages
                pdfOptions.ExportHiddenPage = false;

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram saved successfully to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}