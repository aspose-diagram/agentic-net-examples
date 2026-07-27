using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class MacroExecutor
{
    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            Diagram diagram = LoadDiagram("input.vsdx");

            // Access the VBA modules collection from the diagram
            VbaModuleCollection modules = diagram.VbaProject.Modules;

            // Name of the macro we want to execute / verify
            const string targetMacroName = "MyMacro";

            // Locate the module that contains the macro
            VbaModule targetModule = null;
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].Name.Equals(targetMacroName, StringComparison.OrdinalIgnoreCase))
                {
                    targetModule = modules[i];
                    break;
                }
            }

            if (targetModule == null)
            {
                Console.WriteLine($"Macro \"{targetMacroName}\" not found.");
                return;
            }

            // Capture the macro code – this is the “output” we verify
            string macroCode = targetModule.Codes;
            Console.WriteLine("=== Macro Code ===");
            Console.WriteLine(macroCode);
            Console.WriteLine("==================");

            // (Optional) Remove the macro after verification using the provided method
            diagram.RemoveMacro();

            // Save the diagram (uses the provided save rule)
            SaveDiagram(diagram, "output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // -------------------------------------------------------------------------
    // Lifecycle helper methods – placeholders for the mandated create/load/save rules
    // -------------------------------------------------------------------------
    static Diagram LoadDiagram(string path)
    {
        // The actual implementation is supplied by the lifecycle rule.
        return new Diagram(path);
    }

    static void SaveDiagram(Diagram diagram, string path)
    {
        // The actual implementation is supplied by the lifecycle rule.
        diagram.Save(path, SaveFileFormat.Vdx);
    }
}
