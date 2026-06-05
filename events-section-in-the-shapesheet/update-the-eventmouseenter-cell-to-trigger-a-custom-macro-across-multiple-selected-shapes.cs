using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdm";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            string macroCode = @"
Attribute VB_Name = ""Module1""
Public Sub MyMacro()
    MsgBox ""Macro triggered!""
End Sub";

            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "Module1");
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];
            module.Codes = macroCode;

            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdm);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}