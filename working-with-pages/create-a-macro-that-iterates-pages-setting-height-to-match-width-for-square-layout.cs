using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba; // Required for VbaModuleType enum

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // VBA macro that makes every page square (height = width)
            string macroCode = @"
Public Sub MakePagesSquare()
    Dim pg As Page
    For Each pg In ThisDocument.Pages
        Dim pageWidth As Double
        pageWidth = pg.PageSheet.PageProps.PageWidth.Value
        pg.PageSheet.PageProps.PageHeight.Value = pageWidth
    Next pg
End Sub
";

            // Add a new procedural VBA module and insert the macro code
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "PageMacro");
            var vbaModule = diagram.VbaProject.Modules[moduleIndex];
            vbaModule.Codes = macroCode; // Assign the macro source to the module

            // Save the diagram in a macro‑enabled format to preserve VBA code
            string outputPath = "output.vsdm";
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}