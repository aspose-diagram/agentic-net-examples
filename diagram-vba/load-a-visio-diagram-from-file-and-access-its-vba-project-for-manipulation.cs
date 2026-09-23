using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        // Path to the existing Visio file
        string inputPath = "input.vsdx";

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Access the VBA project
        VbaProject vbaProject = diagram.VbaProject;
        Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
        Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

        // List existing modules
        Console.WriteLine("Existing VBA Modules:");
        foreach (VbaModule mod in vbaProject.Modules)
        {
            Console.WriteLine($"- {mod.Name}");
        }

        // Add a new procedural module
        int newModuleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");
        VbaModule newModule = vbaProject.Modules[newModuleIndex];

        // Set VBA code for the new module
        newModule.Codes = @"Attribute VB_Name = ""MyModule""
Sub HelloWorld()
    MsgBox ""Hello from Aspose.Diagram!""
End Sub";

        Console.WriteLine($"Added new VBA module: {newModule.Name}");

        // Save the diagram in a macro‑enabled format to preserve VBA
        string outputPath = "output.vsdm";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved with VBA to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
