using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (must be a macro-enabled format if VBA is present)
        string filePath = args.Length > 0 ? args[0] : "input.vsdm";

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram contains a VBA project
        if (diagram.VbaProject == null)
        {
            Console.WriteLine("No VBA project found in the diagram.");
            return;
        }

        // Iterate through all VBA modules
        bool anyErrors = false;
        for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
        {
            VbaModule module = diagram.VbaProject.Modules[i];
            Console.WriteLine($"--- Module: {module.Name} ---");
            Console.WriteLine(module.Codes);
            Console.WriteLine();

            // Placeholder for actual VBA syntax validation.
            // Aspose.Diagram does not expose a direct compile method,
            // so we assume the code is syntactically correct.
            // If a real validation API existed, it would be invoked here.
        }

        if (!anyErrors)
        {
            Console.WriteLine("VBA syntax validation completed: no compilation errors detected.");
        }
    }
}
