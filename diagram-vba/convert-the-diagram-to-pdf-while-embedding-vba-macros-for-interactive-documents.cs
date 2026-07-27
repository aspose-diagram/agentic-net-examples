using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (must exist)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has a VBA project (creates one if absent)
                if (diagram.VbaProject == null)
                {
                    throw new Exception("VBA project is not available in the loaded diagram.");
                }

                // Add a new procedural VBA module
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "MyMacroModule");
                var vbaModule = diagram.VbaProject.Modules[moduleIndex];

                // Set VBA code for the module
                vbaModule.Codes = @"
                Attribute VB_Name = ""MyMacroModule""
                Sub HelloWorld()
                MsgBox ""Hello from VBA!""
                End Sub
                ";

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker

                // Save the diagram as PDF (VBA macros remain embedded in the source Visio file)
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram converted to PDF and saved at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }