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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page to add a title whose font size is based on page width.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate a font size proportional to the page width (e.g., 5% of width).
                // Font size for AddText is expressed in inches.
                double fontSizeInches = pageWidth * 0.05;

                // Define the title text.
                string titleText = "Page Title";

                // Position the title near the top‑center of the page.
                double marginTop = pageHeight * 0.05;               // 5% top margin.
                double pinX = pageWidth / 2.0;                      // Center horizontally.
                double pinY = pageHeight - marginTop - (fontSizeInches / 2.0); // Slightly below top edge.

                // Define the text box dimensions (full page width, height enough for the text).
                double textBoxWidth = pageWidth;
                double textBoxHeight = fontSizeInches * 1.2;        // Approximate height for the text.

                // Add the title text shape to the page.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches).
                Shape titleShape = page.AddText(pinX, pinY, textBoxWidth, textBoxHeight,
                                                titleText, "Arial", "#000000", fontSizeInches);

                // Center‑align the paragraph within the shape, if a paragraph exists.
                if (titleShape.Paras.Count > 0)
                {
                    // Horizontal alignment enum resides in Aspose.Diagram.
                    titleShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format.

            // Save the diagram as a PDF file.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}