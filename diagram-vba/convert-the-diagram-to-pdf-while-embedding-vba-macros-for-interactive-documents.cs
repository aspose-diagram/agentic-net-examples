using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

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

            // ------------------------------------------------------------
            // Embed a VBA module with sample macro code.
            // ------------------------------------------------------------
            // Add a new procedural VBA module named "SampleModule".
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "SampleModule");

            // Retrieve the newly added module and set its VBA source code.
            VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];
            vbaModule.Codes = @"
Sub SampleMacro()
    MsgBox ""Hello from VBA!""
End Sub
";

            // ------------------------------------------------------------
            // Configure PDF save options.
            // ------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Set default font for fallback.
            pdfOptions.DefaultFont = "Arial";

            // Do not export hidden pages.
            pdfOptions.ExportHiddenPage = false;

            // Do not split multi‑page diagrams into separate PDFs.
            pdfOptions.SplitMultiPages = false;

            // Set PDF/A compliance (optional).
            pdfOptions.Compliance = PdfCompliance.PdfA1b;

            // ------------------------------------------------------------
            // Save the diagram as PDF.
            // ------------------------------------------------------------
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully converted to PDF with embedded VBA macros: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}