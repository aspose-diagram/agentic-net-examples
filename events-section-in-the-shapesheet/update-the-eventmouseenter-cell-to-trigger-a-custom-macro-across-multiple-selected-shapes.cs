using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving; // Required for SaveFileFormat

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------
            // Add a VBA module with the custom macro (MyMacro)
            // -------------------------------------------------
            // Create (or reuse) a procedural module named "CustomMacros"
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacros");
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];

            // Define the macro code that will be called on mouse‑enter
            module.Codes = @"
Public Sub MyMacro()
    MsgBox ""Mouse entered shape!""
End Sub
";

            // -------------------------------------------------
            // Update EventDblClick for the selected shapes (as EventMouseEnter is not a valid cell)
            // -------------------------------------------------
            // For demonstration, select all shapes whose universal name starts with "Rect"
            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.StartsWith("Rect"))
                {
                    // Set the EventDblClick cell to call the custom macro
                    shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";
                }
            }

            // -------------------------------------------------
            // Save the diagram as a macro‑enabled Visio file
            // -------------------------------------------------
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}