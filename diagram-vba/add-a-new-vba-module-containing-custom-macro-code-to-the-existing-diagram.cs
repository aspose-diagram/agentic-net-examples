using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio diagram (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Add a new procedural VBA module named "CustomMacro"
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacro");

            // Retrieve the newly added module
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];

            // Set the VBA code for the module
            module.Codes = @"
            Attribute VB_Name = ""CustomMacro""
            Sub HelloWorld()
            MsgBox ""Hello from Aspose.Diagram VBA!""
            End Sub
            ";

            // Save the diagram in a macro‑enabled format
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);

            Console.WriteLine("VBA module added and diagram saved as VSDM.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
