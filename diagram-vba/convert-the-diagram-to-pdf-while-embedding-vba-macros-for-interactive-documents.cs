using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Embed a VBA macro into the diagram
                // ------------------------------------------------------------
                // Add a new procedural VBA module named "InteractiveModule"
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "InteractiveModule");
                // Retrieve the newly added module
                VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];
                // Set the VBA code for the module
                vbaModule.Codes = "Sub ShowMessage()\n    MsgBox \"Hello from VBA macro!\"\nEnd Sub";

                // ------------------------------------------------------------
                // Configure PDF save options
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optionally specify a default font for PDF rendering
                pdfOptions.DefaultFont = "Arial";

                // ------------------------------------------------------------
                // Save the diagram as a PDF file
                // ------------------------------------------------------------
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully converted to PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}