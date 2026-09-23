using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";
        // Guard: ensure source file exists
        if (!File.Exists(sourcePath)) { Console.Error.WriteLine($"File not found: {sourcePath}"); return; }

        // Paths for the macro-enabled Visio file and the final PDF
        string macroEnabledPath = "output.vsdm";
        string pdfPath = "output.pdf";

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(sourcePath);

            // Access the VBA project (read‑only property)
            VbaProject vbaProject = diagram.VbaProject;

            // Add a new procedural module named "InteractiveModule"
            int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "InteractiveModule");

            // Retrieve the newly added module and set its VBA code
            VbaModule module = vbaProject.Modules[moduleIndex];
            module.Codes = @"
Attribute VB_Name = ""InteractiveModule""
Sub ShowMessage()
    MsgBox ""Hello from VBA!""
End Sub
";

            // Save the diagram as a macro‑enabled Visio file (VSDM)
            diagram.Save(macroEnabledPath, SaveFileFormat.Vsdm);

            // Prepare PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the same diagram (which now contains VBA) to PDF
            diagram.Save(pdfPath, pdfOptions);

            Console.WriteLine("Diagram successfully saved with VBA macros and exported to PDF.");
        }
        catch (Exception ex)
        {
            // Write error details to the error stream
            Console.Error.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}