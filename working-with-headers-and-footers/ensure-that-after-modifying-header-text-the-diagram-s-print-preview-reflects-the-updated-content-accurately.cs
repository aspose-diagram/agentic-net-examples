using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load an existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Modify the header text (global header/footer)
            diagram.HeaderFooter.HeaderLeft = "Updated Header Left";
            diagram.HeaderFooter.HeaderCenter = "Updated Header Center";
            diagram.HeaderFooter.HeaderRight = "Updated Header Right";

            // Verify that the header was updated
            if (diagram.HeaderFooter.HeaderCenter != "Updated Header Center")
            {
                throw new Exception("Header text was not updated correctly.");
            }

            // Create a print document for the diagram (required for print preview scenarios)
            AsposeDiagramPrintDocument printDoc = new AsposeDiagramPrintDocument(diagram);

            // Configure print/save options – render to PDF as a preview of the printed output
            PrintSaveOptions printOptions = new PrintSaveOptions();
            printOptions.DefaultFont = "Arial";                     // Ensure proper font rendering
            printOptions.SaveFormat = SaveFileFormat.Pdf;           // Output format for preview
            printOptions.EnlargePage = true;                       // Enlarge page to fit content if needed
            printOptions.ExportGuideShapes = false;                // Example setting; adjust as required
            printOptions.IsExportComments = false;                 // Example setting; adjust as required

            // Save the diagram using the print options – the resulting PDF reflects the updated header
            string previewPath = "preview_output.pdf";
            diagram.Save(previewPath, printOptions);

            Console.WriteLine("Header updated and preview PDF saved to: " + previewPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}