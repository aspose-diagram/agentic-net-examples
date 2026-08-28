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

                // Input Visio file (must exist)
                string inputVisioPath = "input.vsdx";

                // Paths for the macro‑enabled Visio file and the final PDF
                string macroEnabledPath = "output.vsdm";
                string pdfOutputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // -------------------------------------------------
                // Embed VBA macro
                // -------------------------------------------------
                // Add a new procedural module named "MyMacro"
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "MyMacro");
                VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];

                // Set the VBA code for the module
                vbaModule.Codes = @"
                Attribute VB_Name = ""MyMacro""
                Sub HelloWorld()
                MsgBox ""Hello from VBA!""
                End Sub
                ";

                // Save the diagram in a macro‑enabled format (VSDM)
                diagram.Save(macroEnabledPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Macro‑enabled Visio file saved to: {macroEnabledPath}");

                // -------------------------------------------------
                // Export to PDF
                // -------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Ensure the options know they are for PDF
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: set a default font for any missing fonts
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF (macros are not carried into PDF, but the source contains them)
                diagram.Save(pdfOutputPath, pdfOptions);
                Console.WriteLine($"PDF file saved to: {pdfOutputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }