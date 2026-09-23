using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path to the output macro‑enabled Visio file
        string outputPath = "output.vsdm";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure a VBA project exists (VBA project is read‑only, just verify)
            if (diagram.VbaProject == null)
                throw new Exception("The diagram does not contain a VBA project.");

            // Add a new procedural module for the custom macro
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacros");
            VbaModule macroModule = diagram.VbaProject.Modules[moduleIndex];

            // Define the macro code (adjust as needed)
            macroModule.Codes = @"
Public Sub MyMacro()
    MsgBox ""Custom macro triggered!""
End Sub
";

            // Iterate over all shapes on the first page (replace with your own selection logic if needed)
            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // NOTE: EventMouseEnter is not a valid event cell in Aspose.Diagram.
                // Using EventDblClick as an example to trigger the macro on a shape event.
                shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";
            }

            // Save the diagram in a macro‑enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}