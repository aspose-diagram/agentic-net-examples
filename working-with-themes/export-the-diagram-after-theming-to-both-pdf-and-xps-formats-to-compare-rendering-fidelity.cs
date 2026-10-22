using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output file paths
        string outputPdfPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ThemedOutput.pdf");
        string outputXpsPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ThemedOutput.xps");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // TODO: Apply desired theme to the diagram.
                // The theming API is not defined in the current reference.
                // Insert theming code here when the appropriate Aspose.Diagram methods are available.

                // -------------------- Export to PDF --------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Ensure the options know they are for PDF format
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Example: do not export hidden pages
                pdfOptions.ExportHiddenPage = false;
                // Set a default font to avoid missing font issues
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF exported successfully to: {outputPdfPath}");

                // -------------------- Export to XPS --------------------
                XPSSaveOptions xpsOptions = new XPSSaveOptions();
                // Ensure the options know they are for XPS format
                xpsOptions.SaveFormat = SaveFileFormat.Xps;
                // Example: do not export hidden pages
                xpsOptions.ExportHiddenPage = false;
                // Set a default font for XPS as well
                xpsOptions.DefaultFont = "Arial";

                // Save the diagram as XPS
                diagram.Save(outputXpsPath, xpsOptions);
                Console.WriteLine($"XPS exported successfully to: {outputXpsPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}