using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (can be .vsdx, .vsdm, etc.)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPdfPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has a VBA project (creates one if missing)
                if (diagram.VbaProject == null)
                {
                    throw new Exception("The diagram does not contain a VBA project.");
                }

                // Add a new procedural VBA module
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "InteractiveModule");
                VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];

                // Set VBA code (example macro)
                vbaModule.Codes = @"
                Attribute VB_Name = ""InteractiveModule""
                Sub ShowMessage()
                MsgBox ""Hello from embedded VBA!""
                End Sub
                ";

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF (macros are not carried into PDF, but they remain in the source diagram)
                diagram.Save(outputPdfPath, pdfOptions);

                // Optional: also save the macro‑enabled Visio file for future editing
                string outputVsdmPath = "output.vsdm";
                diagram.Save(outputVsdmPath, SaveFileFormat.Vsdm);

                Console.WriteLine("Diagram converted to PDF and VBA macro embedded successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }